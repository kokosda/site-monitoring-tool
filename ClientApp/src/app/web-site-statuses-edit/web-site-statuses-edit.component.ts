import { WebSiteStatusesService } from './../services/web-site-statuses.service';
import { WebSiteStatusesComponent } from './../web-site-statuses/web-site-statuses.component';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-web-site-statuses-edit',
  templateUrl: './web-site-statuses-edit.component.html',
  styleUrls: ['./web-site-statuses-edit.component.css']
})
export class WebSiteStatusesEditComponent extends WebSiteStatusesComponent {
  private wss: any = {};

  constructor(webSiteStatusesService: WebSiteStatusesService) { 
    super(webSiteStatusesService);
  }

  ngOnInit() {
    super.ngOnInit();
  }

  add(f) {
    console.log("ADD", f);
  }

  save(wss) {
    console.log("SAVE", wss);
  }

  remove(wss) {
    console.log("REMOVE", wss);
  }
}
