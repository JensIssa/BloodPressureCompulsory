import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class GeoLocationService {
  constructor(private http: HttpClient) {}

  getUserCountry(): Observable<string> {
    return this.http.get<{ country_name: string }>('https://ipapi.co/json/')
      .pipe(map(response => response.country_name));
  }


}
