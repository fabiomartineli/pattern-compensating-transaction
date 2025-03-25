variable "sns_delivery_confirmed_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_order_delivery_confirm_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sns_payment_confirmed_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_order_payment_confirm_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sns_payment_failed_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_order_cancel_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sns_payment_requested_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_order_start_payment_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sns_stock_confirmed_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_order_stock_confirm_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_order_delivery_confirm_id" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_order_payment_confirm_id" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_order_cancel_id" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_order_start_payment_id" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_order_stock_confirm_id" {
  type = string
  nullable = false
  default = ""
}