import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Recruiter } from 'src/app/models/recruiter/recruiter';
import { DeleteRecruiterDialogComponent } from 'src/app/recruiters/delete-recruiter-dialog/delete-recruiter-dialog.component';

@Component({
  selector: 'app-delete-candidate-dialog',
  templateUrl: './delete-candidate-dialog.component.html',
  styleUrls: ['./delete-candidate-dialog.component.scss']
})
export class DeleteCandidateDialogComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<DeleteRecruiterDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Recruiter) { }

  ngOnInit(): void {
  }
}
