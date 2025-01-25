import json
from kafka import KafkaProducer

producer = KafkaProducer(bootstrap_servers=['127.0.0.1:9092'], api_version=(0, 10),
                         value_serializer=lambda m: json.dumps(m).encode('ascii'))
# produce asynchronously
i = 0
while i < 1:
    producer.send('inventory-delete', value="4af463a3e2694b5491107c9dbaac9782")
    i = i + 1
    print("produced")
producer.flush()
