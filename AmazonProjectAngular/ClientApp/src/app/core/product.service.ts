import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { HttpClient } from "@angular/common/http";
import { Injectable, Inject } from "@angular/core";
import { Product } from "@interface/product";

@Injectable()
export class ProductService {
  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  private results = new BehaviorSubject<Product[]>([]);
  private result = new BehaviorSubject<Product>([]);

  public getResults() {
    return this.results.asObservable();
  }

  public getResult() {
    return this.result.asObservable();
  }

  public search(queryString: string) {
    const data = { query: queryString };
    this.http
      .post(this.baseUrl + "api/Products/Search", data)
      .subscribe((response: Product[]) => this.results.next(response));
  }

  public getFeaturedProduct() {
    this.http
      .get(this.baseUrl + "api/Products/Featured")
      .subscribe((response: Product) => this.result.next(response));
  }
}
