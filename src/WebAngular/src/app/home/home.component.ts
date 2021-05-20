import { Component, OnDestroy, OnInit } from '@angular/core';
import { Response } from '../models/common/response';
import { Recruiter } from '../models/recruiter/recruiter';
import { VacancyService } from '../services/vacancy/vacancy.service';
import { RecruiterService } from '../services/recruiter/recruiter.service';
import { forkJoin, Observable } from 'rxjs';
import { Vacancy } from '../models/vacancy/vacancy';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  vacanciesAndRecruiters$!: Observable<{ vacanciesRequest: Response<Vacancy>, recruiterRequest: Response<Recruiter> }>;

  constructor(
    private vacancyService: VacancyService,
    private recruiterService: RecruiterService
  ) { }

  ngOnInit(): void {
    this.getTopVacanciesAndRecruiters();
  }

  getTopVacanciesAndRecruiters(): void {
    this.vacanciesAndRecruiters$ = forkJoin({
      vacanciesRequest: this.vacancyService.getTopVacancies(),
      recruiterRequest: this.recruiterService.GetTopRecruiters()
    });
  }
}
