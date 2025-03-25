output "sqs_order_cancel_arn" {
  value = aws_sqs_queue.order_cancel.arn
}

output "sqs_order_create_arn" {
  value = aws_sqs_queue.order_create.arn
}

output "sqs_order_delivery_confirm_arn" {
  value = aws_sqs_queue.order_delivery_confirm.arn
}

output "sqs_order_payment_confirm_arn" {
  value = aws_sqs_queue.order_payment_confirm.arn
}

output "sqs_order_start_payment_arn" {
  value = aws_sqs_queue.order_start_payment.arn
}

output "sqs_order_stock_confirm_arn" {
  value = aws_sqs_queue.order_stock_confirm.arn
}

output "sqs_order_cancel_id" {
  value = aws_sqs_queue.order_cancel.id
}

output "sqs_order_create_id" {
  value = aws_sqs_queue.order_create.id
}

output "sqs_order_delivery_confirm_id" {
  value = aws_sqs_queue.order_delivery_confirm.id
}

output "sqs_order_payment_confirm_id" {
  value = aws_sqs_queue.order_payment_confirm.id
}

output "sqs_order_start_payment_id" {
  value = aws_sqs_queue.order_start_payment.id
}

output "sqs_order_stock_confirm_id" {
  value = aws_sqs_queue.order_stock_confirm.id
}

