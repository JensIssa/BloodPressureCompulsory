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
    MatCheckbox
  ],
  templateUrl: './measurements-dialog.component.html',
  styleUrl: './measurements-dialog.component.scss'
})
export class MeasurementsDialogComponent {
  displayedColumns: string[] = ['checkbox', 'date', 'systolic', 'diastolic', 'isSeen'];
  dataSource = new MatTableDataSource<GetMeasurementModel>();

  constructor(
    public dialogRef: MatDialogRef<MeasurementsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { measurements: GetMeasurementModel[] },
  ) {
    console.log("measurement data" + data.measurements);
    this.dataSource = new MatTableDataSource(data.measurements);
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
}
