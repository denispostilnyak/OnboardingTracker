import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VacanciesComponent } from './vacancies.component';
import { MaterialComponentsModule } from '../common/material-components.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { VacancyListItemComponent } from './vacancy-list-item/vacancy-list-item.component';
import { VacancyDeleteDialogComponent } from './vacancy-delete-dialog/vacancy-delete-dialog.component';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { VacanciesDialogComponent } from './vacancies-dialog/vacancies-dialog.component';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  declarations: [VacanciesComponent, VacancyListItemComponent, VacancyDeleteDialogComponent, VacanciesDialogComponent],
  imports: [
    CommonModule,
    MaterialComponentsModule,
    FontAwesomeModule,
    PerfectScrollbarModule
  ],
  exports: [],
  providers: [{
    provide: PERFECT_SCROLLBAR_CONFIG,
    useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
  }]
})
export class VacanciesModule { }
