import { State, Action, StateContext } from "@ngxs/store";
import { AddProductToCart } from "./app.actions";

export interface AppStateModel {
  user: any;
  productIds: number[];
}

@State<AppStateModel>({
  name: "app",
  defaults: {
    user: null,
    productIds: []
  }
})
export class AppState {
  constructor() {}

  @Action(AddProductToCart)
  addProduct(
    { patchState, getState }: StateContext<AppStateModel>,
    { productId }: AddProductToCart
  ) {
    const state = getState();
    patchState({ productIds: [...state.productIds, productId] });
  }
}
