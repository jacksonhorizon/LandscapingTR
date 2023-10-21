import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LandscapingTRLookupsModel } from '../models/landscaping-tr-lookups.model';
import { LookupItemModel } from '../models/lookups/lookup-item.model';

const API_URL = 'http://localhost:5028/api/Lookups/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class LookupService {
  constructor(private http: HttpClient) { }

  getPublicContent(): Observable<any> {
    return this.http.get(API_URL + 'all', { responseType: 'text' });
  }

  getUserBoard(): Observable<any> {
    return this.http.get(API_URL + 'user', { responseType: 'text' });
  }

  getEmployeeTypes(): Observable<LookupItemModel[]> {
    var x = this.http.get<LookupItemModel[]>(API_URL + 'GetEmployeeTypes');
    return x;
  }
}
