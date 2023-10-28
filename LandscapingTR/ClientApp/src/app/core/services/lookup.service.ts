import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LookupItemModel } from '../models/lookups/lookup-item.model';

const API_URL = 'http://localhost:5028/api/Lookups/';

@Injectable({
  providedIn: 'root'
})
export class LookupService {
  constructor(private http: HttpClient) { }

  getEmployeeTypes(): Observable<LookupItemModel[]> {
    return this.http.get<LookupItemModel[]>(API_URL + 'GetEmployeeTypes');
  }

  getJobTypes(): Observable<LookupItemModel[]> {
    return this.http.get<LookupItemModel[]>(API_URL + 'GetJobTypes');
  }

  getLocationTypes(): Observable<LookupItemModel[]> {
    return this.http.get<LookupItemModel[]>(API_URL + 'GetLocationTypes');
  }
}
