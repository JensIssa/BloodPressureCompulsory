import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {CreateMeasurementModel} from "../models/create-measurement.model";

@Injectable({
  providedIn: 'root'
})
export class MeasurementService {
  private measurementApiUrl = `http://localhost:8080/measurement`;

  constructor(private http: HttpClient) { }

  addMeasurement(measurement: CreateMeasurementModel): Observable<CreateMeasurementModel> {
    return this.http.post<CreateMeasurementModel>(this.measurementApiUrl, measurement);
  }

  isCountryAllowed(country: string): Observable<{ country: string, isAllowed: boolean }> {
    const url = `${this.measurementApiUrl}/IsCountryAllowed?country=${encodeURIComponent(country)}`;
    return this.http.get<{ country: string, isAllowed: boolean }>(url);
  }
}
