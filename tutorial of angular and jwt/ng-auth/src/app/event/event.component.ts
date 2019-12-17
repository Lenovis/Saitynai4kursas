import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html'
})
export class EventComponent implements OnInit {
  invalidEvent: boolean;
  value: Date = new Date();
  // tslint:disable-next-line: variable-name
  _event: any;
  todaysDate: string;

  constructor(private router: Router, private http: HttpClient) { }

  public event = (form: NgForm) => {
    const credentials = JSON.stringify(form.value);
    this.http.post('http://localhost:5000/api/event', credentials, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(response => {
      this.invalidEvent = false;
      this.router.navigate(['/events']);
    }, err => {
      this.invalidEvent = true;
    });
  }

  ngOnInit() {
    this.value.setHours(this.value.getHours() + 2);
    this.todaysDate = this.value.toISOString();
    this.todaysDate = this.todaysDate.split('T')[0].replace('"', '');

    this.http.get('http://localhost:5000/api/event', {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(response => {
      this._event = response;
    }, err => {
      console.log(err);
    });
  }

}
