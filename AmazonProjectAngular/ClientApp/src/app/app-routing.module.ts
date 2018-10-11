import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { WelcomeComponent } from "./ui/welcome/welcome.component";

const routes: Routes = [
  { path: "", component: WelcomeComponent, pathMatch: "full" },
  {
    path: "product",
    loadChildren: "app/product/product.module#ProductModule"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
