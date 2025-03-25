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