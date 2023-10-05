import { LogLevel, IPublicClientApplication, PublicClientApplication } from '@azure/msal-browser';

export function MSALInstanceFactory(): IPublicClientApplication {
  const isIE = window.navigator.userAgent.indexOf('MSIE ') > -1 || window.navigator.userAgent.indexOf('Trident/') > -1;

  return new PublicClientApplication({
    auth: {
      clientId: '15f624e0-ff19-43e7-8fc1-c3937dd559f0',
      authority: 'https://login.microsoftonline.com/f8cdef31-a31e-4b4a-93e4-5f571e91255a',
      redirectUri: 'http://localhost:4200/auth', // Update this with your actual redirect URI
    },
    cache: {
      cacheLocation: 'localStorage',
      storeAuthStateInCookie: isIE,
    },
    system: {
      loggerOptions: {
        loggerCallback: (logLevel, message, piiEnabled) => {
          console.log(message);
        },
        logLevel: LogLevel.Verbose,
        piiLoggingEnabled: false,
      },
    },
  });
}