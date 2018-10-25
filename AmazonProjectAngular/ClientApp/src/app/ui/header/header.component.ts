import { Component, OnInit, Inject } from "@angular/core";

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.scss"]
})
export class HeaderComponent implements OnInit {
  itemCounter: number = 0;
  totalPrice: string = "$0.00";
  appName: string;

  constructor(@Inject("APP_NAME") appName: string) {
    document.getElementsByTagName("title")[0].innerHTML = appName;
    this.appName = appName;
  }

  ngOnInit() {}
}
