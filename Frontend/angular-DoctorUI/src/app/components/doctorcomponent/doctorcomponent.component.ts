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
import {MatIconModule} from "@angular/material/icon";

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
  displayedColumns: string[] = ['ssn', 'name', 'email', 'delete'];
  dataSource = new MatTableDataSource<GetPatientModel>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private myService: DoctorserviceService) { }

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
}

