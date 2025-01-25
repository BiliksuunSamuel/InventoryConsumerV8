import json
from kafka import KafkaProducer

producer = KafkaProducer(bootstrap_servers=['127.0.0.1:9092'], api_version=(0, 10),
                         value_serializer=lambda m: json.dumps(m).encode('ascii'))
# produce asynchronously
i = 0
while i < 1:
    producer.send('inventory-update', value={
        "Quantity": 100,
        "Location": "Warehouse B",
        "Description": "Updated inventory item",
        "Category": "Electronics",
        "ImageUrl": "http://example.com/updated_image.jpg",
        "Price": 149.99,
        "Name": "Updated Item",
        "UpdatedBy": "admin",
        "Id":"459802e5c7fa456b819d5bee8407479a"
    }
                  )
    i = i + 1
    print("produced")
producer.flush()
