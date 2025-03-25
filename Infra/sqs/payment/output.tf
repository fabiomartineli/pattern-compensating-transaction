output "sqs_payment_cancel_arn" {
  value = aws_sqs_queue.payment_cancel.arn
}

output "sqs_payment_request_arn" {
  value = aws_sqs_queue.payment_request.arn
}

output "sqs_payment_fail_arn" {
  value = aws_sqs_queue.payment_fail.arn
}


output "sqs_payment_cancel_id" {
  value = aws_sqs_queue.payment_cancel.id
}

output "sqs_payment_request_id" {
  value = aws_sqs_queue.payment_request.id
}

output "sqs_payment_fail_id" {
  value = aws_sqs_queue.payment_fail.id
}