import { Injectable } from '@angular/core';
import { OidcClient } from "oidc-client";

@Injectable()
export class SecurityService {

    private storage: any;
    private client: OidcClient;

    constructor() {
        var config = {
            // authority: "http://localhost:5000",
            authority: "https://redfox-app-identityserver.azurewebsites.net",
            client_id: "sensorweb",
            redirect_uri: "http://localhost:4200",
            response_type: "id_token token",
            scope:"openid profile temperatureApi",
            post_logout_redirect_uri: "http://localhost:4200",
            loadUserInfo: true,
            filterProtocolClaims: true,

        };
        this.client = new OidcClient(config);
    }

    public GetToken(): string {
        return localStorage.getItem('auth_token');
    }

    public ResetAuthorizationData() {
        localStorage.removeItem('auth_token');
    }

    public IsLoggedIn(): boolean {
        var token: string = this. GetToken();
        return token != undefined && token !== null;
    }

    public Authorize() {
        this.ResetAuthorizationData();
        console.log("BEGIN Authorize, no auth data");

        this.client.createSigninRequest().then(function(req) {
            console.log("signin request", req, "<a href='" + req.url + "'>go signin</a>");
            window.location.href = req.url;
        }).catch(function(err) {
            console.log(err);
        });
    }

    public AuthorizedCallback() {
        let that = this;
        console.log("BEGIN AuthorizedCallback, no auth data");
        this.client.processSigninResponse().then(function(response) {
            localStorage.setItem('auth_token', response.access_token);
            history.pushState("", document.title, window.location.pathname);
        }).catch(function(err) {
            console.log(err);
        });
    }

    public Logoff() {
        console.log("BEGIN Authorize, no auth data");
        let that = this;

        this.client.createSignoutRequest({ id_token_hint: this.GetToken() }).then(function(req) {
            console.log("signout request", req, "<a href='" + req.url + "'>go signout</a>");
            that.ResetAuthorizationData();
            window.location.href = req.url;
        });
    }
}
