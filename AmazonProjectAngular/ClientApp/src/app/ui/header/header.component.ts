import { Component, OnInit } from "@angular/core";
import { environment } from "@env/environment";

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.scss"]
})
export class HeaderComponent implements OnInit {
  public itemCounter = 0;

  public totalPrice = "$0.00";

  public appName = environment.appName;

  constructor() {
    document.getElementsByTagName("title")[0].innerHTML = this.appName;
  }

  ngOnInit() {}
}
