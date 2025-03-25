module "subscriptions_delivery" {
  source                  = "./delivery"
  sns_stock_confirmed_arn = var.sns_stock_confirmed_arn
  sqs_delivery_create_arn = var.sqs_delivery_create_arn
  sqs_delivery_create_id = var.sqs_delivery_create_id
}

module "subscriptions_order" {
  source                         = "./order"
  sns_delivery_confirmed_arn     = var.sns_delivery_confirmed_arn
  sns_payment_confirmed_arn      = var.sns_payment_confirmed_arn
  sns_payment_failed_arn         = var.sns_payment_failed_arn
  sns_payment_requested_arn      = var.sns_payment_requested_arn
  sns_stock_confirmed_arn        = var.sns_stock_confirmed_arn
  sqs_order_cancel_arn           = var.sqs_order_cancel_arn
  sqs_order_delivery_confirm_arn = var.sqs_order_delivery_confirm_arn
  sqs_order_payment_confirm_arn  = var.sqs_order_payment_confirm_arn
  sqs_order_start_payment_arn    = var.sqs_order_start_payment_arn
  sqs_order_stock_confirm_arn    = var.sqs_order_stock_confirm_arn
  sqs_order_cancel_id = var.sqs_order_cancel_id
  sqs_order_delivery_confirm_id = var.sqs_order_delivery_confirm_id
  sqs_order_payment_confirm_id = var.sqs_order_payment_confirm_id
  sqs_order_start_payment_id = var.sqs_order_start_payment_id
  sqs_order_stock_confirm_id = var.sqs_order_stock_confirm_id
}

module "subscriptions_payment" {
  source                  = "./payment"
  sns_order_created_arn = var.sns_order_created_arn
  sns_stock_failed_arn = var.sns_stock_failed_arn
  sqs_payment_cancel_arn = var.sqs_payment_cancel_arn
  sqs_payment_request_arn = var.sqs_payment_request_arn
  sqs_payment_cancel_id = var.sqs_payment_cancel_id
  sqs_payment_request_id = var.sqs_payment_request_id
}

module "subscriptions_stock" {
  source                  = "./stock"
  sns_delivery_failed_arn = var.sns_delivery_failed_arn
  sns_payment_confirmed_arn = var.sns_payment_confirmed_arn
  sqs_stock_cancel_arn = var.sqs_stock_cancel_arn
  sqs_stock_create_arn = var.sqs_stock_create_arn
  sqs_stock_cancel_id = var.sqs_stock_cancel_id
  sqs_stock_create_id = var.sqs_stock_create_id
}