import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JobModel } from '../models/domain/job.model';

const API_URL = 'http://localhost:5028/api/Jobs/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class JobService {
  constructor(private http: HttpClient) { }


  getAllJobs(): Observable<JobModel[]> {
    return this.http.get<JobModel[]>(API_URL + 'GetAllJobs');
  }

  //saveNewTimeEntry(timeEntryModel: TimeEntryModel): Observable<TimeEntryModel> {
  //  return this.http.post<TimeEntryModel>(API_URL + 'SaveTimeEntry', timeEntryModel, httpOptions);
  //}

  //updateTimeEntry(timeEntryModel: TimeEntryModel): Observable<TimeEntryModel> {
  //  return this.http.put<TimeEntryModel>(API_URL + 'SaveTimeEntry', timeEntryModel, httpOptions);
  //}

  //saveNewTimeEntries(timeEntryModel: TimeEntryModel[]): Observable<TimeEntryModel[]> {
  //  return this.http.post<TimeEntryModel[]>(API_URL + 'SaveTimeEntries', timeEntryModel, httpOptions);
  //}

  //updateTimeEntries(timeEntryModel: TimeEntryModel[]): Observable<TimeEntryModel[]> {
  //  return this.http.put<TimeEntryModel[]>(API_URL + 'SaveTimeEntries', timeEntryModel, httpOptions);
  //}
}
