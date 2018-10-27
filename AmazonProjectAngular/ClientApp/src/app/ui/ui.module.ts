import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CommonModule } from "@angular/common";
import { NgProgressModule } from "@ngx-progressbar/core";

import { LayoutComponent } from "./layout/layout.component";
import { HeaderComponent } from "./header/header.component";
import { FooterComponent } from "./footer/footer.component";
import { SearchComponent } from "./search/search.component";

@NgModule({
  imports: [CommonModule, RouterModule, NgProgressModule],
  declarations: [
    LayoutComponent,
    HeaderComponent,
    FooterComponent,
    SearchComponent
  ],
  exports: [LayoutComponent]
})
export class UiModule {}
