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

  getJobById(jobId: number): Observable<JobModel> {
    return this.http.get<JobModel>(API_URL + 'Job?jobId=' + jobId);
  }

  saveNewJob(jobModel: JobModel): Observable<JobModel> {
    return this.http.post<JobModel>(API_URL + 'Job', jobModel, httpOptions);
  }

  updateJob(jobModel: JobModel): Observable<JobModel> {
    return this.http.put<JobModel>(API_URL + 'Job', jobModel, httpOptions);
  }

  
}
