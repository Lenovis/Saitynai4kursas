import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html'
})
export class EventComponent {
  invalidEvent: boolean;

  constructor(private router: Router, private http: HttpClient) { }

  public event = (form: NgForm) => {
    const credentials = JSON.stringify(form.value);
    this.http.post('http://localhost:5000/api/event', credentials, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(response => {
      // const token = (response as any).token;
      // localStorage.setItem('jwt', token);
      this.invalidEvent = false;
      this.router.navigate(['/']);
    }, err => {
      this.invalidEvent = true;
    });
  }

}
