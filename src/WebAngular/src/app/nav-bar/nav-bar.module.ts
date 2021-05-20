import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar.component';
import { MaterialComponentsModule } from '../common/material-components.module';
import { RouterModule } from '@angular/router';
import {SharedModule} from '../shared/modules/shared/shared.module';


@NgModule({
  declarations: [NavBarComponent],
  imports: [
    CommonModule,
    MaterialComponentsModule,
    RouterModule,
    SharedModule,
  ],
  exports: [
    NavBarComponent
  ]
})
export class NavBarModule { }
