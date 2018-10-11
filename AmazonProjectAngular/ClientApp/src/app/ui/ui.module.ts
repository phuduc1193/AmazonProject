import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";

import { CommonModule } from "@angular/common";
import { LayoutComponent } from "./layout/layout.component";
import { HeaderComponent } from "./header/header.component";
import { FooterComponent } from "./footer/footer.component";
import { SearchComponent } from "./search/search.component";
import { WelcomeComponent } from './welcome/welcome.component';
import { CurrentDealsComponent } from './current-deals/current-deals.component';

@NgModule({
  imports: [CommonModule, RouterModule],
  declarations: [
    LayoutComponent,
    HeaderComponent,
    FooterComponent,
    SearchComponent,
    WelcomeComponent,
    CurrentDealsComponent
  ],
  exports: [LayoutComponent]
})
export class UiModule {}
