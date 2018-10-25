import { HttpClient } from "@angular/common/http";
import { Injectable, Inject } from "@angular/core";

@Injectable()
export class AuthService {
  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  public login(username: string, password: string) {
    const data = { username: username, password: password };
    return this.http.post(this.baseUrl + "api/auth/login", data);
  }

  public register(username: string, password: string) {
    const data = { username: username, password: password };
    return this.http.post(this.baseUrl + "api/auth/register", data);
  }
}
