import { PaymentByOrderRequestModel } from "@/models/payment/payment-by-order-request.model";
import { PaymentByOrderResponseModel } from "@/models/payment/payment-by-order-response.model";

export async function getPaymentByOrderAsync(request: PaymentByOrderRequestModel): Promise<PaymentByOrderResponseModel> {
    const responseDefault = {} as PaymentByOrderResponseModel;

    try {
        const response = await fetch(`http://localhost:5133/api/payments?orderId=${request.orderId}`);
        if (response.ok) {
            return await response.json() as PaymentByOrderResponseModel;
        }

        return responseDefault;

    } catch (error) {
        console.log(error)
        return responseDefault;
    }
}