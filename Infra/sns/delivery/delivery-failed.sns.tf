resource "aws_sns_topic" "delivery_failed" {
  name = "delivery-failed"

  tags = {
    Environment = "estudos-compensating-transaction"
  }
}