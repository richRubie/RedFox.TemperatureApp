import { Component } from '@angular/core';
import { UserManager } from "oidc-client";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'app works!';

  config = {
      authority: "http://localhost:5000",
      client_id: "sensorweb",
      redirect_uri: "http://localhost:4200",
      response_type: "id_token token",
      scope:"openid profile",
      post_logout_redirect_uri : "http://localhost:4200",
  };
  mgr = new UserManager(this.config);
    login(): void {
    this.mgr.signinRedirect();
  }

  logout(): void {
    this.mgr.signoutRedirect();
  }
}
