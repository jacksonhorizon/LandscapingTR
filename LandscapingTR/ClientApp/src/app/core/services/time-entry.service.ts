import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_URL = 'http://localhost:5028/api/test/';

@Injectable({
  providedIn: 'root'
})
export class TimeEntryService {
  constructor(private http: HttpClient) { }

}
