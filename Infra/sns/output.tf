output "sns_delivery_confirmed_arn" {
  value = module.sns_delivery.sns_delivery_confirmed_arn
}

output "sns_delivery_failed_arn" {
  value = module.sns_delivery.sns_delivery_failed_arn
}

output "sns_order_created_arn" {
  value = module.sns_order.sns_order_created_arn
}

output "sns_payment_confirmed_arn" {
  value = module.sns_payment.sns_payment_confirmed_arn
}

output "sns_payment_failed_arn" {
  value = module.sns_payment.sns_payment_failed_arn
}

output "sns_payment_requested_arn" {
  value = module.sns_payment.sns_payment_requested_arn
}

output "sns_stock_confirmed_arn" {
  value = module.sns_stock.sns_stock_confirmed_arn
}

output "sns_stock_failed_arn" {
  value = module.sns_stock.sns_stock_failed_arn
}