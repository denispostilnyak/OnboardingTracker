import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class HttpInternalService {
  private baseUrl = environment.apiUrl;
  private headers = new HttpHeaders();

  constructor(private httpClient: HttpClient) { }

  public setHeader(key: string, value: string): void {
    this.headers = this.headers.set(key, value);
  }

  public getHeaders(): HttpHeaders {
    return this.headers;
  }

  public deleteHeader(key: string): void {
    this.headers = this.headers.delete(key);
  }

  public getRequest<T>(url: string, httpParams?: any): Observable<T> {
    return this.httpClient.get<T>(this.buildUrl(url), { headers: this.getHeaders(), params: httpParams });
  }

  public postRequest<T>(url: string, object: object): Observable<T> {
    return this.httpClient.post<T>(this.buildUrl(url), object, { headers: this.getHeaders() });
  }

  public putRequest<T>(url: string, object: object): Observable<T> {
    return this.httpClient.put<T>(this.buildUrl(url), object, { headers: this.getHeaders() });
  }

  public deleteRequest<T>(url: string, httpParams?: any): Observable<T> {
    return this.httpClient.delete<T>(this.buildUrl(url), { headers: this.getHeaders(), params: httpParams });
  }

  public buildUrl(url: string): string {
    if (url.startsWith('http://') || url.startsWith('https://')) {
      return url;
    }
    return this.baseUrl + url;
  }
}
