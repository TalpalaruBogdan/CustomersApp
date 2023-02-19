import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Config } from './Configurations/Config';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Client';
  customers: any;

  constructor(private client: HttpClient) {

  }

  ngOnInit(): void {
    this.customers = this.client
    .get(Config.API_URL + '/customers')
    .subscribe({
        next: response => {
          this.customers = response;
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
