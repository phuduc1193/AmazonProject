import { Component, OnInit } from "@angular/core";
import { ProductService } from "@app/core/product.service";
import { Product } from "@app/interface/product";

@Component({
  selector: "[appHomePromotedProduct]",
  templateUrl: "./home-promoted-product.component.html",
  styleUrls: ["./home-promoted-product.component.scss"]
})
export class HomePromotedProductComponent implements OnInit {
  public product: Product;

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.productService.getFeaturedProduct().subscribe((result: Product) => {
      this.product = result;
    });
  }
}
