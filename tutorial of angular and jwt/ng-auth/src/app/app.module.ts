import { AuthGuard } from './guards/auth-guard.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { CalendarModule } from '@syncfusion/ej2-angular-calendars';

import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { CustomersComponent } from './customers/customers.component';
import { AppComponent } from './app.component';
import { RegisterComponent } from './register/register.component';
import { EventComponent } from './event/event.component';
import { EventsComponent } from './events/events.component';
import { NavbarComponent } from './navbar/navbar.component';
import { UpdateEventComponent } from './update-event/update-event.component';
import { DataService } from './data.service';
import { CalendarComponent } from './calendar/calendar.component';

export function tokenGetter() {
  return localStorage.getItem('jwt');
}

@NgModule({
  declarations: [
    HomeComponent,
    LoginComponent,
    CustomersComponent,
    AppComponent,
    RegisterComponent,
    EventComponent,
    EventsComponent,
    NavbarComponent,
    UpdateEventComponent,
    CalendarComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    CalendarModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'login', component: LoginComponent },
      { path: 'customers', component: CustomersComponent, canActivate: [AuthGuard] },
      { path: 'register' , component: RegisterComponent },
      { path: 'event' , component: EventComponent, canActivate: [AuthGuard]},
      { path: 'events' , component: EventsComponent, canActivate: [AuthGuard]},
      { path: 'updateEvent' , component: UpdateEventComponent, canActivate: [AuthGuard]},
      { path: 'calendar' , component: CalendarComponent, canActivate: [AuthGuard]},
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: []
      }
    })
  ],
  providers: [AuthGuard, DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
