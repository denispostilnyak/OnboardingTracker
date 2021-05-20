import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Recruiter } from 'src/app/models/recruiter/recruiter';
import { faPencilAlt } from '@fortawesome/free-solid-svg-icons';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { MatDialog } from '@angular/material/dialog';
import { RecruitersDialogComponent } from 'src/app/recruiters/recruiters-dialog/recruiters-dialog.component';
import { DeleteRecruiterDialogComponent } from 'src/app/recruiters/delete-recruiter-dialog/delete-recruiter-dialog.component';
import { Subject } from 'rxjs';
import { RecruiterDialogService } from 'src/app/services/recruiter/dialogs/recruiter-dialog.service';
@Component({
  selector: 'app-recruiter-list-item',
  templateUrl: './recruiter-list-item.component.html',
  styleUrls: ['./recruiter-list-item.component.scss'],
})
export class RecruiterListItemComponent {
  faPencil = faPencilAlt;
  faTrash = faTrash;
  constructor(private dialogService:RecruiterDialogService, private cdRef: ChangeDetectorRef) {}

  @Input() recruiter!: Recruiter;
  @Output() updateRecruiterEvent = new EventEmitter<FormData>();
  @Output() deleteRecruiterEvent = new EventEmitter<number>();

  openEditRecruiterDialog(): void {
    this.dialogService
    .openEditRecruiterDialog("Edit Recruiter", this.recruiter)
    .subscribe(recruiter=>{
        Object.assign(this.recruiter, recruiter);
      this.cdRef.detectChanges();
    });
  }

  openDeleteRecruiterDialog(): void {
    this.dialogService
    .openDeleteRecruiterDialog(this.recruiter)
    .subscribe((recruiter) => {
      if (recruiter) {
        console.log("deleting recruiter", this.recruiter);
        this.deleteRecruiterEvent.emit(recruiter.id);
      }
    });
  }
}
