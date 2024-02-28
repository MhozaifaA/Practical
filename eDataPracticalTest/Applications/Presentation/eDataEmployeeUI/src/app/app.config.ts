import { ApplicationConfig, importProvidersFrom } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimations } from '@angular/platform-browser/animations';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgHttpLoaderModule } from 'ng-http-loader';
import { ToastrModule, provideToastr } from 'ngx-toastr';

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), provideAnimations(), provideHttpClient(
    withInterceptorsFromDi()
  ),
    provideToastr({
      timeOut: 20000,
      positionClass: 'toast-top-right',
    }), 

    importProvidersFrom(
    NgHttpLoaderModule.forRoot()
  )
  ]
};
