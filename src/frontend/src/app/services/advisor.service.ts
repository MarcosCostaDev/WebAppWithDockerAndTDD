import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { firstValueFrom, tap } from 'rxjs';
import { Advisor } from 'src/entities/advisor';
import { ApiResponse } from 'src/entities/apiResponse';

@Injectable({
  providedIn: 'root'
})
export class AdvisorService {

  constructor(private http: HttpClient) { }

  get(): Promise<ApiResponse<Array<Advisor>>> {
    return firstValueFrom(this.http.get<ApiResponse<Array<Advisor>>>(`${environment.url}/api/Advisor`));
  }

  getBySin(sin: string): Promise<ApiResponse<Advisor>> {
    return firstValueFrom(this.http.get<ApiResponse<Advisor>>(`${environment.url}/api/Advisor/${sin}`));
  }

  getByName(name: string): Promise<ApiResponse<Array<Advisor>>> {
    return firstValueFrom(this.http.get<ApiResponse<Array<Advisor>>>(`${environment.url}/api/Advisor/?name=${name}`));
  }

  create(advisor: Advisor): Promise<ApiResponse<Advisor>> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');
    return firstValueFrom(this.http.post<ApiResponse<Advisor>>(`${environment.url}/api/Advisor/`, JSON.stringify(advisor), { headers: headers }));
  }

  update(advisor: Advisor): Promise<ApiResponse<Advisor>> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');
    return firstValueFrom(this.http.put<ApiResponse<Advisor>>(`${environment.url}/api/Advisor/${advisor.sin}`, JSON.stringify(advisor), { headers: headers }));
  }

  delete(advisor: Advisor) {
    return firstValueFrom(this.http.delete(`${environment.url}/api/Advisor/${advisor.sin}`));
  }

}
