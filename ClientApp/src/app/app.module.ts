import { WebSiteStatusesComponent } from './web-site-statuses/web-site-statuses.component';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { WebSiteStatusesService } from './services/web-site-statuses.service';
import { LoginComponent } from './login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    WebSiteStatusesComponent,
    LoginComponent
  ],
  imports: [
    FormsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'websitestatuses', component: WebSiteStatusesComponent },
      { path: 'login', component: LoginComponent }
    ])
  ],
  bootstrap: [AppComponent],
  providers: [
    WebSiteStatusesService
  ]
})
export class AppModule { }
