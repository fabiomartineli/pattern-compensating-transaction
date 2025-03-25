'use client'
import { useActionState } from "react";
import { Input } from "@/components/ui/input";
import { useConfirmDelivery } from "../../(actions)/use-confirm-delivery.hook";
import { Button } from "@/components/ui/button";
import style from './styles.module.css';
import { DeliveryAlert } from "../(alert)";
import { useFailDelivery} from "../../(actions)/use-fail-delivery.hook";
import { DeliveryChangeStatusFormViewModel } from "../../(models)/delivery-change-status-form.viewmodel";


export function PaymentChangeStatus() {
    const [confirmState, confirmAction, isConfirmPending] = useActionState(useConfirmDelivery, {} as DeliveryChangeStatusFormViewModel);
    const [failState, failAction, isFailPending] = useActionState(useFailDelivery, {} as DeliveryChangeStatusFormViewModel);

    return (
        <>
            <form className={style.Form}>
                <Input type="text" name="order-id" placeholder="Pedido" defaultValue={confirmState?.orderId ?? failState?.orderId } />
                <div className={style.Actions}>
                    <Button formAction={confirmAction} type="submit" disabled={isConfirmPending}>
                        {isConfirmPending ? "Confirmando..." : "Confirmar"}
                    </Button>
                    <Button formAction={failAction} variant="destructive" type="submit" disabled={isFailPending}>
                        {isFailPending ? "Falhando..." : "Falhar"}
                    </Button>
                </div>
            </form>

            {confirmState?.success === false && <DeliveryAlert message={confirmState.errorMessage} />}
            {failState?.success === false && <DeliveryAlert message={failState.errorMessage} />}
            
            <hr />
        </>
    )
}