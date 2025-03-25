resource "aws_sns_topic" "payment_requested" {
  name = "payment-requested"

  tags = {
    Environment = "estudos-compensating-transaction"
  }
}