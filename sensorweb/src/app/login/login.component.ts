import { Component, OnInit } from '@angular/core';
import { SecurityService } from '../security.service'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private securityService: SecurityService) { }

  public showLogin: boolean = false;
  public showLogout: boolean = false;

  ngOnInit() {
    console.log("ngOnInit _securityService.AuthorizedCallback");

    this.updateShowButtons();
    if (window.location.hash) {
      this.securityService.AuthorizedCallback();
    }
  }

  signin(): void {
    this.securityService.Authorize();
  }

  updateShowButtons(): void{
    this.showLogout = this.securityService.IsLoggedIn()
    this.showLogin = !this.showLogout;
  }

  signout(): void {
    this.securityService.Logoff();
  }
}
