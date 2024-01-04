import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ODataModule } from 'angular-odata';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ODataModule.forRoot({
      config: {
        serviceRootUrl: 'ODATA_SERVICE_URL' //FOR EXAMPLE : http://localhost:5056/odata
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
