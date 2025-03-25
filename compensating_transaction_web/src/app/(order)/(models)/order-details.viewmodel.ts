import { OrderStatus } from "@/models/order/order-status";

export interface OrderDetailsViewModel {
    orderId: string;
    note: string;
    status: OrderStatus;
    statusDescription: string;
}

export const OrderStatusDescription = new Map<OrderStatus, string>([
    [OrderStatus.Created, 'Enviada'],
    [OrderStatus.Failed, 'Falha'],
    [OrderStatus.DeliveryConfirmed, 'Pedido entregue'],
    [OrderStatus.InDeliveryProcess, 'Pedido em rota de entrega'],
    [OrderStatus.InPaymentProcess, 'Pagamento em processamento'],
    [OrderStatus.InStockSeparationProcess, 'Separação de estoque'],
]);