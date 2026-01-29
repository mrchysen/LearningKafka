# Task 4 - "Order Processing Microservices" - ASP NET Core kafka integration

Goal: To get familiar with stream processing.

## Scenario: A food ordering system.

Topic orders: New orders (OrderId, UserId, Items, Status="New").

Topic payments: Payments (OrderId, Status="Paid"/"Failed").

## Architecture

[OrderMicroservice] -> message to topic "orders" -> [PaymentMicroservice] -> message to topic "payments" -> [OrderMicroservice]

## Tasks:

1. Write a Streaming Application that JOINs orders and payments by OrderId.

2. Filter for only successful payments and create an enriched event into a new paid-orders topic.

What You Will Learn: Stream processing, join, filter, and aggregation operations.