import { Component } from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatDialogModule, MatDialogRef} from "@angular/material/dialog";
import {DoctorserviceService} from "../../services/doctorservice.service";
import {MatFormField, MatFormFieldModule, MatLabel} from "@angular/material/form-field";
import {CommonModule} from "@angular/common";
import {MatInputModule} from "@angular/material/input";
import {MatButtonModule} from "@angular/material/button";

@Component({
  selector: 'app-patient-form-dialog-component',
  standalone: true,
  imports: [
    MatLabel,
    MatFormField,
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './patient-form-dialog-component.component.html',
  styleUrl: './patient-form-dialog-component.component.scss'
})
export class PatientFormDialogComponentComponent {
  patientForm = new FormGroup({
    ssn: new FormControl('', [Validators.required, Validators.minLength(9)]),
    name: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email])
  });

  constructor(
    public dialogRef: MatDialogRef<PatientFormDialogComponentComponent>,
    private doctorService: DoctorserviceService
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  addPatient(): void {
    if (this.patientForm.valid) {
      this.doctorService.addPatient(this.patientForm.value).subscribe(() => {
        this.dialogRef.close();
        alert('Patient added successfully!');
      }, error => {
        console.error('Failed to add patient', error);
      });
    }
  }
}
