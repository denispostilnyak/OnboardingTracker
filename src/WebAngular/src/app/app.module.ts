import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeModule } from './home/home.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialComponentsModule } from './common/material-components.module';
import { InterviewModule } from './interview/interview.module';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NavBarModule } from './nav-bar/nav-bar.module';
import { VacanciesModule } from './vacancies/vacancies.module';
import { RecruitersModule } from './recruiters/recruiters.module';
import { CandidatesModule } from './candidates/candidates.module';
import { AuthLockService } from './services/auth/auth.service';
import { ToastrModule } from 'ngx-toastr';
import {SharedModule} from './shared/modules/shared/shared.module';
@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HomeModule,
    BrowserAnimationsModule,
    InterviewModule,
    MaterialComponentsModule,
    HttpClientModule,
    FontAwesomeModule,
    NavBarModule,
    VacanciesModule,
    RecruitersModule,
    CandidatesModule,
    ToastrModule.forRoot(),
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: JwtInterceptor,
    multi: true
  },
    AuthLockService
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
