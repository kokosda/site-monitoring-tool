import { AuthenticationService } from '../services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LogInComponent implements OnInit {
  private logInService: AuthenticationService;
  private router: Router;
  private invalidLogin: boolean;
  private logIn: any = {};

  constructor(logInService: AuthenticationService, router: Router) { 
    this.logInService = logInService;
    this.router = router;
  }

  ngOnInit() {
  }
	
  onSubmit(form: NgForm) {
    let credentials = JSON.stringify(form.value);
    this.logInService.post(credentials)
      .subscribe(response => {
        this.invalidLogin = false;
        this.router.navigate(["/"]);
    }, error => {
        this.invalidLogin = true;
    });
  }
}
