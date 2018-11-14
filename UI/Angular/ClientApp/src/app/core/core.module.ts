import { NgModule } from "@angular/core";
import { ProductService } from "./product.service";
import { AuthService } from "./auth.service";
import { AuthGuardService } from "./auth-guard.service";

@NgModule({
  providers: [ProductService, AuthService, AuthGuardService]
})
export class CoreModule {}
