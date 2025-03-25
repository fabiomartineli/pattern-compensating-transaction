import { PaymentChangeStatus } from "./(components)/(change-status)/form";
import { PaymentDetails } from "./(components)/(details)/form";

export default function Payment() {
    return (
        <>
            <PaymentChangeStatus />
            <PaymentDetails />
        </>
    )
}