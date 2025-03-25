resource "aws_sqs_queue" "delivery_fail" {
  name                       = "delivery-fail"
  delay_seconds              = 10
  max_message_size           = 2048
  message_retention_seconds  = 86400
  visibility_timeout_seconds = 120
  receive_wait_time_seconds  = 10

  redrive_policy = jsonencode({
    deadLetterTargetArn = aws_sqs_queue.delivery_fail_dl.arn
    maxReceiveCount     = 1
  })

  tags = {
    Environment = "estudos-compensating-transaction"
  }
}

resource "aws_sqs_queue" "delivery_fail_dl" {
  name = "delivery-fail-dl"

  tags = {
    Environment = "estudos-compensating-transaction"
  }
}

resource "aws_sqs_queue_redrive_allow_policy" "delivery_fail_redrive_allow_policy" {
  queue_url = aws_sqs_queue.delivery_fail_dl.id

  redrive_allow_policy = jsonencode({
    redrivePermission = "byQueue",
    sourceQueueArns   = [aws_sqs_queue.delivery_fail.arn]
  })
}
