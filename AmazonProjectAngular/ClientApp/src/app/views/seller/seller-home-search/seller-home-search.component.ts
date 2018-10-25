import {
  Component,
  OnInit,
  HostListener,
  ViewChild,
  ElementRef
} from "@angular/core";
import { ProductService } from "../../../core/product.service";

@Component({
  selector: "app-seller-home-search",
  templateUrl: "./seller-home-search.component.html",
  styleUrls: ["./seller-home-search.component.scss"]
})
export class SellerHomeSearchComponent implements OnInit {
  public searchPlaceHolder: string;
  public boxYPosition: number;
  public fixedNavbar: boolean;

  @ViewChild("box")
  box: ElementRef;

  @HostListener("window:resize", ["$event"])
  onResize(event?) {
    if (window.innerWidth < 350) {
      this.searchPlaceHolder = "Enter keywords";
    } else this.searchPlaceHolder = "Enter keywords or the UPC/ISBN #";

    if (this.box.nativeElement.getBoundingClientRect().top < 0)
      this.boxYPosition = window.scrollY;
  }

  @HostListener("window:scroll", ["$event"])
  onWindowScroll($event) {
    if (this.boxYPosition && window.scrollY < this.boxYPosition) {
      this.fixedNavbar = false;
      return;
    }

    if (this.box.nativeElement.getBoundingClientRect().top >= 0) return;

    if (!this.boxYPosition || window.scrollY <= this.boxYPosition)
      this.boxYPosition = window.scrollY;

    this.fixedNavbar = true;
  }

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.onResize();
  }

  search(input: string) {
    this.productService.search(input);
  }
}
