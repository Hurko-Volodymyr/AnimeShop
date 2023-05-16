import { ItemToBasketModel } from "../itemToBasketModel"

export interface IAddToBasketRequest {
  id: string
  basketItem: ItemToBasketModel
}