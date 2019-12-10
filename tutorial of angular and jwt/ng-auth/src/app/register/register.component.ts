import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  invalidRegister: boolean;

  constructor(private router: Router, private http: HttpClient) { }

  public register = (form: NgForm) => {
    const credentials = JSON.stringify(form.value);
    this.http.post('http://localhost:5000/api/auth/register', credentials, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(response => {
      const token = (response as any).token;
      localStorage.setItem('jwt', token);
      this.invalidRegister = false;
      this.router.navigate(['/']);
    }, err => {
      this.invalidRegister = true;
    });
  }

}
