import { State, Action, StateContext, Selector } from "@ngxs/store";
import { AuthResponse } from "../../interface/auth-response";
import { AuthService } from "../../core/auth.service";
import { tap } from "rxjs/operators";
import { stateNameErrorMessage } from "@ngxs/store/src/decorators/state";

export interface AuthStateModel {
  authResponse: AuthResponse;
}

export class AuthenticateUser {
  static readonly type = "[auth] authenticate user";
  constructor(public username: string, public password: string) {}
}

export class LogOutUser {
  static readonly type = "[auth] log out user";
}

@State<AuthStateModel>({
  name: "auth",
  defaults: {
    authResponse: null
  }
})
export class AuthState {
  constructor(private authService: AuthService) {}

  @Selector()
  static isLoggedIn(state: AuthStateModel) {
    if (!state.authResponse) return false;
    const timestamp = new Date().getTime() / 1000;
    return state.authResponse.expires_in < timestamp ? true : false;
  }

  @Action(AuthenticateUser)
  authenticateUser(
    { setState, getState }: StateContext<AuthStateModel>,
    { username, password }: AuthenticateUser
  ) {
    const state = getState();
    return this.authService.login(username, password).pipe(
      tap((result: AuthResponse) => {
        setState({ ...state, authResponse: result });
      })
    );
  }

  @Action(LogOutUser)
  LogOutUser({ setState, getState }: StateContext<AuthStateModel>) {
    const state = getState();
    return setState({ ...state, authResponse: null });
  }
}
