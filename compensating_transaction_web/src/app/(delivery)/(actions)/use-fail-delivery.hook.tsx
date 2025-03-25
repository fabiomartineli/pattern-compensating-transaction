import { failDeliveryAsync } from "@/services/delivery/delivery-fail.service";
import { DeliveryChangeStatusFormViewModel } from "../(models)/delivery-change-status-form.viewmodel";

export async function useFailDelivery(_: DeliveryChangeStatusFormViewModel, data: FormData): Promise<DeliveryChangeStatusFormViewModel> {
    const response = await failDeliveryAsync({
        orderId: String(data.get("order-id"))
    });

    return {
        orderId: String(data.get("order-id")),
        success: response.success,
        errorMessage: response.success ? "" : "Ocorreu um erro ao informar falha na entrega."
    };
}