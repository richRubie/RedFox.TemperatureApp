import { Component, OnInit } from '@angular/core';
import { DataService } from "../data.service";
import { Data } from "../data";
import { BaseChartDirective } from "ng2-charts/ng2-charts";

@Component({
  selector: 'app-graph',
  templateUrl: './graph.component.html',
  styleUrls: ['./graph.component.css']
})
export class GraphComponent implements OnInit {

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.getData();
  }
  public hasData: boolean = false;

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
        time: {
          unit: 'hour',
          displayFormats: {
            hour: 'ha Do MMM'
          }
        }
      }]
    }
  };

  getData(): void {
    this.dataService.getData()
      .then((data) => {
        this.hasData = data.length > 0;
        this.labels = data.map(value => value.date);
        this.datasets = [
          {
            label: "temperature",
            data: data.map(value => value.temperature),
            yAxisID: "left",
            pointRadius: 0,
            spanGaps: true,
            lineTension: 0.5,
            cubicInterpolationMode: 'default'
          },
          {
            label: "humidity",
            data: data.map(value => value.humidity),
            yAxisID: "right",
             pointRadius: 0,
            spanGaps: true,
            lineTension: 0.5
         }
        ];
      });
  }

}
