resource "aws_sns_topic_subscription" "sns_payment_confirmed_sqs_stock_create" {
  topic_arn = var.sns_payment_confirmed_arn
  protocol  = "sqs"
  endpoint  = var.sqs_stock_create_arn
}

resource "aws_sqs_queue_policy" "sns_payment_confirmed_sqs_stock_create_policy" {
  queue_url = var.sqs_stock_create_id

  policy = jsonencode({
    Version = "2012-10-17",
    Statement = [{
      Sid    = "EnableSubscription",
      Effect = "Allow",
      Principal = {
        Service = "sns.amazonaws.com",
      },
      Action   = "SQS:SendMessage",
      Resource = var.sqs_stock_create_arn,
      Condition = {
        ArnLike = {
          "aws:SourceArn" = var.sns_payment_confirmed_arn,
        }
      }
    }]
  })
}