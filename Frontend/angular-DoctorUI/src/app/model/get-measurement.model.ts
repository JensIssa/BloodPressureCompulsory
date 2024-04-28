export class GetMeasurementModel {
  constructor(
    public patientSSN: string,
    public systolic: number,
    public diastolic: number,
    public isSeen: boolean,
    public date: string,
    public id: number
  ) {}
}
