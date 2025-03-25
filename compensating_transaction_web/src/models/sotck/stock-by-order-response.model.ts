import { StockStatus } from "./stock-status";

export type StockByOrderResponseModel = {
    id: string;
    updatedAt: Date;
    status: StockStatus;
}