import { DeliveryStatus } from "./delivery-status";

export type DeliveryByOrderResponseModel = {
    id: string;
    updatedAt: Date;
    status: DeliveryStatus;
}