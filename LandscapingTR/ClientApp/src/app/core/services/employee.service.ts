import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeModel } from '../models/company-resources/employee.model';

const API_URL = 'http://localhost:5028/api/Employees/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  constructor(private http: HttpClient) { }

  getEmployee(employeeId: number): Observable<EmployeeModel> {
    return this.http.get(API_URL + 'GetEmployee?employeeId=' + employeeId);
  }

  getAllEmployees(): Observable<EmployeeModel[]> {
    return this.http.get<EmployeeModel[]>(API_URL + 'GetAllEmployees');
  }

  saveNewEmployee(employeeModel: EmployeeModel): Observable<EmployeeModel> {
    return this.http.post(API_URL + 'Employee', employeeModel, httpOptions);
  }

  updateEmployee(employeeModel: EmployeeModel): Observable<EmployeeModel> {
    return this.http.put(API_URL + 'Employee', employeeModel, httpOptions);
  }
}
