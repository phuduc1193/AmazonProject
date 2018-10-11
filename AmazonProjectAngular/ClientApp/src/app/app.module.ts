import { NgModule } from "@angular/core";

import { AppComponent } from "./app.component";

import { BrowserModule } from "@angular/platform-browser";
import { AppRoutingModule } from "./app-routing.module";
import { UiModule } from "./ui/ui.module";
import { CoreModule } from "./core/core.module";
import { SharedModule } from "./shared/shared.module";
import { ProductModule } from "./product/product.module";

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    AppRoutingModule,
    UiModule,
    CoreModule,
    SharedModule,
    ProductModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
