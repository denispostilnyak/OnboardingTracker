import { Injectable, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { Vacancy } from 'src/app/models/vacancy/vacancy';
import { VacancyDeleteDialogComponent } from 'src/app/vacancies/vacancy-delete-dialog/vacancy-delete-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class VacancyDeleteDialogService implements OnDestroy {
  private unsubscribe$ = new Subject<void>();

  public constructor(private dialog: MatDialog) { }

  public openDialog(inputVacancy: Vacancy): any {
    const dialog = this.dialog.open(VacancyDeleteDialogComponent, {
      data: { vacancy: inputVacancy },
      minWidth: 300,
      autoFocus: false
    });

    return dialog.afterClosed();
  }

  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
