import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Vacancy } from '../models/vacancy/vacancy';
import { VacancyService } from '../services/vacancy/vacancy.service';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { RecruiterService } from '../services/recruiter/recruiter.service';
import { Recruiter } from '../models/recruiter/recruiter';
import { NotificationService } from '../services/notification/notification.service';
import { VacanciesDialogService } from '../services/vacancy/vacancies-dialog.service';
@Component({
  selector: 'app-vacancies',
  templateUrl: './vacancies.component.html',
  styleUrls: ['./vacancies.component.scss'],
})
export class VacanciesComponent implements OnInit, OnDestroy {
  vacancies!: Vacancy[];
  defaultPicture = 'https://bit.ly/2KBf9c0';
  faPlus = faPlus;
  recruiters!: Recruiter[];

  private unsubscribe$ = new Subject<void>();
  constructor(
    private vacancyService: VacancyService,
    private recruiterService: RecruiterService,
    private notificationService : NotificationService,
    private vacanciesDialogService: VacanciesDialogService,
    private cdRef: ChangeDetectorRef
  ) { }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  ngOnInit(): void {
    this.notificationService.registerCallbackFor("/vacancies",this,this.openAddVacancyDialog);
    this.getVacancies();
    this.getRecruiters();
  }

  openAddVacancyDialog(): void {
    this.vacanciesDialogService.openDialog(this.recruiters, true).subscribe((vacancy: Vacancy) => {
      if (vacancy) {
        vacancy.vacancyPictureUrl = vacancy.vacancyPictureUrl ?? this.defaultPicture;
        this.vacancies.push(vacancy);
        this.cdRef.markForCheck();
      }
    });
  }

  getRecruiters(): void {
    this.recruiterService.GetRecruiters()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((res: any) => {
        this.recruiters = res.items;
      });
  }

  getVacancies(): void {
    this.vacancyService
      .getVacancies()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((res: any) => {
        this.vacancies = res.items.map((item: Vacancy) => {
          item.vacancyPictureUrl = item.vacancyPictureUrl ?? this.defaultPicture;
          return item;
        });
        this.cdRef.markForCheck();
      });
  }

  public onDelete(id: number): void {
    this.vacancyService.deleteVacancy(id)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe((vacancyDeleted: Vacancy) => {
        this.vacancies = this.vacancies.filter(vacancy => vacancy.id !== vacancyDeleted.id);
        this.cdRef.markForCheck();
      });
  }

  public onEdit(vacancy: Vacancy): void {
    this.vacanciesDialogService.openDialog(this.recruiters, false, vacancy)
      .subscribe((dialogVacancy: Vacancy) => {
        if (dialogVacancy) {
          this.vacancies = this.vacancies.map((item: Vacancy) => {
            if (dialogVacancy.id === item.id) {
              item = dialogVacancy;
              item.vacancyPictureUrl = item.vacancyPictureUrl ?? this.defaultPicture;
            }

            return item;
          });
          this.cdRef.markForCheck();
        }
      });
  }
}
