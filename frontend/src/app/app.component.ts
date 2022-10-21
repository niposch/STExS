import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {WeatherForecastService} from "../services/generated/services/weather-forecast.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit{
  title = 'STExS';

  public temperatures:Array<number> = [];
  constructor(private readonly http: HttpClient,
              private weatherForecastService: WeatherForecastService
              ) {
  }

  ngOnInit(): void {
    this.weatherForecastService.getWeatherForecast$Plain().subscribe( response => {
      this.temperatures = response.map(r => r.temperatureC ?? 0);
      console.table(response);
    });
  }
}
