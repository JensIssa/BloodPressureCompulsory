export class CreateMeasurementModel {
  constructor(
    public systolic: number,
    public diastolic: number,
    public patientSSN: string
  ) {}
}
