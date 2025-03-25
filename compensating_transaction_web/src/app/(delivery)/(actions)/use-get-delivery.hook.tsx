import { getDeliveryByOrderAsync } from "@/services/delivery/delivery-by-order.service";
import { DeliveryDetailsViewModel, DeliveryStatusDescription } from "../(models)/delivery-details.viewmodel";

export async function useGetDelivery(_: DeliveryDetailsViewModel, data: FormData): Promise<DeliveryDetailsViewModel> {
    const response = await getDeliveryByOrderAsync({
        orderId: String(data.get("order-id"))
    });

    return {
        orderId: String(data.get("order-id")),
       status: response.status,
       statusDescription: DeliveryStatusDescription.get(response.status) || "Não identificado"
    };
}