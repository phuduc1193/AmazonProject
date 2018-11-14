import { State, Selector } from "@ngxs/store";
import { AuthResponse } from "../../interface/auth-response";
import { AuthService } from "../../core/auth.service";

export interface AuthStateModel {
  authResponse: AuthResponse;
}

@State<AuthStateModel>({
  name: "auth",
  defaults: {
    authResponse: null
  }
})
export class AuthState {
  constructor() {}

  @Selector()
  static isLoggedIn() {
    return AuthService.isLoggedIn();
  }
}
