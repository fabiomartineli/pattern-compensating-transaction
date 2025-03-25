resource "aws_sns_topic_subscription" "sns_delivery_confirmed_sqs_order_delivery_confirm" {
  topic_arn = var.sns_delivery_confirmed_arn
  protocol  = "sqs"
  endpoint  = var.sqs_order_delivery_confirm_arn
}

resource "aws_sqs_queue_policy" "sns_delivery_confirmed_sqs_order_delivery_confirm_policy" {
  queue_url =  var.sqs_order_delivery_confirm_id

  policy = jsonencode({
    Version = "2012-10-17",
    Statement = [{
      Sid    = "EnableSubscription",
      Effect = "Allow",
      Principal = {
        Service = "sns.amazonaws.com",
      },
      Action   = "SQS:SendMessage",
      Resource = var.sqs_order_delivery_confirm_arn,
      Condition = {
        ArnLike = {
          "aws:SourceArn" = var.sns_delivery_confirmed_arn,
        }
      }
    }]
  })
}