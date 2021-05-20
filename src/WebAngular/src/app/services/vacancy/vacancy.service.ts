import { Vacancy } from './../../models/vacancy/vacancy';
import { Response } from '../../models/common/response';
import { Injectable } from '@angular/core';
import { HttpInternalService } from '../http-internal/http-internal.service';
import { Observable } from 'rxjs/internal/Observable';
import { Skill } from '../../models/skill/skill';

@Injectable({
  providedIn: 'root'
})
export class VacancyService {
  private route = '/api/vacancies';

  constructor(private httpService: HttpInternalService) { }

  public getVacancies(): Observable<Response<Vacancy>> {
    return this.httpService.getRequest<Response<Vacancy>>(this.route);
  }

  public getTopVacancies(): Observable<Response<Vacancy>> {
    return this.httpService.getRequest<Response<Vacancy>>(`${this.route}/top`);
  }

  public getVacancyById(id: number): Observable<Vacancy> {
    return this.httpService.getRequest<Vacancy>(`${this.route}`, { id });
  }

  public createVacancy(vacancy: FormData): Observable<Vacancy> {
    return this.httpService.postRequest<Vacancy>(`${this.route}`, vacancy);
  }

  public updateVacancy(vacancy: FormData): Observable<Vacancy> {
    return this.httpService.putRequest<Vacancy>(`${this.route}`, vacancy);
  }

  public addSkillsToVacancy(vacancy: Vacancy): Observable<Skill[]> {
    return this.httpService.putRequest<Skill[]>(`${this.route}/assign/skills`, vacancy);
  }

  public deleteVacancy(id: number): Observable<Vacancy> {
    return this.httpService.deleteRequest<Vacancy>(`${this.route}/${id}`);
  }
}
