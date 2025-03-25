output "sqs_stock_cancel_arn" {
  value = aws_sqs_queue.stock_cancel.arn
}

output "sqs_stock_create_arn" {
  value = aws_sqs_queue.stock_create.arn
}

output "sqs_stock_cancel_id" {
  value = aws_sqs_queue.stock_cancel.id
}

output "sqs_stock_create_id" {
  value = aws_sqs_queue.stock_create.id
}