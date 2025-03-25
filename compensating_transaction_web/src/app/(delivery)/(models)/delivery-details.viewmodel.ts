import { DeliveryStatus } from "@/models/delivery/delivery-status";

export interface DeliveryDetailsViewModel {
    orderId: string;
    status: DeliveryStatus;
    statusDescription: string;
}

export const DeliveryStatusDescription = new Map<DeliveryStatus, string>([
    [DeliveryStatus.Failed, 'Falha'],
    [DeliveryStatus.InRouteProcess, 'Em rota de entrega'],
    [DeliveryStatus.Delivered, 'Entregue'],
]);