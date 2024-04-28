import {Component, Inject} from '@angular/core';
import {GetMeasurementModel} from "../../model/get-measurement.model";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {CommonModule, DatePipe, JsonPipe} from "@angular/common";
import {
  MatCellDef,
  MatHeaderCellDef,
  MatHeaderRowDef,
  MatRowDef,
  MatTable,
  MatTableDataSource,
  MatTableModule
} from "@angular/material/table";
import {GetPatientModel} from "../../model/get-patient.model";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {MatIconModule} from "@angular/material/icon";
import {MatCheckbox} from "@angular/material/checkbox";
import {MeasurementService} from "../../services/measurementservice.service";
import {FormsModule} from "@angular/forms";
import {UpdateMeasurementModel} from "../../model/update-measurement.model";
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-measurements-dialog',
  standalone: true,
  imports: [
    JsonPipe,
    DatePipe,
    MatTable,
    MatSort,
    MatHeaderRowDef, // Add this
    MatRowDef, // Add this
    MatHeaderCellDef, // Add this,
    MatTableModule,
    MatCellDef, // Add this,
    CommonModule,
    MatIconModule,
    MatCheckbox,
    FormsModule
  ],
  templateUrl: './measurements-dialog.component.html',
  styleUrl: './measurements-dialog.component.scss'
})
export class MeasurementsDialogComponent {
  displayedColumns: string[] = ['date', 'systolic', 'diastolic', 'isSeen', 'checkbox', 'update' ];
  dataSource = new MatTableDataSource<GetMeasurementModel>();

  constructor(
    public dialogRef: MatDialogRef<MeasurementsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { measurements: GetMeasurementModel[] },
    private myService: MeasurementService,
     private snackBar: MatSnackBar // Inject MatSnackBar here
) {
    console.log("measurement data" + data.measurements);
    this.dataSource = new MatTableDataSource(data.measurements);
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  markAsSeen(measurement: GetMeasurementModel): void {
    measurement.isSeen = !measurement.isSeen;
    this.myService.markAsSeen(measurement.id).subscribe(() => {
      // Handle successful response from backend
    });
  }
  updateMeasurements(measurement: GetMeasurementModel): void {
    const updatedDetails = new UpdateMeasurementModel(measurement.systolic, measurement.diastolic);
    this.myService.updateMeasurement(measurement.id, updatedDetails).subscribe({
      next: () => {
        // Show a snackbar on successful update
        this.snackBar.open('Update successful', 'Close', {
          duration: 3000,  // Duration in milliseconds after which the snackbar will disappear
        });
        console.log('Update successful');
      },
      error: (error) => {
        // Handle error
        this.snackBar.open('Error updating measurement: ' + error.message, 'Close', {
          duration: 3000,
        });
        console.error('Error updating measurement:', error);
      }
    });
  }



}
