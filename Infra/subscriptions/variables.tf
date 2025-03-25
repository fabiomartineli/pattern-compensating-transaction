variable "sns_delivery_failed_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_stock_cancel_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sns_payment_confirmed_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_stock_create_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sns_order_created_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_payment_request_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sns_stock_failed_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_payment_cancel_arn" {
  type = string
  nullable = false
  default = ""
}

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

variable "sqs_delivery_create_arn" {
  type = string
  nullable = false
  default = ""
}





variable "sqs_stock_cancel_id" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_stock_create_id" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_payment_request_id" {
  type = string
  nullable = false
  default = ""
}
variable "sqs_payment_cancel_id" {
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

variable "sqs_delivery_create_id" {
  type = string
  nullable = false
  default = ""
}