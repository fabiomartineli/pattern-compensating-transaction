resource "aws_sns_topic_subscription" "sns_order_created_sqs_payment_request" {
  topic_arn = var.sns_order_created_arn
  protocol  = "sqs"
  endpoint  = var.sqs_payment_request_arn
}

resource "aws_sqs_queue_policy" "sns_order_created_sqs_payment_request_policy" {
  queue_url = var.sqs_payment_request_id

  policy = jsonencode({
    Version = "2012-10-17",
    Statement = [{
      Sid    = "EnableSubscription",
      Effect = "Allow",
      Principal = {
        Service = "sns.amazonaws.com",
      },
      Action   = "SQS:SendMessage",
      Resource = var.sqs_payment_request_arn,
      Condition = {
        ArnLike = {
          "aws:SourceArn" = var.sns_order_created_arn,
        }
      }
    }]
  })
}