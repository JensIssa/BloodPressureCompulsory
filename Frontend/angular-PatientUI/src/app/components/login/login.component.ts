import { Component } from '@angular/core';
import {PatientService} from "../../services/patient.service";
import {Router} from "@angular/router";
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  ssn: string = '';

  constructor(private patientService: PatientService, private router: Router) {}

  onSubmit(): void {
    this.patientService.getPatient(this.ssn).subscribe({
      next: (patient) => {
        this.router.navigate(['dashboard', { ssn: this.ssn }]);
      },
      error: (error) => {
        alert('No patient with the this SSN exists in the database');
      }
    });
  }
}
