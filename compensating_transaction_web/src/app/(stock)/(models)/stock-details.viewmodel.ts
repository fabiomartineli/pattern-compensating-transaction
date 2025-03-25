import { StockStatus } from "@/models/sotck/stock-status";

export interface StockDetailsViewModel {
    orderId: string;
    status: StockStatus;
    statusDescription: string;
}

export const StockStatusDescription = new Map<StockStatus, string>([
    [StockStatus.Failed, 'Falha'],
    [StockStatus.WaitingReturn, 'Aguardando retorno da mercadoria'],
    [StockStatus.Processing, 'Em processamento'],
    [StockStatus.Confirmed, 'Confirmado'],
]);