import { PaymentFailRequestModel } from "@/models/payment/payment-fail-request.model";
import { PaymentFailResponseModel } from "@/models/payment/payment-fail-response.model";

export async function failPaymentAsync(request: PaymentFailRequestModel): Promise<PaymentFailResponseModel>{
    try {
        const response = await fetch(`http://localhost:5133/api/payments/fail`, {
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