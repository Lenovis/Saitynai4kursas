import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {
  events: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    const token = localStorage.getItem('jwt');
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
