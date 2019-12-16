import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})

export class EventsComponent implements OnInit {
  events: any;

  constructor(private http: HttpClient) { }

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
    }, err => {
      console.log(err);
    });
  }

  ngOnInit() {
    this.http.get('http://localhost:5000/api/event', {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(response => {
      this.events = response;
    }, err => {
      console.log(err);
    });
  }

}
