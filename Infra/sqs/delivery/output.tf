output "sqs_delivery_confirm_arn" {
  value = aws_sqs_queue.delivery_confirm.arn
}

output "sqs_delivery_fail_arn" {
  value = aws_sqs_queue.delivery_fail.arn
}

output "sqs_delivery_create_arn" {
  value = aws_sqs_queue.delivery_create.arn
}

output "sqs_delivery_confirm_id" {
  value = aws_sqs_queue.delivery_confirm.id
}

output "sqs_delivery_fail_id" {
  value = aws_sqs_queue.delivery_fail.id
}

output "sqs_delivery_create_id" {
  value = aws_sqs_queue.delivery_create.id
}