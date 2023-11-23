import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TimeEntryModel } from '../models/time/time-entry.model';

const API_URL = 'http://localhost:5028/api/TimeEntries/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class TimeEntryService {
  constructor(private http: HttpClient) { }

  getAllSubmittedTimeEntries(): Observable<TimeEntryModel[]> {
    return this.http.get<TimeEntryModel[]>(API_URL + 'AllSubmittedTimeEntriesWithinDates');
  }

  getAllTimeEntriesByEmployeeId(employeeId: number): Observable<TimeEntryModel[]> {
    return this.http.get<TimeEntryModel[]>(API_URL + 'AllTimeEntriesByEmployeeId');
  }

  saveNewTimeEntry(timeEntryModel: TimeEntryModel): Observable<TimeEntryModel> {
    return this.http.post<TimeEntryModel>(API_URL + 'SaveTimeEntry', timeEntryModel, httpOptions);
  }

  updateTimeEntry(timeEntryModel: TimeEntryModel): Observable<TimeEntryModel> {
    return this.http.put<TimeEntryModel>(API_URL + 'SaveTimeEntry', timeEntryModel, httpOptions);
  }

  saveNewTimeEntries(timeEntryModel: TimeEntryModel[]): Observable<TimeEntryModel[]> {
    return this.http.post<TimeEntryModel[]>(API_URL + 'SaveTimeEntries', timeEntryModel, httpOptions);
  }

  updateTimeEntries(timeEntryModel: TimeEntryModel[]): Observable<TimeEntryModel[]> {
    return this.http.put<TimeEntryModel[]>(API_URL + 'SaveTimeEntries', timeEntryModel, httpOptions);
  }
}
