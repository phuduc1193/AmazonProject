import { Component, OnInit } from "@angular/core";
import { environment } from "@env/environment";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"]
})
export class HomeComponent implements OnInit {
  appName: string = environment.appName;
  register: boolean = false;

  constructor() {}

  ngOnInit() {}
}
