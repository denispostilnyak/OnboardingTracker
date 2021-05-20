import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Recruiter } from '../../models/recruiter/recruiter';
import { HttpInternalService } from '../http-internal/http-internal.service';
import { Response } from '../../models/common/response'
@Injectable({
  providedIn: 'root',
})
export class RecruiterService {
  private route = "/api/recruiters";
  private topRecruitersRoute = "/api/recruiters/top?count=3";
  constructor(private http: HttpInternalService) { }

  GetTopRecruiters():Observable<Response<Recruiter>>{
    return this.http.getRequest<Response<Recruiter>>(this.topRecruitersRoute);
  }

  GetRecruiters(): Observable<Response<Recruiter>> {
    return this.http.getRequest<Response<Recruiter>>(this.route);
  }
  CreateRecruiter(recruiter: FormData): Observable<Recruiter> {
    return this.http.postRequest<Recruiter>(this.route, recruiter);
  }
  UpdateRecruiter(recruiter: FormData): Observable<Recruiter> {
    return this.http.putRequest<Recruiter>(this.route, recruiter);
  }
  DeleteRecruiter(id: number): Observable<Recruiter> {
    return this.http.deleteRequest<Recruiter>(`${this.route}/${id}`);
  }
}
