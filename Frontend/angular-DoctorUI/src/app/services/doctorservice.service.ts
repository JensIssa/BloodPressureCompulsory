import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {GetPatientModel} from "../model/get-patient.model";
import {GetMeasurementModel} from "../model/get-measurement.model";
import {FormControl, ɵFormGroupValue, ɵTypedOrUntyped} from "@angular/forms";

@Injectable({
  providedIn: 'root'
})
export class DoctorserviceService {

  private patientAPIUrl = "http://localhost:8090/patient"

  private measurementApiUrl = `http://localhost:8080/measurement`;

  constructor(private http: HttpClient) {
  }

  getPatients(): Observable<GetPatientModel[]> {
    return this.http.get<GetPatientModel[]>(this.patientAPIUrl);
  }

  deletePatient(ssn: string): Observable<any> {
    const url = `${this.patientAPIUrl}/DeletePatient?ssn=${encodeURIComponent(ssn)}`;
    return this.http.delete(url);
  }

  getMeasurementsForPatient(ssn: string): Observable<GetMeasurementModel[]> {
    const url = `${this.measurementApiUrl}/GetByPatientSSN/${encodeURIComponent(ssn)}`;
    return this.http.get<GetMeasurementModel[]>(url);
  }

  addPatient(patient: ɵTypedOrUntyped<{
    name: FormControl<string | null>;
    email: FormControl<string | null>;
    ssn: FormControl<string | null>
  }, ɵFormGroupValue<{
    name: FormControl<string | null>;
    email: FormControl<string | null>;
    ssn: FormControl<string | null>
  }>, any>): Observable<any> {
    const url = `${this.patientAPIUrl}/AddPatient`;
    return this.http.post(url, patient);
  }
}

