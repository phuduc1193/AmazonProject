import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgBootstrapFormValidationModule } from "ng-bootstrap-form-validation";
import { HttpClientModule } from "@angular/common/http";
import { NgProgressHttpModule } from "@ngx-progressbar/http";
import { NgProgressModule } from "@ngx-progressbar/core";
import { NgxsModule } from "@ngxs/store";
import { NgxsLoggerPluginModule } from "@ngxs/logger-plugin";
import { NgxsReduxDevtoolsPluginModule } from "@ngxs/devtools-plugin";
import { NgxsStoragePluginModule } from "@ngxs/storage-plugin";
import { AppState } from "./app.state";
import { AuthState } from "./state/auth.state";

@NgModule({
  imports: [
    HttpClientModule,
    NgProgressModule.forRoot(),
    NgProgressHttpModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    NgBootstrapFormValidationModule.forRoot(),
    NgxsModule.forRoot([AppState, AuthState]),
    NgxsLoggerPluginModule.forRoot({ logger: console, collapsed: true }),
    NgxsReduxDevtoolsPluginModule.forRoot(),
    NgxsStoragePluginModule.forRoot()
  ]
})
export class SharedModule {}
