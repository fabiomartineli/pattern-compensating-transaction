import { DeliveryFailRequestModel } from "@/models/delivery/delivery-fail-request.model";
import { DeliveryFailResponseModel } from "@/models/delivery/delivery-fail-response.model";

export async function failDeliveryAsync(request: DeliveryFailRequestModel): Promise<DeliveryFailResponseModel>{
    try {
        const response = await fetch(`http://localhost:5133/api/delivery/fail`, {
            method: "POST",
            body: JSON.stringify(request),
            headers: {
                "Content-Type": "application/json"
            }
        });

        return {
            success: response.ok,
        }

    } catch (error) {
        console.log(error)
        return {
            success: false,
        }
    }
}