import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
  <navbar></navbar>
  <div style="background-color: #fafafa;">
  <router-outlet></router-outlet>
  </div>
  `,
  styles: []
})
export class AppComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
