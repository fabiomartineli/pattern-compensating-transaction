terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "5.82.2"
    }
  }

  backend "local" {
    path = "state/terraform.tfstate"
  }
}

provider "aws" {
  region     = var.provider_aws_region
  access_key = var.provider_aws_access_key
  secret_key = var.provider_aws_secret_key

  default_tags {
    tags = {
      Environment = "estudos-compensating-transaction"
    }
  }
}

module "sns" {
  source = "./sns"
}

module "sqs" {
  source = "./sqs"
}

module "subscriptions" {
  source                         = "./subscriptions"
  sns_delivery_confirmed_arn     = module.sns.sns_delivery_confirmed_arn
  sns_delivery_failed_arn        = module.sns.sns_delivery_failed_arn
  sns_order_created_arn          = module.sns.sns_order_created_arn
  sns_payment_confirmed_arn      = module.sns.sns_payment_confirmed_arn
  sns_payment_failed_arn         = module.sns.sns_payment_failed_arn
  sns_payment_requested_arn      = module.sns.sns_payment_requested_arn
  sns_stock_confirmed_arn        = module.sns.sns_stock_confirmed_arn
  sns_stock_failed_arn           = module.sns.sns_stock_failed_arn
  sqs_delivery_create_arn        = module.sqs.sqs_delivery_create_arn
  sqs_order_cancel_arn           = module.sqs.sqs_order_cancel_arn
  sqs_order_delivery_confirm_arn = module.sqs.sqs_order_delivery_confirm_arn
  sqs_order_payment_confirm_arn  = module.sqs.sqs_order_payment_confirm_arn
  sqs_order_start_payment_arn    = module.sqs.sqs_order_start_payment_arn
  sqs_order_stock_confirm_arn    = module.sqs.sqs_order_stock_confirm_arn
  sqs_payment_cancel_arn         = module.sqs.sqs_payment_cancel_arn
  sqs_payment_request_arn        = module.sqs.sqs_payment_request_arn
  sqs_stock_cancel_arn           = module.sqs.sqs_stock_cancel_arn
  sqs_stock_create_arn           = module.sqs.sqs_stock_create_arn
  sqs_delivery_create_id         = module.sqs.sqs_delivery_create_id
  sqs_order_cancel_id            = module.sqs.sqs_order_cancel_id
  sqs_order_delivery_confirm_id  = module.sqs.sqs_order_delivery_confirm_id
  sqs_order_payment_confirm_id   = module.sqs.sqs_order_payment_confirm_id
  sqs_order_start_payment_id     = module.sqs.sqs_order_start_payment_id
  sqs_order_stock_confirm_id     = module.sqs.sqs_order_stock_confirm_id
  sqs_payment_cancel_id          = module.sqs.sqs_payment_cancel_id
  sqs_payment_request_id         = module.sqs.sqs_payment_request_id
  sqs_stock_cancel_id            = module.sqs.sqs_stock_cancel_id
  sqs_stock_create_id            = module.sqs.sqs_stock_create_id
}
