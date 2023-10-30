import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable } from 'rxjs';
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
    return this.http.get(API_URL + 'GetEmployee?employeeId=' + employeeId).pipe(
      map((employee: EmployeeModel) => this.convertDates(employee))
    );
  }

  getAllEmployees(): Observable<EmployeeModel[]> {
    return this.http.get<EmployeeModel[]>(API_URL + 'GetAllEmployees').pipe(
      map(employees => employees.map(employee => this.convertDates(employee)))
    );
  }

  saveNewEmployee(employeeModel: EmployeeModel): Observable<EmployeeModel> {
    return this.http.post(API_URL + 'Employee', employeeModel, httpOptions);
  }

  updateEmployee(employeeModel: EmployeeModel): Observable<EmployeeModel> {
    return this.http.put(API_URL + 'Employee', employeeModel, httpOptions);
  }

  deleteEmployee(employeeModel: EmployeeModel): Observable<EmployeeModel> {
    const url = `${API_URL}Employee`;

    const options = {
      body: employeeModel, // Send the model as the request body
    };

    return this.http.request<EmployeeModel>('delete', url, options);
  }

  private convertDates(employee: EmployeeModel): EmployeeModel {
    if (employee.createdDate == null) {
      employee.createdDate = new Date();
    }

    employee.createdDate = new Date(employee.createdDate);

    return employee;
  }

  private convertDatesMultipleEmployees(employees: EmployeeModel[]): EmployeeModel[] {

    employees.forEach(employee => {
      if (employee.createdDate == null) {
        employee.createdDate = new Date();
      }

      employee.createdDate = new Date(employee.createdDate);
    });

    return employees;
  }
}
