import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { MaterialComponentsModule } from '../common/material-components.module';
import { HighlightTopVacancyDirective } from './highlight-top-vacancy.directive';
import { HighlightRecruiterDirective } from './highlight-recruiter.directive';
import { RecruiterCardComponent } from './recruiter-card/recruiter-card.component';
import { VacancyCardComponent } from './vacancy-card/vacancy-card.component';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import { SharedModule } from '../shared/modules/shared/shared.module';
@NgModule({
  declarations: [
    HomeComponent,
    RecruiterCardComponent,
    VacancyCardComponent,
    HighlightTopVacancyDirective,
    HighlightRecruiterDirective,
  ],
  imports: [CommonModule, MaterialComponentsModule, FontAwesomeModule, SharedModule],
  exports: [HomeComponent],
})
export class HomeModule {}
