output "sns_payment_confirmed_arn" {
  value = aws_sns_topic.payment_confirmed.arn
}

output "sns_payment_failed_arn" {
  value = aws_sns_topic.payment_failed.arn
}

output "sns_payment_requested_arn" {
  value = aws_sns_topic.payment_requested.arn
}