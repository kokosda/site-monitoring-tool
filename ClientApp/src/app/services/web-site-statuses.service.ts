import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class WebSiteStatusesService {
  private http;

  constructor(http: HttpClient) { 
    this.http = http;
  }

  get() {
    return this.http.get('/api/websitestatuses')
      .pipe(map(response => response));
  }
}
 