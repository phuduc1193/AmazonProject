import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { NgxsModule } from "@ngxs/store";
import { NgxsLoggerPluginModule } from "@ngxs/logger-plugin";
import { NgxsReduxDevtoolsPluginModule } from "@ngxs/devtools-plugin";

import { AppState } from "../shared/app.state";

@NgModule({
  imports: [
    HttpClientModule,
    FormsModule,
    NgxsModule.forRoot([AppState]),
    NgxsLoggerPluginModule.forRoot({ logger: console, collapsed: true }),
    NgxsReduxDevtoolsPluginModule.forRoot()
  ],
  declarations: []
})
export class SharedModule {}
