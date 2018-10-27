import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { FormControl, Validators, FormGroup } from "@angular/forms";
import { ErrorMessage } from "ng-bootstrap-form-validation";

import { Store } from "@ngxs/store";
import { AuthenticateUser } from "../../../shared/state/auth.state";

@Component({
  selector: "[appHomeRegister]",
  templateUrl: "./home-register.component.html",
  styleUrls: ["./home-register.component.scss"]
})
export class HomeRegisterComponent implements OnInit {
  @Input()
  appName: string;
  formGroup: FormGroup;
  @Output()
  logIn: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(private store: Store) {}

  ngOnInit() {
    this.formGroup = new FormGroup(
      {
        Username: new FormControl("", [Validators.required]),
        Password: new FormControl("", [Validators.required]),
        PasswordVerification: new FormControl("", [
          Validators.required,
          this.matchValidator("Password")
        ])
      },
      { updateOn: "change" }
    );
  }

  matchValidator(otherControlName: string) {
    let thisControl: FormControl;
    let otherControl: FormControl;

    return function matchOtherValidate(control: FormControl) {
      if (!control.parent) {
        return null;
      }

      if (!thisControl) {
        thisControl = control;
        otherControl = control.parent.get(otherControlName) as FormControl;
        if (!otherControl) {
          throw new Error(
            "matchOtherValidator(): other control is not found in parent group"
          );
        }
        otherControl.valueChanges.subscribe(() => {
          thisControl.updateValueAndValidity();
        });
      }

      if (!otherControl) {
        return null;
      }

      return otherControl.value === thisControl.value
        ? null
        : {
            mismatch: true
          };
    };
  }

  customErrorMessages: ErrorMessage[] = [
    {
      error: "mismatch",
      format: (label, error) => `${label} mismatch`
    }
  ];

  get username() {
    return this.formGroup.get("Username").value;
  }
  get password() {
    return this.formGroup.get("Password").value;
  }
  get passwordVerification() {
    return this.formGroup.get("PasswordVerification").value;
  }

  onSubmit() {
    if (!this.formGroup.valid) return;
    this.store.dispatch(new AuthenticateUser(this.username, this.password));
  }

  onLogIn() {
    this.logIn.emit(true);
  }
}
