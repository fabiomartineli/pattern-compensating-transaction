resource "aws_sns_topic" "payment_failed" {
  name = "payment-failed"

  tags = {
    Environment = "estudos-compensating-transaction"
  }
}