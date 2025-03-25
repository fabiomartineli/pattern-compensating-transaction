import { PaymentStatus } from "./payment-status";

export type PaymentByOrderResponseModel = {
    id: string;
    updatedAt: Date;
    status: PaymentStatus;
}