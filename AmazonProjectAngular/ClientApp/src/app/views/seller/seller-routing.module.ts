import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { SellerComponent } from "./seller.component";

const routes: Routes = [{ path: "", component: SellerComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SellerRoutingModule {}
