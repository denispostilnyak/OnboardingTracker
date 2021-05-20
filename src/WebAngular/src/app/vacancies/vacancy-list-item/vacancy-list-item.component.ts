import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { JobType } from 'src/app/models/common/job-type';
import { Vacancy } from 'src/app/models/vacancy/vacancy';
import { VacancyDeleteDialogService } from 'src/app/services/vacancy/vacancy-delete-dialog.service';

@Component({
  selector: 'app-vacancy-list-item',
  templateUrl: './vacancy-list-item.component.html',
  styleUrls: ['./vacancy-list-item.component.scss']
})
export class VacancyListItemComponent implements OnInit {
  defaultPicture = 'https://bit.ly/2KBf9c0';
  jobTypes = JobType;
  @Input() vacancy!: Vacancy;
  @Output() deleteVacancyEvent: EventEmitter<number> = new EventEmitter();
  @Output() editVacancyEvent: EventEmitter<Vacancy> = new EventEmitter();

  constructor(private vacancyDeleteDialogService: VacancyDeleteDialogService) { }
  ngOnInit(): void {
  }

  deleteVacancy(): void {
    this.vacancyDeleteDialogService.openDialog(this.vacancy)
      .subscribe((confirmed: boolean) => {
        if (confirmed === true) {
          this.deleteVacancyEvent.emit(this.vacancy.id);
        }
      });
  }

  editVacancy(): void {
    this.editVacancyEvent.emit(this.vacancy);
  }
}
