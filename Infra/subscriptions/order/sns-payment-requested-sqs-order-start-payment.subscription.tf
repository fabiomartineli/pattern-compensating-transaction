resource "aws_sns_topic_subscription" "sns_payment_requested_sqs_order_start_payment" {
  topic_arn = var.sns_payment_requested_arn
  protocol  = "sqs"
  endpoint  = var.sqs_order_start_payment_arn
}

resource "aws_sqs_queue_policy" "sns_payment_requested_sqs_order_start_payment_policy" {
  queue_url = var.sqs_order_start_payment_id

  policy = jsonencode({
    Version = "2012-10-17",
    Statement = [{
      Sid    = "EnableSubscription",
      Effect = "Allow",
      Principal = {
        Service = "sns.amazonaws.com",
      },
      Action   = "SQS:SendMessage",
      Resource = var.sqs_order_start_payment_arn,
      Condition = {
        ArnLike = {
          "aws:SourceArn" = var.sns_payment_requested_arn,
        }
      }
    }]
  })
}