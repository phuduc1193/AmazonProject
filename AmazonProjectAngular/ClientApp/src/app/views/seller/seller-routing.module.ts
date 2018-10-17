import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { SellerComponent } from "./seller.component";
import { SellerLayoutComponent } from "./seller-layout/seller-layout.component";
import { SellerOverviewComponent } from "./seller-overview/seller-overview.component";

const routes: Routes = [
  { path: "", component: SellerComponent },
  {
    path: "sl",
    component: SellerLayoutComponent,
    children: [{ path: "overview", component: SellerOverviewComponent }]
  },
  { path: "**", redirectTo: "" }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SellerRoutingModule {}
