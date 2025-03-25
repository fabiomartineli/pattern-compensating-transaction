import { OrderByIdRequestModel } from "@/models/order/order-by-id-request.model";
import { OrderByIdResponseModel } from "@/models/order/order-by-id-response.model";

export async function getOrderByIdAsync(request: OrderByIdRequestModel): Promise<OrderByIdResponseModel> {
    const responseDefault = {} as OrderByIdResponseModel;

    try {
        const response = await fetch(`http://localhost:5133/api/orders/${request.orderId}`);
        if (response.ok) {
            return await response.json() as OrderByIdResponseModel;
        }

        return responseDefault;

    } catch (error) {
        console.log(error)
        return responseDefault;
    }
}