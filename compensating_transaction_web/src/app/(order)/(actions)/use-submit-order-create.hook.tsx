import { createOrderAsync } from "@/services/order/order-create.service";
import { OrderFormViewModel } from "../(models)/order-form.viewmodel";

export async function useSubmitOrderCreate(_: OrderFormViewModel, data: FormData): Promise<OrderFormViewModel> {
    const response = await createOrderAsync({
        number: String(data?.get("number"))
    });

    return {
        id: response.id,
        number: response.success ? String(data?.get("number")) : "",
        success: response.success,
        message: response.success ? `Pedido ${response.id}` : "Ocorreu um erro na geração do pedido."
    };
}