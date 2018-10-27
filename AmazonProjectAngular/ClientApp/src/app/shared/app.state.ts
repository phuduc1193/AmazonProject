import { State, Action, StateContext } from "@ngxs/store";
import { AddProductToCart } from "./app.actions";
import { AuthState } from "./state/auth.state";

export interface AppStateModel {
  productIds: number[];
}

@State<AppStateModel>({
  name: "app",
  defaults: {
    productIds: []
  },
  children: [AuthState]
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
