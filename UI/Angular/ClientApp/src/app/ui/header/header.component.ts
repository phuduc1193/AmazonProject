import { Component, OnInit, Inject } from "@angular/core";
import { AuthService } from "../../core/auth.service";

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.scss"]
})
export class HeaderComponent implements OnInit {
  itemCounter: number = 0;
  totalPrice: string = "$0.00";
  appName: string;
  isLoggedIn: boolean;

  constructor(
    @Inject("APP_NAME") appName: string,
    private authService: AuthService
  ) {
    document.getElementsByTagName("title")[0].innerHTML = appName;
    this.appName = appName;
  }

  ngOnInit() {
    this.isLoggedIn = this.authService.isLoggedIn();
  }

  logout() {
    this.authService.logOut();
  }
}
