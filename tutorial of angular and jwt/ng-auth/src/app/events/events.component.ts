import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { DataService } from '../data.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css'],
})

export class EventsComponent implements OnInit {
  events: any;
  testmessage: string;

  constructor(private http: HttpClient, public router: Router, private data: DataService) { }

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


  edit(a: string) {

    this.data.changeMessage(a);
    this.data.currentMessage.subscribe(message => this.testmessage = message);

    console.log(this.testmessage); // for debuging

    this.router.navigate(['/updateEvent']);
  }

  refresh() {
    window.location.reload();
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
