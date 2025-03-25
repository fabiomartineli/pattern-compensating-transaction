import { DeliveryConfirmRequestModel } from "@/models/delivery/delivery-confirm-request.model";
import { DeliveryConfirmResponseModel } from "@/models/delivery/delivery-confirm-response.model";

export async function confirmDeliveryAsync(request: DeliveryConfirmRequestModel): Promise<DeliveryConfirmResponseModel> {
    try {
        const response = await fetch(`http://localhost:5133/api/delivery/confirm`, {
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