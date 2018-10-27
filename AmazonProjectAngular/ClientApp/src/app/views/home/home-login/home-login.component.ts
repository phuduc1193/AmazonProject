import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { FormControl, Validators, FormGroup } from "@angular/forms";

import { Store } from "@ngxs/store";
import { AuthenticateUser } from "../../../shared/state/auth.state";

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

  constructor(private store: Store) {}

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
    this.store.dispatch(new AuthenticateUser(this.username, this.password));
  }

  onRegister() {
    this.register.emit(true);
  }
}
