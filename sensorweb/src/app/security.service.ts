import { Injectable } from '@angular/core';
import { OidcClient } from "oidc-client";

@Injectable()
export class SecurityService {

    private storage: any;
    private client: OidcClient;

    constructor() {
        var config = {
            authority: "http://localhost:5000",
            client_id: "sensorweb",
            redirect_uri: "http://localhost:4200",
            response_type: "id_token token",
            scope:"openid profile temperatureApi",
            post_logout_redirect_uri: "http://localhost:4200",
            loadUserInfo: true,
            filterProtocolClaims: true
        };
        this.client = new OidcClient(config);
    }

    public GetToken(): any {
        return this.signinResponse.access_token;
    }

    public ResetAuthorizationData() {
        this.signinResponse = null;
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

    private signinResponse;
    public AuthorizedCallback() {
        let that = this;
        console.log("BEGIN AuthorizedCallback, no auth data");
        this.ResetAuthorizationData();

        this.client.processSigninResponse().then(function(response) {
            that.signinResponse = response;
            console.log("signin response", that.signinResponse);
        }).catch(function(err) {
            console.log(err);
        });
    }

    public Logoff() {
        console.log("BEGIN Authorize, no auth data");
        let that = this;

        this.client.createSignoutRequest({ id_token_hint: this.signinResponse && this.signinResponse.id_token }).then(function(req) {
            console.log("signout request", req, "<a href='" + req.url + "'>go signout</a>");
            that.ResetAuthorizationData();
            window.location.href = req.url;
        });
    }
}
