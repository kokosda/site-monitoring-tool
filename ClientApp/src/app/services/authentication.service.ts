import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private http;

  constructor(http: HttpClient) { 
    this.http = http;
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
    let token = (<any>response).token;
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
    let result = this.getToken() !== null;
    return result;
  }
}