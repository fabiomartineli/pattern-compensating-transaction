output "sns_delivery_confirmed_arn" {
  value = aws_sns_topic.delivery_confirmed.arn
}

output "sns_delivery_failed_arn" {
  value = aws_sns_topic.delivery_failed.arn
}