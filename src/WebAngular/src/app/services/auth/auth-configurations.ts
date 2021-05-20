import { AUTH_CONFIG } from './auth-variables';

const params: Auth0LockAuthParamsOptions = {
    scope: AUTH_CONFIG.scope
};

const auth: Auth0LockAuthOptions = {
    responseType: 'token id_token',
    params: params,
    redirectUrl: AUTH_CONFIG.callbackURL,
    redirect: false,
    autoParseHash: true,
    sso: false,
    audience: AUTH_CONFIG.audience
};

export const auth0LockOptions: Auth0LockConstructorOptions = {
    theme: {
        logo: 'https://devoxblob.blob.core.windows.net/timex/logo.png',
        primaryColor: '#3c4349'
    },
    auth: auth,
    socialButtonStyle: 'small',
    languageDictionary: {
        title: 'OnboardingTracker'
    },
    autoclose: true,
    rememberLastLogin: true,
    closable: true,
    avatar: {
        url: (email: any, cb: any) => cb({} as auth0.Auth0Error, ''),
        displayName: (email: any, cb: any) => cb({} as auth0.Auth0Error, '')
    }
};
