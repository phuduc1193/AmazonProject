import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CommonModule } from "@angular/common";
import { LoadingBarHttpClientModule } from "@ngx-loading-bar/http-client";
import { LoadingBarRouterModule } from "@ngx-loading-bar/router";

import { LayoutComponent } from "./layout/layout.component";
import { HeaderComponent } from "./header/header.component";
import { FooterComponent } from "./footer/footer.component";
import { SearchComponent } from "./search/search.component";

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    LoadingBarHttpClientModule,
    LoadingBarRouterModule
  ],
  declarations: [
    LayoutComponent,
    HeaderComponent,
    FooterComponent,
    SearchComponent
  ],
  exports: [LayoutComponent]
})
export class UiModule {}
