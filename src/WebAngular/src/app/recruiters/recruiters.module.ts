import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecruitersComponent } from './recruiters.component';
import { MaterialComponentsModule } from '../common/material-components.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { RecruitersDialogComponent } from './recruiters-dialog/recruiters-dialog.component';
import { FormsModule } from '@angular/forms';
import { DeleteRecruiterDialogComponent } from './delete-recruiter-dialog/delete-recruiter-dialog.component';
import { RecruiterListItemComponent } from './recruiter-list-item/recruiter-list-item.component';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { SharedModule } from '../shared/modules/shared/shared.module';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};
@NgModule({
  declarations: [
    RecruitersComponent,
    RecruitersDialogComponent,
    DeleteRecruiterDialogComponent,
    RecruiterListItemComponent,
  ],
  imports: [
    CommonModule,
    MaterialComponentsModule,
    FontAwesomeModule,
    FormsModule,
    PerfectScrollbarModule,
    SharedModule
  ],
  exports: [RecruitersDialogComponent],
  providers: [{
    provide: PERFECT_SCROLLBAR_CONFIG,
    useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
  }]
})
export class RecruitersModule {}
