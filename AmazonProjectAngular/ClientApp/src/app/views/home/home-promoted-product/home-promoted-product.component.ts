import { Component, OnInit, OnDestroy } from "@angular/core";
import { untilDestroyed } from "ngx-take-until-destroy";
import { ProductService } from "../../../core/product.service";
import { Product } from "../../../interface/product";

@Component({
  selector: "[appHomePromotedProduct]",
  templateUrl: "./home-promoted-product.component.html",
  styleUrls: ["./home-promoted-product.component.scss"]
})
export class HomePromotedProductComponent implements OnInit, OnDestroy {
  public product: Product;

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.productService
      .getFeaturedProduct()
      .pipe(untilDestroyed(this))
      .subscribe((result: Product) => {
        this.product = result;
      });
  }

  ngOnDestroy() {}
}
