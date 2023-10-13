using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieStoreCore.Domain;
using MovieStoreInfrastructure;
using MovieStoreInfrastructure.Repositories;
using System.Net;
using System.Net.Mail;

namespace MovieStoreApi
{
    public class TimedHostedService : IHostedService, IDisposable
    {

        private readonly ILogger<TimedHostedService> _logger;
        private Timer? _timer = null;
        private IServiceProvider _serviceProvider;
        private EmailServiceOptions _emailServiceOptions;

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceProvider serviceProvider, IOptions<EmailServiceOptions> emailServiceOptions)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _emailServiceOptions = emailServiceOptions.Value;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(_emailServiceOptions.TimeInterval));

            return Task.CompletedTask;
        }

        private void SendExpirationNotification(PurchasedMovie purchase)
        {
            using (SmtpClient smtpClient = new SmtpClient(_emailServiceOptions.SmtpServer))
            {
                smtpClient.Port = _emailServiceOptions.SmtpPort;
                smtpClient.Credentials = new NetworkCredential(_emailServiceOptions.SmtpUsername, _emailServiceOptions.SmtpPassword);
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("stemultra@gmail.com");
                    mailMessage.Subject = "Movie Hold Expiration";
                    mailMessage.Body = $"Your hold on the movie '{purchase.Movie.Title}' has expired.";
                    mailMessage.IsBodyHtml = true;

                    mailMessage.To.Add(purchase.Customer.Email);

                    try
                    {
                        smtpClient.Send(mailMessage);
                        Console.WriteLine($"Expiration notification email sent to {purchase.Customer.Email}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Email sending failed: {ex.Message}");
                    }
                }
            }
        }

        private void DoWork(object? state)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MovieStoreContext>();
            var purchasedMovies = context.PurchasedMovies
            .Include(pm => pm.Movie)
            .Include(pm => pm.Customer)
            .Where(x => x.ExpirationDate != null)
            .ToList();
            var repository = scope.ServiceProvider.GetRequiredService<IRepository<PurchasedMovie>>();

            //List<PurchasedMovie> purchasedMovies = repository.GetAll().ToList();
            foreach (var purchasedMovie in purchasedMovies)
            {
                if (purchasedMovie.ExpirationDate.Value < DateTime.UtcNow && DateTime.UtcNow < purchasedMovie.ExpirationDate.Value.AddMinutes(_emailServiceOptions.TimeInterval))
                {
                    SendExpirationNotification(purchasedMovie);
                    _logger.LogInformation("Timed Hosted Service is working. Count");
                }
            }
        }
        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

