import { OrderCreateRequestModel } from "@/models/order/order-create-request.model";
import { OrderCreateResponseModel } from "@/models/order/order-create-response.model";

export async function createOrderAsync(request: OrderCreateRequestModel): Promise<OrderCreateResponseModel> {
    const responseDefault = {
        success: false,
        id: ""
    }

    try {
        const response = await fetch(`http://localhost:5133/api/orders`, {
            method: "POST",
            body: JSON.stringify(request),
            headers: {
                "Content-Type": "application/json"
            }
        });

        if (response.ok) {
            const data = await response.json();
    
            return {
                success: true,
                id: data.id
            }
        }

        return responseDefault;

    } catch (error) {
        console.log(error)
        return responseDefault;
    }
}