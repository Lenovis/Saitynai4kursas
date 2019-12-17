import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { DataService } from '../data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.css']
})
export class CalendarComponent implements OnInit {

  public result: any;
  public events: any;
  public eve: any;
  public currentDate: string[];
  public calendarJson: JSON;
  testmessage: string;
  value: Date = new Date();

  constructor(private http: HttpClient, private data: DataService, private router: Router) { }

  onChange(args) {
    this.calendarJson = args.value;
    this.result = JSON.stringify(this.calendarJson);
    this.result = this.result.split('T')[0].replace('"', '');
    this.todaysEvents();
  }

  todaysEvents() {
    this.http.get('http://localhost:5000/api/event', {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(response => {
      this.events = response;
    }, err => {
      console.log(err);
    });
    this.eve = this.events.filter(x => x.EventStartDate == this.result);
  }

  public delete = (form: NgForm) => {
    const params = JSON.stringify(form.value);
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };

    const url = `http://localhost:5000/api/event/${params}`;

    this.http.delete(url, options).subscribe
    (response => {
      this.events = response;
      this.refresh();
    }, err => {
      console.log(err);
    });
  }
  refresh() {
    window.location.reload();
  }

  edit(a: string) {

    this.data.changeMessage(a);
    this.data.currentMessage.subscribe(message => this.testmessage = message);

    this.router.navigate(['/updateEvent']);
  }
  ngOnInit() {
    this.http.get('http://localhost:5000/api/event', {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(response => {
      this.events = response;
      this.eve = this.events.filter(x => x.EventStartDate == this.result);
    }, err => {
      console.log(err);
    });
    this.value.setHours(this.value.getHours() + 2);
    this.result = this.value.toISOString();
    this.result = this.result.split('T')[0].replace('"', '');
  }

}
