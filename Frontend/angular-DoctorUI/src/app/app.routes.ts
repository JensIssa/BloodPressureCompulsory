import { Routes } from '@angular/router';
import {AppComponent} from "./app.component";
import {DoctorcomponentComponent} from "./components/doctorcomponent/doctorcomponent.component";

export const routes: Routes = [
  { path: '', redirectTo: 'doctorcomponent', pathMatch: 'full' },

  {path: 'doctorcomponent', component: DoctorcomponentComponent}
];
