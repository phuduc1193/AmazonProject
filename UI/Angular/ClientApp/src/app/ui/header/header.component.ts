import { Component, OnInit, Inject } from "@angular/core";
import { Select } from "@ngxs/store";
import { Observable } from "rxjs";
import { AuthState } from "../../shared/state/auth.state";
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

  constructor(
    @Inject("APP_NAME") appName: string,
    private authService: AuthService
  ) {
    document.getElementsByTagName("title")[0].innerHTML = appName;
    this.appName = appName;
  }

  @Select(AuthState.isLoggedIn)
  isLoggedIn$: Observable<boolean>;

  ngOnInit() {}

  logout() {
    this.authService.logOut();
  }
}
