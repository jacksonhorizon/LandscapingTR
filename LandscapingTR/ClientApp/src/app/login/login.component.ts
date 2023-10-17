import { Component, OnInit } from '@angular/core';
import { LoginModel } from '../core/models/company-resources/login.model';
import { LoginService } from '../core/services/login.service';

@Component({
  selector: 'app-login-component',
  styleUrls: ['./login.component.css'],
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
  form: any = {
    username: null,
    password: null
  };
  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';
  roles: string[] = [];
  user: string = '';

  constructor(private loginService: LoginService) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    const { username, password } = this.form;
    const loginModel = new LoginModel();
    loginModel.username = username;
    loginModel.password = password;

    this.loginService.login(loginModel).subscribe({
      next: data => {

        this.user = username;
        this.isLoginFailed = false;
        this.isLoggedIn = true;
        this.reloadPage();
      },
      error: err => {
        this.errorMessage = err.error.message;
        this.isLoginFailed = true;
      }
    });
  }

  reloadPage(): void {
    window.location.reload();
  }
}
