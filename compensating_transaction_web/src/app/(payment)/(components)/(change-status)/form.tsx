'use client'
import { Input } from "@/components/ui/input";
import { useConfirmPayment } from "../../(actions)/use-confirm-payment.hook";
import { useActionState } from "react";
import { Button } from "@/components/ui/button";
import style from './styles.module.css';
import { PaymentAlert } from "../(alert)";
import { useFailPayment } from "../../(actions)/use-fail-payment.hook";
import { PaymentChangeStatusFormViewModel } from "../../(models)/payment-change-status-form.viewmodel";


export function PaymentChangeStatus() {
    const [confirmState, confirmAction, isConfirmPending] = useActionState(useConfirmPayment, {} as PaymentChangeStatusFormViewModel);
    const [failState, failAction, isFailPending] = useActionState(useFailPayment, {} as PaymentChangeStatusFormViewModel);

    return (
        <>
            <form className={style.Form}>
                <Input type="text" name="order-id" placeholder="Pedido" defaultValue={confirmState.orderId ?? failState.orderId} />
                <div className={style.Actions}>
                    <Button formAction={confirmAction} type="submit" disabled={isConfirmPending}>
                        {isConfirmPending ? "Confirmando..." : "Confirmar"}
                    </Button>
                    <Button formAction={failAction} variant="destructive" type="submit" disabled={isFailPending}>
                        {isFailPending ? "Falhando..." : "Falhar"}
                    </Button>
                </div>
            </form>

            {confirmState.success === false && <PaymentAlert message={confirmState.errorMessage} />}
            {failState.success === false && <PaymentAlert message={failState.errorMessage} />}

            <hr />
        </>
    )
}