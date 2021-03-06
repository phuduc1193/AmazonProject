import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HomeRoutingModule } from "./home-routing.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgBootstrapFormValidationModule } from "ng-bootstrap-form-validation";

import { HomeComponent } from "./home.component";
import { HomeDealsComponent } from "./home-deals/home-deals.component";
import { HomePromotedProductComponent } from "./home-promoted-product/home-promoted-product.component";
import { HomeDealOfTheDayComponent } from "./home-deal-of-the-day/home-deal-of-the-day.component";
import { HomeAdsComponent } from "./home-ads/home-ads.component";
import { HomeBookBestSellersComponent } from "./home-book-best-sellers/home-book-best-sellers.component";
import { HomeElectronicBestSellersComponent } from "./home-electronic-best-sellers/home-electronic-best-sellers.component";
import { HomeTrendingProductsComponent } from "./home-trending-products/home-trending-products.component";
import { HomeMostWishedComponent } from "./home-most-wished/home-most-wished.component";
import { HomeMoviesComponent } from "./home-movies/home-movies.component";
import { HomeAuthComponent } from './home-auth/home-auth.component';

@NgModule({
  imports: [
    CommonModule,
    HomeRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgBootstrapFormValidationModule
  ],
  declarations: [
    HomeComponent,
    HomeDealsComponent,
    HomePromotedProductComponent,
    HomeDealOfTheDayComponent,
    HomeAdsComponent,
    HomeBookBestSellersComponent,
    HomeElectronicBestSellersComponent,
    HomeTrendingProductsComponent,
    HomeMostWishedComponent,
    HomeMoviesComponent,
    HomeAuthComponent
  ]
})
export class HomeModule {
  constructor() {
    console.log("HomeModule loaded.");
  }
}
