# Task 2 - "Microservices Logging" — Practical Scenario

Goal: To simulate real-world usage for log collection.

Architecture:

Several "microservices" (Console Apps) that generate "logs" (messages with INFO, WARN, ERROR levels) at random intervals.

A centralized Consumer (Log Aggregator) that reads all logs and:

Writes them to a file.

Or outputs ERROR logs to a separate window/file in red color.

Create a topic named app-logs with multiple partitions.

Tasks:

Implement saturation (produce messages faster than they are consumed). Observe how messages accumulate in the topic.

Launch 2-3 instances of the Consumer-aggregator in the same consumer group. Verify that the load is distributed among them (partitions are reallocated).

Stop one Consumer and observe rebalancing.

What you will learn: Consumer Groups, Rebalance, distributed processing, partitions.

# End words

 Make sure your topic has right partition count in your topic. Partition count = count of app in one consumer group which will proccess messages.
 You can change partition count in kafka-ui. 
 Or command:
 `docker exec -ti kafka /opt/kafka/bin/kafka-topics.sh --alter --topic logs --partitions 2 --bootstrap-server localhost:9092`
 will create 2 partitions. 