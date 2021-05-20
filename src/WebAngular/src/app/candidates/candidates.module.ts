import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialComponentsModule } from '../common/material-components.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FormsModule } from '@angular/forms';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { CandidatesComponent } from './candidates.component';
import { CandidatesDialogComponent } from './candidate-dialog/candidate-dialog.component';
import { DeleteCandidateDialogComponent } from './delete-candidate-dialog/delete-candidate-dialog.component';
import {CandidateListItemComponent} from './candidate-list-item/candidate-list-item.component';
@NgModule({
  declarations: [CandidatesComponent, CandidatesDialogComponent, DeleteCandidateDialogComponent, CandidateListItemComponent],
  imports: [
    CommonModule,
    MaterialComponentsModule,
    FontAwesomeModule,
    FormsModule,
    PerfectScrollbarModule,
  ],
})
export class CandidatesModule { }
