import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { AuthLockService } from '../services/auth/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

    constructor(private auth: AuthLockService) { }

    canActivate(): boolean {
        if (!this.auth.isAuthenticated()) {
            this.auth.authenticate();
            return false;
        }
        return true;
    }
}
