import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { DataService } from '../data.service';

@Component({
  selector: 'app-update-event',
  templateUrl: './update-event.component.html',
  styleUrls: ['./update-event.component.css'],
})
export class UpdateEventComponent implements OnInit {

  messageId: string;
  event: any;
  invalidEvent: boolean;

  constructor(private data: DataService, private http: HttpClient, private router: Router) { }

  eventUpdate = (form: NgForm) => {
    const credentials = JSON.stringify(form.value);
    const url = `http://localhost:5000/api/event/${this.messageId}`;
    this.http.patch(url , credentials, {
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
    this.data.currentMessage.subscribe(message => this.messageId = message);
    console.log(this.messageId); // for debuging
    if (this.messageId !== 'Empty data service') {

      const url = `http://localhost:5000/api/event/${this.messageId}`;
      console.log(url);

      this.http.get(url , {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(response => {
      this.event = response;
      console.log(this.event);
    }, err => {
      console.log(err);
    });

    } else {
      console.log('nope, bandyk kazka kita');
      this.invalidEvent = true;
    }
  }

}
