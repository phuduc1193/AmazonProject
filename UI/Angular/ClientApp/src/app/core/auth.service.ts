import { Injectable } from "@angular/core";
import { UserManager, UserManagerSettings, User } from "oidc-client";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  private manager = new UserManager(getClientSettings());
  private user: User = null;

  constructor() {
    this.manager.getUser().then((user: User) => {
      this.user = user;
    });
  }

  isLoggedIn(): boolean {
    return this.user != null && !this.user.expired;
  }

  getClaims(): any {
    return this.user.profile;
  }

  getAuthorizationHeaderValue(): string {
    return `${this.user.token_type} ${this.user.access_token}`;
  }

  startAuthentication(): Promise<void> {
    return this.manager.signinRedirect();
  }

  completeAuthentication(): Promise<void> {
    return this.manager.signinRedirectCallback().then(user => {
      this.user = user;
    });
  }

  logOut(): Promise<void> {
    return this.manager.signoutPopup();
  }
}

export function getClientSettings(): UserManagerSettings {
  return {
    authority: "http://localhost:5000/",
    client_id: "spa",
    redirect_uri: "http://localhost:4200/callback",
    post_logout_redirect_uri: "http://localhost:4200",
    response_type: "id_token token",
    scope: "openid profile email address phone",
    filterProtocolClaims: true,
    loadUserInfo: true
  };
}
