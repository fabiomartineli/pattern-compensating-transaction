resource "aws_sns_topic" "stock_confirmed" {
  name = "stock-confirmed"

  tags = {
    Environment = "estudos-compensating-transaction"
  }
}