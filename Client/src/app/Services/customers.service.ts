import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Config } from '../Configurations/Config';

@Injectable({
  providedIn: 'root'
})
export class CustomersService {

  public Customers : any;
  client : HttpClient;

  constructor(private httpclient: HttpClient) {
    this.client = httpclient;
  }

  GetUsers(): any {
    this.Customers = this.client
      .get(Config.API_URL + '/customers')
      .subscribe({
        next: response => {
          this.Customers = response;
          error: (error: any) => {
            console.log(error);
          };
          complete: () => {
            console.log("Completed")
          }
        }
      })
  }
}
