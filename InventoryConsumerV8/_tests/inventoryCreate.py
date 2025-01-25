import json
from kafka import KafkaProducer

producer = KafkaProducer(bootstrap_servers=['127.0.0.1:9092'], api_version=(0, 10),
                         value_serializer=lambda m: json.dumps(m).encode('ascii'))
# produce asynchronously
i = 0
while i < 1:
    producer.send('inventory-create', value={
        "Quantity": 100,
        "Location": "Warehouse A",
        "Description": "Sample inventory item",
        "Category": "Electronics",
        "ImageUrl": "http://example.com/image.jpg",
        "Price": 199.99,
        "Name": "Sample Item",
        "CreatedBy": "admin"
    }
                  )
    i = i + 1
    print("produced")
producer.flush()
