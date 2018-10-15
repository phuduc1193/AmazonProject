import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { SellerRoutingModule } from "./seller-routing.module";
import { SellerComponent } from "./seller.component";

@NgModule({
  imports: [CommonModule, SellerRoutingModule],
  declarations: [SellerComponent]
})
export class SellerModule {
  constructor() {
    console.log("SellerModule loaded.");
  }
}
