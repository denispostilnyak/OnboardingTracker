import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Vacancy } from 'src/app/models/vacancy/vacancy';

@Component({
  selector: 'app-vacancy-delete-dialog',
  templateUrl: './vacancy-delete-dialog.component.html',
  styleUrls: ['./vacancy-delete-dialog.component.scss']
})
export class VacancyDeleteDialogComponent implements OnInit {
  vacancy!: Vacancy;

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialogRef: MatDialogRef<VacancyDeleteDialogComponent>,
  ) {
    this.vacancy = data.vacancy;
  }

  ngOnInit(): void {
  }

}
