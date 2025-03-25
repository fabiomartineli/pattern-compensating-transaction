import { PaymentChangeStatusFormViewModel } from "../(models)/payment-change-status-form.viewmodel";
import { failPaymentAsync } from "@/services/payment/payment-fail.service";

export async function useFailPayment(_: PaymentChangeStatusFormViewModel, data: FormData): Promise<PaymentChangeStatusFormViewModel> {
    const response = await failPaymentAsync({
        orderId: String(data.get("order-id"))
    });

    return {
        orderId: String(data.get("order-id")),
        success: response.success,
        errorMessage: response.success ? "" : "Ocorreu um erro ao informar falha no pagamento."
    };
}