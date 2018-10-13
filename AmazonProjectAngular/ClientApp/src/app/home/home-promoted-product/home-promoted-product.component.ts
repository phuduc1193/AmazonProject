import { Component, OnInit } from "@angular/core";
import { ProductService } from "@core/product.service";
import { Product } from "@interface/product";

@Component({
  selector: "[appHomePromotedProduct]",
  templateUrl: "./home-promoted-product.component.html",
  styleUrls: ["./home-promoted-product.component.scss"]
})
export class HomePromotedProductComponent implements OnInit {
  public product: Product;

  constructor(private productService: ProductService) {
    this.productService.getFeaturedProduct();
    this.productService.getResult().subscribe((result: Product) => {
      this.product = result;
    });
  }

  ngOnInit() {}
}
