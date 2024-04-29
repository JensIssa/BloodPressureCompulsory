import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import {provideHttpClient} from "@angular/common/http";
import { routes } from './app/app.routes';
import { provideRouter } from '@angular/router';
import {provideAnimations} from "@angular/platform-browser/animations";


bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    provideHttpClient(),
    provideAnimations()
  ]
}).catch((err) =>
  console.error(err)
);
