import { Component, OnInit, Inject } from "@angular/core";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"]
})
export class HomeComponent implements OnInit {
  register: boolean = false;
  appName: string;

  constructor(@Inject("APP_NAME") appName: string) {
    this.appName = appName;
  }

  ngOnInit() {}
}
