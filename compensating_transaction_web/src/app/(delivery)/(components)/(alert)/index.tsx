import {
    Alert,
    AlertDescription,
} from "@/components/ui/alert"

interface DeliveryAlertProps {
    message: string;
}

export function DeliveryAlert(props: DeliveryAlertProps) {
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