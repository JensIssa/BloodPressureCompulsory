import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private measurementApiUrl = `http://localhost:8080/measurement`;
  private patientApiUrl = `http://localhost:8090/patient`;

  constructor(private http: HttpClient) { }

  getPatient(ssn: string): Observable<any> {
    return this.http.get(`${this.patientApiUrl}/GetPatient?ssn=${ssn}`)
  }

  getMeasurementsBySSN(ssn: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.measurementApiUrl}/GetByPatientSSN/${ssn}`);
  }

  addMeasurement(measurement: any): Observable<any> {
    return this.http.post(`${this.measurementApiUrl}`, measurement);
  }

  updateMeasurement(id: number, measurement: any): Observable<any> {
    return this.http.put(`${this.measurementApiUrl}/${id}`, measurement);
  }

  deleteMeasurement(id: number): Observable<any> {
    return this.http.delete(`${this.measurementApiUrl}/${id}`);
  }
}
