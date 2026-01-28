# Task 3 - "User Event Tracker" - Working with Keys and Serialization

Goal: Master message keys and working with data formats (JSON, Avro).

## Architecture:

A Producer simulates a frontend sending events (PageView, ButtonClick, Purchase) with a key = UserId.

A topic user-events with multiple partitions.

## Tasks:

1. Create C# classes for events (UserEvent).
2. Send messages, serializing them into JSON. Ensure that all events for the same UserId go to the same partition (thanks to the key).
3. Write a Consumer that deserializes the JSON and aggregates user statistics (e.g., counts events).

4. *Advanced Task*: Use Confluent Schema Registry (available in Confluent Docker images) and Avro serialization to ensure a data contract.

What you will learn: Key-Value messages, ordering guarantees within a partition, serialization/deserialization, Schema Registry.