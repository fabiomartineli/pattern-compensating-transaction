resource "aws_sns_topic" "delivery_confirmed" {
  name = "delivery-confirmed"

  tags = {
    Environment = "estudos-compensating-transaction"
  }
}