import { PaymentDetailsViewModel, PaymentStatusDescription } from "../(models)/payment-details.viewmodel";
import { getPaymentByOrderAsync } from "@/services/payment/payment-by-order.service";

export async function useGetPayment(_: PaymentDetailsViewModel, data: FormData): Promise<PaymentDetailsViewModel> {
    const response = await getPaymentByOrderAsync({
        orderId: String(data.get("order-id"))
    });

    return {
        orderId: String(data.get("order-id")),
        status: response.status,
        statusDescription: PaymentStatusDescription.get(response.status) || "Não identificado"
    };
}