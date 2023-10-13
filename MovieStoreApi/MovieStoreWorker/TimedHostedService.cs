using MovieStoreCore.Domain;
using MovieStoreInfrastructure.Repositories;
using System.Net;
using System.Net.Mail;

namespace MovieStoreWorker
{
    public class TimedHostedService : IHostedService, IDisposable
    {

        private readonly ILogger<TimedHostedService> _logger;
        private Timer? _timer = null;
        private readonly IRepository<PurchasedMovie> _repository;


        public TimedHostedService(ILogger<TimedHostedService> logger, IRepository<PurchasedMovie> repository)
        {
            _logger = logger;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void SendExpirationNotification(PurchasedMovie purchase)
        {
            // SMTP server settings
            string smtpServer = "smtp.elasticemail.com";
            int smtpPort = 2525;
            string smtpUsername = "stemultra@gmail.com";
            string smtpPassword = "A989F9A2953FB59EA8BF87AB3288315FC61B";

            using (SmtpClient smtpClient = new SmtpClient(smtpServer))
            {
                smtpClient.Port = smtpPort;
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
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
            List<PurchasedMovie> purchasedMovies = _repository.GetAll().ToList();
            foreach (var purchasedMovie in purchasedMovies)
            {
                if (purchasedMovie.ExpirationDate.HasValue && purchasedMovie.ExpirationDate.Value < DateTime.UtcNow)
                {
                    SendExpirationNotification(purchasedMovie);
                    _repository.Delete(purchasedMovie);
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