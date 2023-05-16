import { config } from "../../constants/api-constants";
import { IAddToBasketRequest } from "../../models/requests/addToBasketRequest";
import apiClient from "../client";

export const addtoBasket = (addToBasketRequest: IAddToBasketRequest) =>
  apiClient({
    url: config.basketUrl,
    path: `addtobasket`,
    method: "POST",
    data: addToBasketRequest
  });

export const getFromBasket = (id: string) =>
  apiClient({
    url: config.basketUrl,
    path: `getfrombasket`,
    method: "POST",
    data: { userId: id }
  });

export const deleteItemFromBasket = (userId: string, ItemId: number) =>
  apiClient({
    url: config.basketUrl,
    path: `deleteitemfrombasket`,
    method: "POST",
    data: { userId: userId, ItemId: ItemId }
  });

export const ClearBasket = (userId: string) =>
  apiClient({
    url: config.basketUrl,
    path: `clearbasket`,
    method: "POST",
    data: { userId: userId }
  });