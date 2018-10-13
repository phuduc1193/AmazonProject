import { Component, OnInit } from "@angular/core";
import { ProductService } from "@core/product.service";

@Component({
  selector: "app-search",
  templateUrl: "./search.component.html",
  styleUrls: ["./search.component.scss"]
})
export class SearchComponent implements OnInit {
  constructor(private productService: ProductService) {}

  ngOnInit() {}

  search(input: string) {
    this.productService.search(input);
  }
}
