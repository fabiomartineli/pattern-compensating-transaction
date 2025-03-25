import { OrderStatus } from "./order-status";

export type OrderByIdResponseModel = {
    id: string;
    note: string;
    status: OrderStatus;
}