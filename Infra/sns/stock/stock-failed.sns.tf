resource "aws_sns_topic" "stock_failed" {
  name = "stock-failed"

  tags = {
    Environment = "estudos-compensating-transaction"
  }
}