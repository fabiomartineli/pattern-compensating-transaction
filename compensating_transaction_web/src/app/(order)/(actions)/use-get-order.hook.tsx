import { getOrderByIdAsync } from "@/services/order/order-by-id.service";
import { OrderDetailsViewModel, OrderStatusDescription } from "../(models)/order-details.viewmodel";

export async function useGetOrder(_: OrderDetailsViewModel, data: FormData): Promise<OrderDetailsViewModel> {
    const response = await getOrderByIdAsync({
        orderId: String(data?.get("order-id"))
    });

    return {
        orderId: String(data.get("order-id")),
        note: response.note,
        status: response.status,
        statusDescription: OrderStatusDescription.get(response.status) || "Não identificado"
    };
}