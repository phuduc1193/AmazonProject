import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.scss"]
})
export class HeaderComponent implements OnInit {
  public itemCounter = 0;

  public totalPrice = "$0.00";

  constructor() {}

  ngOnInit() {}
}
