import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Recruiter } from 'src/app/models/recruiter/recruiter';

@Component({
  selector: 'app-delete-recruiter-dialog',
  templateUrl: './delete-recruiter-dialog.component.html',
  styleUrls: ['./delete-recruiter-dialog.component.scss']
})
export class DeleteRecruiterDialogComponent  {

  constructor(
    public dialogRef: MatDialogRef<DeleteRecruiterDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Recruiter
  ) {}

  closeWithoutDeleting(): void{
    this.dialogRef.close();
  }
}
