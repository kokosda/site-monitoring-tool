import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt'

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private http: HttpClient;
  private jwtHelper: JwtHelperService;

  constructor(http: HttpClient) { 
    this.http = http;
    this.jwtHelper = new JwtHelperService();
  }

  post(credentials: any) {    
    let headers = new HttpHeaders({'Content-Type': 'application/json; charset=utf-8' });
    return this.http.post('/api/login', credentials, { headers: headers })
      .pipe(map(response => {
        this.logIn(response);
        return response;
      }));
  }

  logIn(response: any) {
    let token = response.token;
    console.log("TOKEN", response);
    localStorage.setItem("jwt", token);
  }

  logOut() {
    localStorage.removeItem("jwt");
  }

  getToken() {
    let result = localStorage.getItem("jwt");
    return result;
  }

  isLoggedIn() {
    let token = this.getToken();
    
    if (token === null)
      return false;

    var result = !this.jwtHelper.isTokenExpired(token);   
    return result;
  }
}