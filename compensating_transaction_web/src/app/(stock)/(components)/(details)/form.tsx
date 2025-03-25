'use client'
import { Input } from "@/components/ui/input";
import { useActionState } from "react";
import { Button } from "@/components/ui/button";
import style from './styles.module.css';
import { Label } from "@/components/ui/label";
import { useGetStock } from "../../(actions)/use-get-stock.hook";
import { StockDetailsViewModel } from "../../(models)/stock-details.viewmodel";


export function StockDetails() {
    const [state, formAction, isPending] = useActionState(useGetStock, {} as StockDetailsViewModel);

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
            </section>
        </>
    )
}