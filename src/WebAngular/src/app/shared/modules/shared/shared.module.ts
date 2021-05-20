import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BreadcrumbsComponent } from './breadcrumbs/breadcrumbs.component';
import { RouterModule } from '@angular/router';
import {MaterialComponentsModule} from '../../../common/material-components.module';

@NgModule({
  declarations: [BreadcrumbsComponent],
  imports: [
    CommonModule,
    RouterModule,
    MaterialComponentsModule
  ],
  exports:[BreadcrumbsComponent]
})
export class SharedModule { }
