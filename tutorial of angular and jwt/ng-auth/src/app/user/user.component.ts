import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  private users: any;
  private test: any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get('http://localhost:5000/api/user', {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(response => {
      this.users = response;
      this.test = localStorage.getItem('login');
      this.test = this.test as string;
      this.users = this.users.filter(x => x.userLogIn == this.test);
      console.log(this.users);
    }, err => {
      console.log(err);
    });
  }
}
