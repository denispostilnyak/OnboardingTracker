import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VacancyComponent } from './vacancy.component';
import { MaterialComponentsModule } from '../common/material-components.module';

@NgModule({
  declarations: [VacancyComponent],
  imports: [
    CommonModule,
    MaterialComponentsModule
  ],
  exports: [VacancyComponent]
})
export class VacancyModule { }
