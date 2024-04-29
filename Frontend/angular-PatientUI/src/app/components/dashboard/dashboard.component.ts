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
export class DashboardComponent implements OnInit {
  measurement: CreateMeasurementModel = new CreateMeasurementModel(0, 0, '');
  isVisible = false;
  country: string = '';  // User-selected country

  constructor(private measurementService: MeasurementService) {}

  ngOnInit(): void {
  }

  checkCountryAllowed(selectedCountry: string): void {
    if (!selectedCountry) {
      this.isVisible = false;
      return; // Do not check if no country is selected
    }
    this.measurementService.isCountryAllowed(selectedCountry).subscribe({
      next: ({ isAllowed }) => {
        this.isVisible = isAllowed;
      },
      error: (error) => {
        console.error('Error checking if country is allowed:', error);
        this.isVisible = false; // Default to false on error
      }
    });
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
