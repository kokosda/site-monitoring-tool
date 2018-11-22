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
  private isProcessing: boolean;
  private message: string;

  constructor(webSiteStatusesService: WebSiteStatusesService) { 
    super(webSiteStatusesService);
    this.isProcessing = false;
  }

  ngOnInit() {
    super.ngOnInit();
  }

  create(f) {
    var wss = JSON.stringify(f.value);
    this.isProcessing = true;
    this.webSiteStatusesService.create(wss)
      .subscribe(response => {
        this.isProcessing = false;
        this.message = "Done!"
        this.loadAll();
      }, error => {
        this.isProcessing = false;
        this.message = "Error: " + JSON.stringify(error);
      });
  }

  update(wss) {
    this.isProcessing = true;
    this.webSiteStatusesService.update(wss)
      .subscribe(response => {
        this.isProcessing = false;
        this.message = "Done!"
        this.loadAll();
      }, error => {
        this.isProcessing = false;
        this.message = "Error: " + JSON.stringify(error);
      });
  }

  delete(wss) {
    this.isProcessing = true;
    this.webSiteStatusesService.delete(wss)
      .subscribe(response => {
        this.isProcessing = false;
        this.message = "Done!"
        this.loadAll();
      }, error => {
        this.isProcessing = false;
        this.message = "Error: " + JSON.stringify(error);
      });
  }
}
