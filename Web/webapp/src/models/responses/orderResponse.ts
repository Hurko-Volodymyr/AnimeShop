import OrderDetailsResponse from "./orderDetailsResponse";

interface OrderResponse {
  id: number;
  gameAccountId: string;  
  name: string;
  lastName: string;  
  email: string;
  orderProducts: OrderDetailsResponse[];
  createdAt: string;
  totalSum: number;
}

export default OrderResponse;