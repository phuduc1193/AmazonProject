import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { SellerRoutingModule } from "./seller-routing.module";
import { SellerComponent } from "./seller.component";
import { SellerLayoutComponent } from "./seller-layout/seller-layout.component";
import { SellerHeaderComponent } from "./seller-header/seller-header.component";

@NgModule({
  imports: [CommonModule, SellerRoutingModule],
  declarations: [SellerComponent, SellerLayoutComponent, SellerHeaderComponent]
})
export class SellerModule {
  constructor() {
    console.log("SellerModule loaded.");
  }
}
