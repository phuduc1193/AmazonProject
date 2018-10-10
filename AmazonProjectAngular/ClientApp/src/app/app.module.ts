import { BrowserModule } from "@angular/platform-browser";
import { NgxsModule } from "@ngxs/store";
import { NgxsLoggerPluginModule } from "@ngxs/logger-plugin";
import { NgxsReduxDevtoolsPluginModule } from "@ngxs/devtools-plugin";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { ProductComponent } from "./product/product.component";

import { AppState } from "./shared/app.state";
import { UiModule } from "./ui/ui.module";
import { CoreModule } from "./core/core.module";

@NgModule({
  declarations: [AppComponent, ProductComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: "", component: AppComponent, pathMatch: "full" },
      { path: "product", component: ProductComponent }
    ]),
    NgxsModule.forRoot([AppState]),
    NgxsLoggerPluginModule.forRoot({ logger: console, collapsed: true }),
    NgxsReduxDevtoolsPluginModule.forRoot(),
    UiModule,
    CoreModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
