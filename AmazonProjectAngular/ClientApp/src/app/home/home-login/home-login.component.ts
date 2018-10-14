import { Component, OnInit } from "@angular/core";

import { environment } from "@environments/environment";

@Component({
  selector: "[appHomeLogin]",
  templateUrl: "./home-login.component.html",
  styleUrls: ["./home-login.component.scss"]
})
export class HomeLoginComponent implements OnInit {
  public appName = environment.appName;

  constructor() {}

  ngOnInit() {}
}
