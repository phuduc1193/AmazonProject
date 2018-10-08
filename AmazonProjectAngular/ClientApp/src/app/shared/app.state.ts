import { State, Action, StateContext } from '@ngxs/store';
import { AddProductToCart } from './app.actions';

export interface AppStateModel {
  productIds: number[];
}

@State<AppStateModel>({
  name: 'app',
  defaults: {
    productIds: []
  }
})
export class AppState {
  @Action(AddProductToCart)
  addProduct({ patchState, getState }: StateContext<AppStateModel>, { productId }: AddProductToCart) {
    const state = getState();
    patchState({ productIds: [...state.productIds, productId] });
  }
}
