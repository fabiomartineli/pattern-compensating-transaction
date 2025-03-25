import { DeliveryByOrderRequestModel } from "@/models/delivery/delivery-by-order-request.model";
import { DeliveryByOrderResponseModel } from "@/models/delivery/delivery-by-order-response.model";

export async function getDeliveryByOrderAsync(request: DeliveryByOrderRequestModel): Promise<DeliveryByOrderResponseModel>{
    const responseDefault = {} as DeliveryByOrderResponseModel;

    try {
        const response = await fetch(`http://localhost:5133/api/delivery?orderId=${request.orderId}`);
    
        if (response.ok){
            return await response.json() as DeliveryByOrderResponseModel;
        }

        return responseDefault;

    } catch (error) {
        console.log(error)
        return responseDefault;
    }
}