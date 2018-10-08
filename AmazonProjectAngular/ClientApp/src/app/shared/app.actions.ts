export class AddProductToCart {
  static readonly type = '[app] AddProductToCart';
  constructor(public productId: number) { }
}
