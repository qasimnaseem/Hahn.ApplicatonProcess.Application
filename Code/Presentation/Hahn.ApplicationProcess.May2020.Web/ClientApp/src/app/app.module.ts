import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { MaterialModule } from "./material/material.module";
import { AppComponent } from './app.component';
import { NgxSpinnerModule } from "ngx-spinner";
import { TranslateModule } from '@ngx-translate/core';
import { ApplicantListComponent } from './components/applicant/applicant-list/applicant-list.component';
import { ApplicantAddEditComponent } from './components/applicant/applicant-add-edit/applicant-add-edit.component';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppHttpInterceptor } from './shared/interceptors/app-http-interceptor';
import { AgGridModule } from 'ag-grid-angular';
import { ButtonRendererComponent } from './components/common/button-renderer/button-renderer/button-renderer.component';
import { ShellModule } from './components/common/shell/shell/shell.module';

@NgModule({
  declarations: [
    AppComponent,
    ApplicantListComponent,
    ApplicantAddEditComponent,
    ButtonRendererComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    ShellModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    NgxSpinnerModule,
    TranslateModule.forRoot(),
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    AgGridModule.withComponents([ButtonRendererComponent])
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AppHttpInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
