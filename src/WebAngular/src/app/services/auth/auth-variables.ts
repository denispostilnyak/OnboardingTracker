import { environment } from 'src/environments/environment';
import { AuthConfig } from '../../models/auth/auth-config';


export const AUTH_CONFIG: AuthConfig = {
  clientID: 'hgOJjh1n44wEU200ltBzkrq20IOAL6Pf',
  domain: 'devoxsoftware.eu.auth0.com',
  callbackURL: environment.callbackUrl,
  scope: 'openid profile email',
  audience: 'https://onboardingtracker/api'
};
