import { Component, OnInit } from "@angular/core";
import { AuthService } from "../../../core/auth.service";

@Component({
  selector: "[appHomeAuth]",
  templateUrl: "./home-auth.component.html",
  styleUrls: ["./home-auth.component.scss"]
})
export class HomeAuthComponent implements OnInit {
  constructor(private authService: AuthService) {}

  ngOnInit() {}

  logIn() {
    this.authService.startAuthentication();
  }
}
