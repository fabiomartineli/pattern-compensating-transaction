resource "aws_sns_topic_subscription" "sns_stock_failed_sqs_payment_cancel" {
  topic_arn = var.sns_stock_failed_arn
  protocol  = "sqs"
  endpoint  = var.sqs_payment_cancel_arn
}

resource "aws_sqs_queue_policy" "sns_stock_failed_sqs_payment_cancel_policy" {
  queue_url = var.sqs_payment_cancel_id

  policy = jsonencode({
    Version = "2012-10-17",
    Statement = [{
      Sid    = "EnableSubscription",
      Effect = "Allow",
      Principal = {
        Service = "sns.amazonaws.com",
      },
      Action   = "SQS:SendMessage",
      Resource = var.sqs_payment_cancel_arn,
      Condition = {
        ArnLike = {
          "aws:SourceArn" = var.sns_stock_failed_arn,
        }
      }
    }]
  })
}