import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { AuthService } from "../../core/auth.service";

@Component({
  selector: "app-callback",
  template: `
    Login successful.
  `
})
export class CallbackComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.authService.completeAuthentication();
    this.router.navigate([""]);
  }
}
