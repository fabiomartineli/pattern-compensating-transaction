import { StockDetailsViewModel, StockStatusDescription } from "../(models)/stock-details.viewmodel";
import { getStockByOrderAsync } from "@/services/sotck/stock-by-order.service";

export async function useGetStock(_: StockDetailsViewModel, data: FormData): Promise<StockDetailsViewModel> {
    const response = await getStockByOrderAsync({
        orderId: String(data.get("order-id"))
    });

    return {
        orderId: String(data.get("order-id")),
        status: response.status,
        statusDescription: StockStatusDescription.get(response.status) || "Não identificado"
    };
}