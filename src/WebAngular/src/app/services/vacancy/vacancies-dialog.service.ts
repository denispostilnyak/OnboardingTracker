import { VacanciesDialogComponent } from './../../vacancies/vacancies-dialog/vacancies-dialog.component';
import { Injectable, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { Recruiter } from 'src/app/models/recruiter/recruiter';
import { Vacancy } from 'src/app/models/vacancy/vacancy';

@Injectable({ providedIn: 'root' })
export class VacanciesDialogService implements OnDestroy {
    private unsubscribe$ = new Subject<void>();

    public constructor(private dialog: MatDialog) { }

    public openDialog(inputRecruiters: Recruiter[], inputIsCreate: boolean, inputVacancy?: Vacancy): any {
        const dialog = this.dialog.open(VacanciesDialogComponent, {
            data: { recruiters: inputRecruiters, vacancy: inputVacancy, isCreate: inputIsCreate },
            minWidth: 300,
            autoFocus: false,
            panelClass: 'custom-dialog-container',
        });

        return dialog.afterClosed();
    }

    public ngOnDestroy(): void {
        this.unsubscribe$.next();
        this.unsubscribe$.complete();
    }
}
