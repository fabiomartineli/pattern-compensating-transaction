import { PaymentConfirmRequestModel } from "@/models/payment/payment-confirm-request.model";
import { PaymentConfirmResponseModel } from "@/models/payment/payment-confirm-response.model";

export async function confirmPaymentAsync(request: PaymentConfirmRequestModel): Promise<PaymentConfirmResponseModel> {
    try {
        const response = await fetch(`http://localhost:5133/api/payments/confirm`, {
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