import { environment } from 'src/environments/environment';
import { AuthConfig } from '../../models/auth/auth-config';


export const AUTH_CONFIG: AuthConfig = {
  clientID: 'BZ4yzDRgRonE6tejYLXJNWZ19LRKLVxv',
  domain: 'onboarding-tracker.us.auth0.com',
  callbackURL: environment.callbackUrl,
  scope: 'openid profile email',
  audience: 'https://onboardingtracker'
};
