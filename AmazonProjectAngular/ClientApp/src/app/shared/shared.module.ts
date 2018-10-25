import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgBootstrapFormValidationModule } from "ng-bootstrap-form-validation";
import { HttpClientModule } from "@angular/common/http";
import { NgxsModule } from "@ngxs/store";
import { NgxsLoggerPluginModule } from "@ngxs/logger-plugin";
import { NgxsReduxDevtoolsPluginModule } from "@ngxs/devtools-plugin";
import { AppState } from "../shared/app.state";

@NgModule({
  imports: [
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgBootstrapFormValidationModule.forRoot(),
    NgxsModule.forRoot([AppState]),
    NgxsLoggerPluginModule.forRoot({ logger: console, collapsed: true }),
    NgxsReduxDevtoolsPluginModule.forRoot()
  ]
})
export class SharedModule {}
