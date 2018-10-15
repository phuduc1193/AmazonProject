import { BehaviorSubject, Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Injectable, Inject } from "@angular/core";
import { Product } from "@app/interface/product";

@Injectable()
export class ProductService {
  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  private results = new BehaviorSubject<Product[]>([]);

  public getResults() {
    return this.results.asObservable();
  }

  public search(queryString: string) {
    const data = { query: queryString };
    this.http
      .post(this.baseUrl + "api/Products/Search", data)
      .subscribe((response: Product[]) => this.results.next(response));
  }

  public getFeaturedProduct(): Observable<Product> {
    return this.http.get<Product>(this.baseUrl + "api/Products/Featured");
  }
}
