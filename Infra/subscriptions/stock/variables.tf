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