import { Injectable } from '@angular/core';
import { Data } from './data';
import { DATA } from './mock-data';
import { Headers, Http } from '@angular/http';
import { SecurityService } from './security.service'

import 'rxjs/add/operator/toPromise';

@Injectable()
export class DataService {

  // private dataUrl = 'http://localhost:5001/api/temperaturehumidity';  // URL to web api
  private dataUrl = 'https://redfox-app-temperatureapp.azurewebsites.net/api/temperaturehumidity';  // URL to web api

  constructor(private http: Http, private securityService: SecurityService) { }
  getData(): Promise<Data[]> {
    var headers = this.getHeaders();
    var options = { headers: headers };
    return this.http.get(this.dataUrl, options)
      .toPromise()
      .then(response => response.json().map((item) => this.CreateData(item)))
      .catch(this.handleError);
  }

  private CreateData(item): Data{
          var data : Data = new Data();
          data.humidity = item.humidity as number;
          data.temperature = item.temperature as number;
          data.date = new Date(item.logDateTime);
          return data;;
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }

  private getHeaders(): Headers {
      var headers = new Headers();

      var token = this.securityService.GetToken();

      if (token !== "") {
          headers.append('Authorization', 'Bearer ' + token);
      }

      return headers;
  }
}
