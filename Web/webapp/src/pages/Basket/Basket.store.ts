import { makeAutoObservable, runInAction } from "mobx";
import * as cartApi from "../../api/modules/basket";
import { authStore } from "../../App";
import BasketItemModel from "../../models/basketInfo";
import { ItemToBasketModel } from "../../models/itemToBasketModel";

export class BasketStore {
  items: BasketItemModel[] = [];
  totalSum: number = 0;
  totalCount: number = 0;
  isLoading = false;
  constructor() {
    makeAutoObservable(this);
    runInAction(this.prefetchData);
  }

  prefetchData = async () => {
    try {
      this.isLoading = true;
      const res = await cartApi.getFromBasket(authStore.user?.profile.sub ?? "default");
      this.items = res.basketItems;
      this.totalSum = res.totalSum;
    } catch (e) {
      if (e instanceof Error) {
        console.error(e.message);
      }
    }
    this.isLoading = false;
  };

  getTotalCountOfBasketItems() {
    return this.items.reduce((ac, item) => ac + item.count, 0);
  }

  addItem = async (item: ItemToBasketModel) => {
    await cartApi.addtoBasket({ id: authStore.user?.profile.sub!, basketItem: { id: item.id, name: item.name, region: item.region, price: item.price, pictureUrl: item.pictureUrl } });
    await this.prefetchData();
  }

  deleteItem = async (id: number) => {
    await cartApi.deleteItemFromBasket(authStore.user?.profile.sub!, id);
    await this.prefetchData();
  }

  clearBasket = async () => {
    await cartApi.ClearBasket(authStore.user?.profile.sub!);
    await this.prefetchData();
  }
}
