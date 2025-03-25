import { StockByOrderRequestModel } from "@/models/sotck/stock-by-order-request.model";
import { StockByOrderResponseModel } from "@/models/sotck/stock-by-order-response.model";

export async function getStockByOrderAsync(request: StockByOrderRequestModel): Promise<StockByOrderResponseModel> {
    const responseDefault = {} as StockByOrderResponseModel;

    try {
        const response = await fetch(`http://localhost:5133/api/stock?orderId=${request.orderId}`);
        if (response.ok) {
            return await response.json() as StockByOrderResponseModel;
        }

        return responseDefault;

    } catch (error) {
        console.log(error)
        return responseDefault;
    }
}