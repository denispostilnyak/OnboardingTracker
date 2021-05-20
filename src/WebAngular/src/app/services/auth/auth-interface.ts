export interface AuthServiceApi {
  authenticated: boolean;
  logout: () => void;
  login: () => void;
  authenticate: () => void;
}
