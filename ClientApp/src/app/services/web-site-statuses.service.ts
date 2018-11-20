import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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

  create(wss) {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    return this.http.post('/api/websitestatuses', wss, { headers: headers })
      .pipe(map(response => response));
  }

  update(wss) {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    return this.http.put('/api/websitestatuses/' + wss.id, wss, { headers: headers })
      .pipe(map(response => response));
  }

  delete(wss) {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    return this.http.delete('/api/websitestatuses/' + wss.id, wss, { headers: headers })
      .pipe(map(response => response));
  }
}
 