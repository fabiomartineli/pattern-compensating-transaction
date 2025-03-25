resource "aws_sns_topic" "payment_confirmed" {
  name = "payment-confirmed"

  tags = {
    Environment = "estudos-compensating-transaction"
  }
}