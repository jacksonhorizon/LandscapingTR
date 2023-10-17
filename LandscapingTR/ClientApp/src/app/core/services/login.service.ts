import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginModel } from '../models/company-resources/login.model';

const AUTH_API = 'http://localhost:5028/api/Employees/';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(private http: HttpClient) { }

  login(loginModel: LoginModel): Observable<any> {
    return this.http.put(AUTH_API + 'Login', loginModel, httpOptions);
  }
}
