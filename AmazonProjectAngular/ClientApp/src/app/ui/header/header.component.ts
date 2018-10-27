import { Component, OnInit, Inject } from "@angular/core";
import { Store, Select } from "@ngxs/store";
import { Observable } from "rxjs";
import { AuthState, LogOutUser } from "../../shared/state/auth.state";

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.scss"]
})
export class HeaderComponent implements OnInit {
  itemCounter: number = 0;
  totalPrice: string = "$0.00";
  appName: string;

  @Select(AuthState.isLoggedIn)
  isLoggedIn$: Observable<boolean>;

  constructor(@Inject("APP_NAME") appName: string, private store: Store) {
    document.getElementsByTagName("title")[0].innerHTML = appName;
    this.appName = appName;
  }

  ngOnInit() {}

  logout() {
    this.store.dispatch(new LogOutUser());
  }
}
