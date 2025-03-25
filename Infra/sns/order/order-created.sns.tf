resource "aws_sns_topic" "order_created" {
  name = "order-created"

  tags = {
    Environment = "estudos-compensating-transaction"
  }
}