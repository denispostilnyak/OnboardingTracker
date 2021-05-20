import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { Observable } from 'rxjs';
import { User } from '../../models/auth/user';
import { auth0LockOptions } from './auth-configurations';
import Auth0Lock from 'auth0-lock';
import jwtDecode from 'jwt-decode';
import { AuthServiceApi } from './auth-interface';
import { AUTH_CONFIG } from './auth-variables';

const enum LocalStorage {
  IdToken = 'id_token',
  AccessToken = 'access_token',
  ExpiresAt = 'expires_at'
}

@Injectable()
export class AuthLockService implements AuthServiceApi {

  private userMetadata: User = {
    email: '',
    avatar: ''
  };

  private lock = new Auth0Lock(AUTH_CONFIG.clientID, AUTH_CONFIG.domain, auth0LockOptions);

  private authSubject = new BehaviorSubject<boolean>(false);

  isAuthenticated$ = this.authSubject.asObservable();

  private userSubject = new Subject<User>();

  isUserFilled$ = this.userSubject.asObservable();

  constructor() {
  }

  authenticate = () => {
    if (this.isAuthenticated()) {
      this.authenticateFromToken();
    } else {
      this.login();
    }

    this.lock.on('authenticated', (authResult: any) => {
      this.setSession(authResult);
      if (!authResult.idTokenPayload) {
        this.dispatchUserFromToken(jwtDecode(authResult.id_token));
      } else {
        this.dispatchUserFromToken(authResult.idTokenPayload);
      }

      this.getUserInfo();
    });

    this.lock.on('authorization_error', (error: any) => console.error(error));
  }

  login = () => this.lock.show();

  logout = () => {
    localStorage.removeItem(LocalStorage.IdToken);
    localStorage.removeItem(LocalStorage.AccessToken);
    localStorage.removeItem(LocalStorage.ExpiresAt);
    this.authSubject.next(false);
  }

  getUserInfo(): void {
    const avatar = this.userMetadata.avatar;
    this.lock.getUserInfo(localStorage.getItem(LocalStorage.AccessToken) || '', (error, profile) => {
      if (!error) {
        this.userSubject.next(
          {
            name: profile.name,
            email: profile.email,
            avatar: avatar
          });
      }
    });

  }

  getToken = () => localStorage.getItem(LocalStorage.IdToken);

  isAuthenticated = () => new Date().getTime() < JSON.parse(localStorage.getItem(LocalStorage.ExpiresAt) || 'null');

  get authenticated(): boolean {
    return this.isAuthenticated();
  }

  private authenticateFromToken = () => {
    const token = this.getToken();
    if (!token) {
      return;
    }
    this.dispatchUserFromToken(jwtDecode(token));
    this.getUserInfo();
  }

  private dispatchUserFromToken = (token: any) => {
    this.userMetadata.avatar = token.picture;
    this.authSubject.next(true);
  }

  private setSession = ({ expiresIn, idToken, accessToken }: auth0.Auth0DecodedHash) => {
    if (idToken) {
      localStorage.setItem(LocalStorage.IdToken, idToken);
    }
    if (accessToken) {
      localStorage.setItem(LocalStorage.AccessToken, accessToken);
    }
    if (expiresIn) {
      localStorage.setItem(LocalStorage.ExpiresAt, JSON.stringify(expiresIn * 1000 + new Date().getTime()));
    }
  }
}
