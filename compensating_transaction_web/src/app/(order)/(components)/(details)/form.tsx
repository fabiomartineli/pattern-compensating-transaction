'use client'
import { Input } from "@/components/ui/input";
import { useActionState } from "react";
import { Button } from "@/components/ui/button";
import style from './styles.module.css';
import { Label } from "@/components/ui/label";
import { useGetOrder } from "../../(actions)/use-get-order.hook";
import { OrderDetailsViewModel } from "../../(models)/order-details.viewmodel";


export function OrderDetails() {
    const [state, formAction, isPending] = useActionState(useGetOrder, {} as OrderDetailsViewModel);

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
                    <Input readOnly defaultValue={state.statusDescription} />
                </div>
                <div className={style.Label}>
                    <Label>Observação</Label>
                    <Input readOnly defaultValue={state.note} />
                </div>
            </section>
        </>
    )
}