import { Component, OnInit, Inject } from "@angular/core";
import { Select } from "@ngxs/store";
import { Observable } from "rxjs";
import { AuthState } from "../../shared/state/auth.state";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"]
})
export class HomeComponent implements OnInit {
  register: boolean = false;
  appName: string;

  @Select(AuthState.isLoggedIn)
  isLoggedIn$: Observable<boolean>;

  constructor(@Inject("APP_NAME") appName: string) {
    this.appName = appName;
  }

  ngOnInit() {}
}
