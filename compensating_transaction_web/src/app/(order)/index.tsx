import { OrderCreateForm } from "./(components)/(create)/form";
import { OrderDetails } from "./(components)/(details)/form";

export default function Order() {
    return (
        <>
            <OrderCreateForm />
            <OrderDetails />
        </>
    )
}