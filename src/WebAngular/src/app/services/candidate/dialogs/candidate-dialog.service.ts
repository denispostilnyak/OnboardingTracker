import { Injectable, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable, Subject } from 'rxjs';
import { Origin } from '../../../models/origin/origin';
import { CandidatesDialogComponent } from '../../../candidates/candidate-dialog/candidate-dialog.component';
import { Candidate } from 'src/app/models/candidate/candidate';
import { DeleteCandidateDialogComponent } from 'src/app/candidates/delete-candidate-dialog/delete-candidate-dialog.component';
@Injectable({ providedIn: 'root' })
export class CandidateDialogService implements OnDestroy {
  public unsubscribe$ = new Subject<void>();

  public constructor(private dialog: MatDialog) {}

  public openCreateCandidateDialog(title:string, origins: Array<Origin>): Observable<Candidate> {
    const dialog = this.dialog.open(CandidatesDialogComponent, {
      data: { title: title, origins: origins, candidate: new Candidate() },
      minWidth: 400,
      autoFocus: false,
      panelClass: 'custom-dialog-container',
    });
    return dialog.afterClosed();
  }
  public openEditCandidateDialog(title: string, origins: Array<Origin>, candidate:Candidate): Observable<Candidate>{
    const dialog = this.dialog.open(CandidatesDialogComponent, {
      data: { title: title, origins: origins, candidate: candidate },
      minWidth: 400,
      autoFocus: false,
      panelClass: 'custom-dialog-container',
    });
    return dialog.afterClosed();
  }
  public openDeleteCandidateDialog(candidate:Candidate):Observable<any>{
    const dialog = this.dialog.open(DeleteCandidateDialogComponent,{
      data: { firstName:candidate.firstName, lastName:candidate.lastName },
      minWidth: 400,
      autoFocus: false
    });
    return dialog.afterClosed();
  }

  public ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
