module "sns_delivery" {
  source = "./delivery"
}

module "sns_order" {
  source = "./order"
}

module "sns_payment" {
  source = "./payment"
}

module "sns_stock" {
  source = "./stock"
}