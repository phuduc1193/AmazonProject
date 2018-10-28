import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { NgProgressRouterModule } from "@ngx-progressbar/router";

const routes: Routes = [
  {
    path: "",
    loadChildren: "app/views/home/home.module#HomeModule",
    pathMatch: "full"
  },
  {
    path: "product",
    loadChildren: "app/views/product/product.module#ProductModule"
  },
  {
    path: "sell",
    loadChildren: "app/views/seller/seller.module#SellerModule"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes), NgProgressRouterModule.forRoot()],
  exports: [RouterModule]
})
export class AppRoutingModule {}
