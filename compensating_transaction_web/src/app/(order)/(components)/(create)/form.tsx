'use client'
import { Input } from "@/components/ui/input";
import { useSubmitOrderCreate } from "../../(actions)/use-submit-order-create.hook";
import { useActionState } from "react";
import { Button } from "@/components/ui/button";
import style from './styles.module.css';
import { OrderAlert } from "../(alert)";
import { OrderFormViewModel } from "../../(models)/order-form.viewmodel";


export function OrderCreateForm() {
    const [state, formAction, isPending] = useActionState(useSubmitOrderCreate, {} as OrderFormViewModel);

    return (
        <>
            <form className={style.Form}>
                <Input type="text" name="number" placeholder="Numeração" />
                <Button formAction={formAction} type="submit" disabled={isPending}>
                    {isPending ? "Criando..." : "Criar"}
                </Button>
            </form>

            <OrderAlert message={state.message} />

            <hr />
        </>
    )
}