import { Component, OnInit } from '@angular/core';
import {MeasurementService} from "../../services/Measurement.service";
import {FormsModule} from "@angular/forms";
import {CreateMeasurementModel} from "../../models/create-measurement.model";
import {GeoLocationService} from "../../services/geolocation.service";
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  measurement: CreateMeasurementModel = new CreateMeasurementModel(0,0, '');
  isVisible = false;


  constructor(private measurementService: MeasurementService, private geoLocationService: GeoLocationService) {}

  ngOnInit(): void {
    this.geoLocationService.getUserCountry().subscribe(
      country => {
        if (country === 'Denmark') {
          this.isVisible = true;
        }
      },
      error => {
        console.error('Error retrieving user country:', error);
      }
    );
  }

  addMeasurement(): void {
    if (this.measurement.systolic > 0 && this.measurement.diastolic > 0 && this.measurement.patientSSN) {
      this.measurementService.addMeasurement(this.measurement).subscribe({
        next: () => {
          alert('Measurement added successfully, the doctor will take a look in the near future');
          this.measurement = new CreateMeasurementModel(0, 0, '');
        },
        error: (error) => {
          alert(`Failed to add measurement: ${error.message}`);
        }
      });
    } else {
      alert('Please fill in all fields with valid values');
    }
  }
}
