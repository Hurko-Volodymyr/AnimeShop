import { config } from "../../constants/api-constants";
import OrderModel from "../../models/orderModel";
import apiClient from "../client";

export const getOrderByUserId = (userId: string) =>
  apiClient({
    url: config.orderUrl,
    path: `getordersbyuserid?userId=${userId}`,
    method: "POST",
    data: userId
  });

export const addOrder = (order: OrderModel) =>
  apiClient({
    url: config.orderUrl,
    path: `addorder`,
    method: "POST",
    data: order
  });