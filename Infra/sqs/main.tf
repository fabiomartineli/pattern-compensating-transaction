module "sqs_delivery" {
  source = "./delivery"
}

module "sqs_order" {
  source = "./order"
}

module "sqs_payment" {
  source = "./payment"
}

module "sqs_stock" {
  source = "./stock"
}