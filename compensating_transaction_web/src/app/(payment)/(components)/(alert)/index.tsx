import {
    Alert,
    AlertDescription,
} from "@/components/ui/alert"

interface PaymentAlertProps {
    message: string;
}

export function PaymentAlert(props: PaymentAlertProps) {
    return (
        <>
            <Alert variant="destructive">
                <AlertDescription>
                    {props.message}
                </AlertDescription>
            </Alert>
        </>
    )
}