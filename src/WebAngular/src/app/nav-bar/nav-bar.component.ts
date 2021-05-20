import { User } from './../models/auth/user';
import { ChangeDetectorRef, Component, HostListener, OnInit } from '@angular/core';
import { AuthLockService } from '../services/auth/auth.service';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  hideBreadcrumbs$ = new Subject<boolean>();

  user!: User | null;
  isAuthenticated = false;
  languages!: string[];
  currentLang!: string;
  constructor(
    private auth: AuthLockService,
    private cdRef: ChangeDetectorRef,
    private translate: TranslateService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    if(!this.auth.isAuthenticated()) {
      this.router.navigate(['/']);
    }

    this.initTranslate();
    this.login();
    this.auth.isUserFilled$.subscribe((user: User) => {
      this.isAuthenticated = true;
      this.user = user;
      this.cdRef.detectChanges();
    });
    this.router.events.pipe(filter((event)=>event instanceof NavigationEnd)).subscribe((event)=>{
      if(this.router.url=="/"){
        this.hideBreadcrumbs$.next(true);
      }else{
        this.hideBreadcrumbs$.next(false);
      }
    });
  }

  initTranslate(): void {
    this.translate.addLangs(['en', 'ua']);
    this.translate.setDefaultLang('en');
    this.languages = this.translate.getLangs();
    const browserLang = this.translate.getBrowserLang();
    this.translate.use(browserLang.match(/en|ua/) ? browserLang : 'en');
    this.currentLang = 'en';
  }

  translatePage(lang: string): void {
    this.currentLang = lang;
    this.translate.use(lang);
  }

  login(): void {
    this.auth.authenticate();
  }

  logout(): void {
    this.auth.logout();
    this.isAuthenticated = false;
    this.user = null;
    this.router.navigate(['/']);
    this.cdRef.detectChanges();
  }
}
