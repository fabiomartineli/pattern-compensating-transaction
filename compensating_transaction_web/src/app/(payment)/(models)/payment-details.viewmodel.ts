import { PaymentStatus } from "@/models/payment/payment-status";

export interface PaymentDetailsViewModel {
    orderId: string;
    status: PaymentStatus;
    statusDescription: string;
}

export const PaymentStatusDescription = new Map<PaymentStatus, string>([
    [PaymentStatus.Failed, 'Falha'],
    [PaymentStatus.Processing, 'Em processamento'],
    [PaymentStatus.RefundRequested, 'Reembolso solicitado'],
    [PaymentStatus.Success, 'Confirmado'],
]);