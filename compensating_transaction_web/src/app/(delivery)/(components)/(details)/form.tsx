'use client'
import { Input } from "@/components/ui/input";
import { useActionState } from "react";
import { Button } from "@/components/ui/button";
import style from './styles.module.css';
import { useGetDelivery } from "../../(actions)/use-get-delivery.hook";
import { Label } from "@/components/ui/label";
import { DeliveryDetailsViewModel } from "../../(models)/delivery-details.viewmodel";


export function PaymentDetails() {
    const [state, formAction, isPending] = useActionState(useGetDelivery, {} as DeliveryDetailsViewModel);

    return (
        <>
            <form className={style.Form}>
                <Input type="text" name="order-id" placeholder="Pedido" defaultValue={state.orderId} />
                <Button formAction={formAction} type="submit" disabled={isPending}>
                    {isPending ? "Buscando..." : "Buscar"}
                </Button>
            </form>

            <section className={style.Section}>
                <div className={style.Label}>
                    <Label>Status</Label>
                    <Input readOnly defaultValue={state?.statusDescription} />
                </div>
            </section>
        </>
    )
}