# How to run kafka localy with only docker

Start using kafka in docker by this tutorial: https://docs.docker.com/guides/kafka/

1. Run kafka by `docker run -d --name=kafka -p 9092:9092 apache/kafka`
2. Create topic by `docker exec -ti kafka /opt/kafka/bin/kafka-console-producer.sh --bootstrap-server :9092 --topic demo`. 
Then in new line put some string input to produce messages to topic. 
3. Read your messages by consuming `docker exec -ti kafka /opt/kafka/bin/kafka-console-consumer.sh --bootstrap-server :9092 --topic demo --from-beginning`

# How to run kafka localy with docker-compose

`docker-compose -f ./kafka-compose.yaml up`

Now you can send messages to localhost:9092. And use web application kafka-ui on localhost:8080