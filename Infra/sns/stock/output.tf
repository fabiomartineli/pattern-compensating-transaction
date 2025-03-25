output "sns_stock_confirmed_arn" {
  value = aws_sns_topic.stock_confirmed.arn
}

output "sns_stock_failed_arn" {
  value = aws_sns_topic.stock_failed.arn
}