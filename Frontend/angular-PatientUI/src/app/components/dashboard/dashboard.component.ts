import { Component } from '@angular/core';
import {MeasurementService} from "../../services/Measurement.service";
import {FormsModule} from "@angular/forms";
import {CreateMeasurementModel} from "../../models/create-measurement.model";

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  measurement: CreateMeasurementModel = new CreateMeasurementModel(0,0, '');

  constructor(private measurementService: MeasurementService) {}

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
