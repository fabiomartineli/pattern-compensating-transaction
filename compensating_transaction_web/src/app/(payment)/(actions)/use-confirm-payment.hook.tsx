import { confirmPaymentAsync } from "@/services/payment/payment-confirm.service";
import { PaymentChangeStatusFormViewModel } from "../(models)/payment-change-status-form.viewmodel";

export async function useConfirmPayment(_: PaymentChangeStatusFormViewModel, data: FormData): Promise<PaymentChangeStatusFormViewModel> {
    const response = await confirmPaymentAsync({
        orderId: String(data.get("order-id"))
    });

    return {
        orderId: String(data.get("order-id")),
        success: response.success,
        errorMessage: response.success ? "" : "Ocorreu um erro ao confirmar o pagamento."
    };
}