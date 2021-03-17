import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { switchMap, catchError } from 'rxjs/operators';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: Observable<WeatherForecast[]>;
  public forecastSubject: Subject<undefined>;
  newForecastForm: FormGroup;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.forecastSubject = new BehaviorSubject<undefined>(null);
    this.forecasts = this.forecastSubject.pipe(
      switchMap(x =>
        http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').pipe(
          catchError(error => {
            console.log(error);
            return [];
          }))
      ));

    this.newForecastForm = new FormGroup({
      date: new FormControl(),
      temperatureC: new FormControl(),
      summary: new FormControl(),
    });
  }

  delete(id: number) {
    this.http.delete(this.baseUrl + 'weatherforecast' + `/${id}`).subscribe(
      () => this.forecastSubject.next(null));
  }

  create() {
    this.http.post<WeatherForecast>(this.baseUrl + 'weatherforecast', this.newForecastForm.value).subscribe(
      () => {
        this.forecastSubject.next(null);
        this.newForecastForm.reset();
      });
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
