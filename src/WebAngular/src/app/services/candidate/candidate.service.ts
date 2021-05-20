import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Candidate } from 'src/app/models/candidate/candidate';
import { HttpInternalService } from '../http-internal/http-internal.service';
import { Response } from '../../models/common/response';

@Injectable({
  providedIn: 'root',
})
export class CandidateService {
  private route = '/api/candidates';
  constructor(private http: HttpInternalService) {}

  GetCandidates(): Observable<Response<Candidate>> {
    return this.http.getRequest<Response<Candidate>>(this.route);
  }
  CreateCandidate(candidate: FormData): Observable<Candidate> {
    return this.http.postRequest<Candidate>(this.route, candidate);
  }
  UpdateCandidate(candidate: FormData): Observable<Candidate> {
    return this.http.putRequest<Candidate>(this.route, candidate);
  }
  DeleteCandidate(id: number): Observable<Candidate> {
    return this.http.deleteRequest<Candidate>(`${this.route}/${id}`);
  }
}
