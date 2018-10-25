import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { FormControl, Validators, FormGroup } from "@angular/forms";

import { AuthService } from "@app/core/auth.service";
import { AuthResponse } from "@app/interface/auth-response";

@Component({
  selector: "[appHomeLogin]",
  templateUrl: "./home-login.component.html",
  styleUrls: ["./home-login.component.scss"]
})
export class HomeLoginComponent implements OnInit {
  @Input()
  appName: string;
  formGroup: FormGroup;
  @Output()
  register: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.formGroup = new FormGroup(
      {
        Username: new FormControl("", [Validators.required]),
        Password: new FormControl("", [Validators.required])
      },
      { updateOn: "change" }
    );
  }

  get username() {
    return this.formGroup.get("Username").value;
  }
  get password() {
    return this.formGroup.get("Password").value;
  }

  onSubmit() {
    if (!this.formGroup.valid) return;
    this.authService
      .login(this.username, this.password)
      .subscribe((result: AuthResponse) => {
        console.log(result);
      });
  }

  onRegister() {
    this.register.emit(true);
  }
}
