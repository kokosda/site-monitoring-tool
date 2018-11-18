import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt'

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LogInComponent } from './login/login.component';
import { WebSiteStatusesComponent } from './web-site-statuses/web-site-statuses.component';

import { WebSiteStatusesService } from './services/web-site-statuses.service';
import { AuthenticationService } from './services/authentication.service';
import { AuthenticationGuardService } from './services/authentication-guard.service';
import { WebSiteStatusesEditComponent } from './web-site-statuses-edit/web-site-statuses-edit.component';

export function tokenGetter() {
  return localStorage.getItem('jwt');
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    WebSiteStatusesComponent,
    WebSiteStatusesEditComponent,
    LogInComponent
  ],
  imports: [
    FormsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost']
      }
    }),
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'websitestatuses', component: WebSiteStatusesComponent },
      { path: 'websitestatuses/edit', component: WebSiteStatusesEditComponent, canActivate: [AuthenticationGuardService] },
      { path: 'login', component: LogInComponent }
    ])
  ],
  bootstrap: [AppComponent],
  providers: [
    WebSiteStatusesService,
    AuthenticationService,
    AuthenticationGuardService
  ]
})
export class AppModule { }
