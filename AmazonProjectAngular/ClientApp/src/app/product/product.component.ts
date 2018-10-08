import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Store } from '@ngxs/store';
import { AddProductToCart } from '../shared/app.actions';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styles: [
    'input {display: block; padding: 3%; margin: 0; border: 1px solid #eee !important; width: 100%;}',
    'td {margin: 0; padding: 0}',
    '.table>tbody>tr>td {border-top: none}'
  ]
})
export class ProductComponent {
  public products: Product[];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private store: Store) {
    this.http.get<Product[]>(baseUrl + 'api/Products').subscribe(result => {
      this.products = result;
    }, error => console.error(error));
  }

  addProduct(name: string, description: string) {
    const data: Product = { name: name, description: description };
    this.http.post<Product>(this.baseUrl + 'api/Products', data).subscribe((result: Product) => {
      this.products.push(result);
    }, error => console.error(error));
  }

  addToCart(productId: number) {
    this.store.dispatch(new AddProductToCart(productId));
  }
}

interface Product {
  id?: number;
  name: string;
  description: string;
  category?: ProductCategory;
}

interface ProductCategory {
  id: number;
  name: string;
}
