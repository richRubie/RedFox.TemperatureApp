import { Component } from '@angular/core';
import { UserManager, OidcClient, Log } from "oidc-client";
import { DataService } from "./data.service";
import { Data } from "./data";
import { BaseChartDirective } from "ng2-charts/ng2-charts";
import { SecurityService } from "./security.service";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})

export class AppComponent {
    constructor(private dataService: DataService, private securityService: SecurityService) { }
    title = 'app works!';
    data: Data[] = [];

    ngOnInit() {
        console.log("ngOnInit _securityService.AuthorizedCallback");

        if (window.location.hash) {
            this.securityService.AuthorizedCallback();
        }
    }

    signin(): void {
        this.securityService.Authorize();
    }
    signout(): void {
        this.securityService.Logoff();
    }
    getData(): void {
        this.dataService.getData()
            .then((data) => {
                this.data = data;
                this.labels = data.map(value => value.date);
                this.datasets = [
                    {
                        label: "temperature",
                        data: data.map(value => value.temperature),
                        yAxisID: "left"
                    },
                    {
                        label: "humidity",
                        data: data.map(value => value.humidity),
                        yAxisID: "right"
                    }
                ];
            });
    }

    temperature: number[] = [];
    humidity: number[] = [];
    datasets: Array<any> = [];

    labels: Array<Date> = [];

    options = {
        scales: {
            yAxes: [{
                id: "left",
                position: "left",
                ticks: {
                    beginAtZero: false
                }
            },
                {
                    id: "right",
                    position: "right",
                    ticks: {
                        beginAtZero: false
                    }
                }],
            xAxes: [{
                type: 'time',
            }]
        }
    };
}
