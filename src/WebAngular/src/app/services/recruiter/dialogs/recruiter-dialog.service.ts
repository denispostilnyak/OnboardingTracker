import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { Recruiter } from 'src/app/models/recruiter/recruiter';

import { DeleteRecruiterDialogComponent } from 'src/app/recruiters/delete-recruiter-dialog/delete-recruiter-dialog.component';
import { RecruitersDialogComponent } from 'src/app/recruiters/recruiters-dialog/recruiters-dialog.component';
@Injectable({
  providedIn: 'root'
})
export class RecruiterDialogService {

  public constructor(private dialog: MatDialog) {}

  public openCreateRecruiterDialog(title:string): Observable<Recruiter> {
    const dialog = this.dialog.open(RecruitersDialogComponent, {
      data: { title: title, recruiter:new Recruiter() },
      minWidth: 400,
      autoFocus: false,
      panelClass: 'custom-dialog-container',
    });
    return dialog.afterClosed();
  }
  public openEditRecruiterDialog(title: string, recruiter:Recruiter): Observable<Recruiter>{
    const dialog = this.dialog.open(RecruitersDialogComponent, {
      data: { title: title,recruiter:recruiter },
      minWidth: 400,
      autoFocus: false,
      panelClass: 'custom-dialog-container',
    });
    return dialog.afterClosed();
  }
  public openDeleteRecruiterDialog(recruiter:Recruiter):Observable<any>{
    const dialog = this.dialog.open(DeleteRecruiterDialogComponent,{
      data: recruiter,
      minWidth: 400,
      autoFocus: false
    });
    return dialog.afterClosed();
  }
}
