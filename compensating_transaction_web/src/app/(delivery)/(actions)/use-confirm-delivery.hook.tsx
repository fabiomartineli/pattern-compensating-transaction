import { confirmDeliveryAsync } from "@/services/delivery/delivery-confirm.service";
import { DeliveryChangeStatusFormViewModel } from "../(models)/delivery-change-status-form.viewmodel";

export async function useConfirmDelivery(_: DeliveryChangeStatusFormViewModel, data: FormData): Promise<DeliveryChangeStatusFormViewModel> {
    const response = await confirmDeliveryAsync({
        orderId: String(data.get("order-id"))
    });

    return {
        orderId: String(data.get("order-id")),
        success: response.success,
        errorMessage: response.success ? "" : "Ocorreu um erro ao confirmar a entrega."
    };
}