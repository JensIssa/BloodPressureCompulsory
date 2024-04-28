import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {GetPatientModel} from "../model/get-patient.model";
import {GetMeasurementModel} from "../model/get-measurement.model";
import {UpdateMeasurementModel} from "../model/update-measurement.model";

@Injectable({
  providedIn: 'root'
})
export class MeasurementService {


  private measurementApiUrl = `http://localhost:8080/measurement`;

  constructor(private http: HttpClient) {
  }


  markAsSeen(measurementID: number): Observable<any> {
    const url = `${this.measurementApiUrl}/MarkAsSeen/${(measurementID)}`;
    return this.http.put(url, measurementID);
  }

  updateMeasurement(measurementID: number, updateMeasurementModel: UpdateMeasurementModel): Observable<any> {
    const url = `${this.measurementApiUrl}/${measurementID}`;
    return this.http.put(url, updateMeasurementModel);
  }

}

