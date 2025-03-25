variable "sns_stock_confirmed_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_delivery_create_arn" {
  type = string
  nullable = false
  default = ""
}

variable "sqs_delivery_create_id" {
  type = string
  nullable = false
  default = ""
}