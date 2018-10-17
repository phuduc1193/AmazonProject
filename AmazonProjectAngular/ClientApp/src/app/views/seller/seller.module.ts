import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { SellerRoutingModule } from "./seller-routing.module";

import { SellerComponent } from "./seller.component";
import { SellerHomeSearchComponent } from "./seller-home-search/seller-home-search.component";
import { SellerHomeDealsComponent } from "./seller-home-deals/seller-home-deals.component";

import { SellerLayoutComponent } from "./seller-layout/seller-layout.component";
import { SellerHeaderComponent } from "./seller-header/seller-header.component";
import { SellerOverviewComponent } from "./seller-overview/seller-overview.component";

@NgModule({
  imports: [CommonModule, SellerRoutingModule],
  declarations: [
    SellerComponent,
    SellerHomeSearchComponent,
    SellerHomeDealsComponent,
    SellerLayoutComponent,
    SellerHeaderComponent,
    SellerOverviewComponent
  ]
})
export class SellerModule {
  constructor() {
    console.log("SellerModule loaded.");
  }
}
