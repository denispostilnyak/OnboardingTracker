import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Origin } from 'src/app/models/origin/origin';
import { HttpInternalService } from '../http-internal/http-internal.service';
import { Response } from '../../models/common/response';
@Injectable({
  providedIn: 'root'
})
export class OriginService {
  private route = "/api/origins";
  constructor(private http: HttpInternalService) { }
  GetOrigins(): Observable<Response<Origin>> {
    return this.http.getRequest<Response<Origin>>(this.route);
  }
}
