import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CommonModule } from "@angular/common";
import { NgProgressModule } from "@ngx-progressbar/core";

import { LayoutComponent } from "./layout/layout.component";
import { HeaderComponent } from "./header/header.component";
import { FooterComponent } from "./footer/footer.component";
import { SearchComponent } from "./search/search.component";
import { CallbackComponent } from './callback/callback.component';

@NgModule({
  imports: [CommonModule, RouterModule, NgProgressModule],
  declarations: [
    LayoutComponent,
    HeaderComponent,
    FooterComponent,
    SearchComponent,
    CallbackComponent
  ],
  exports: [LayoutComponent]
})
export class UiModule {}
