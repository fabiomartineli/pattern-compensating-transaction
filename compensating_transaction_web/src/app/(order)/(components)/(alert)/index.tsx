import {
    Alert,
    AlertDescription,
} from "@/components/ui/alert"

interface OrderAlertProps {
    message: string;
}

export function OrderAlert(props: OrderAlertProps) {
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