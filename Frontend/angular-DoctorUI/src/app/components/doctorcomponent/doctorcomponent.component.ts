import { Component, OnInit, ViewChild } from '@angular/core';
import { GetPatientModel } from '../../model/get-patient.model';
import { DoctorserviceService } from '../../services/doctorservice.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import {
  MatCellDef,
  MatHeaderCellDef,
  MatHeaderRowDef,
  MatRowDef,
  MatTable,
  MatTableDataSource, MatTableModule,
} from '@angular/material/table';
import {CommonModule} from "@angular/common";
import { MatDialog } from '@angular/material/dialog';
import {MatIconModule} from "@angular/material/icon";
import {MeasurementsDialogComponent} from "../measurements-dialog/measurements-dialog.component";
import {
  PatientFormDialogComponentComponent
} from "../patient-form-dialog-component/patient-form-dialog-component.component";

@Component({
  selector: 'app-doctorcomponent',
  standalone: true,
  imports: [
    MatPaginator,
    MatSort,
    MatTable, // Add this
    MatHeaderRowDef, // Add this
    MatRowDef, // Add this
    MatHeaderCellDef, // Add this,
    MatTableModule,
    MatCellDef, // Add this,
    CommonModule,
    MatIconModule
  ],
  templateUrl: './doctorcomponent.component.html',
  styleUrl: './doctorcomponent.component.scss'
})
export class DoctorcomponentComponent implements OnInit {
  displayedColumns: string[] = ['ssn', 'name', 'email', 'measurements', 'delete'];
  dataSource = new MatTableDataSource<GetPatientModel>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private myService: DoctorserviceService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.myService.getPatients().subscribe(patients => {
      this.dataSource.data = patients;
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }
  deletePatient(patient: GetPatientModel): void {
    if (confirm(`Are you sure you want to delete patient "${patient.name}"?`)) {
      this.myService.deletePatient(patient.ssn).subscribe({
        next: () => {
          // Refresh the table data after successful deletion
          this.myService.getPatients().subscribe(patients => {
            this.dataSource.data = patients;
          });
        },
        error: error => {
          console.error('Error deleting patient:', error);
          alert('An error occurred while deleting the patient.');
        },
      });
    }
  }

  viewMeasurements(patient: GetPatientModel): void {
    this.myService.getMeasurementsForPatient(patient.ssn).subscribe(measurements => {
      this.dialog.open(MeasurementsDialogComponent, {
        data: { measurements },
      });
    });
  }

  openAddPatientDialog(): void {
    const dialogRef = this.dialog.open(PatientFormDialogComponentComponent);

    dialogRef.afterClosed().subscribe(result => {
      // Refresh the list of patients
      this.myService.getPatients().subscribe(patients => {
        this.dataSource.data = patients;
      });
    });
  }
}

