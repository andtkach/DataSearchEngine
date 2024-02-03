
# Start elastic
docker network create elastic
docker pull docker.elastic.co/elasticsearch/elasticsearch:8.12.0
docker run --name es01 --net elastic -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" -t docker.elastic.co/elasticsearch/elasticsearch:8.12.0

docker network create devnet
docker run --name search --net devnet -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" -e "ELASTIC_PASSWORD=password" -t docker.elastic.co/elasticsearch/elasticsearch:8.12.0

# go to 
https://localhost:9200/

# get password 
elastic | password

# Start kibana
docker pull docker.elastic.co/kibana/kibana:8.12.0
docker run --name kibana --net elastic -p 5601:5601 docker.elastic.co/kibana/kibana:8.12.0
docker run --name kibana --net devnet -p 5601:5601 docker.elastic.co/kibana/kibana:8.12.0

# Go to 
http://0.0.0.0:5601/?code=312069 to get started.
http://localhost:5601/?code=312069

# Insert Kibana token from Elastic
eyJ2ZXIiOiI4LjEyLjAiLCJhZHIiOlsiMTcyLjIwLjAuMjo5MjAwIl0sImZnciI6IjllZGMyMDYyNWQ0NDBjZWFiMjNhMDZmYzdlNDAyNWEzZmZjYTZmMTIxZDAzNDdlOWI3NTEzOTEyZTQ1YTJkMjMiLCJrZXkiOiJ6UHdsWUkwQm9Wbi1wU0hrZHVMTjo4aWVQZkZlM1MxQ2lYazNvMFpjd1Z3In0=

# Login with 
elastic | password

# Go to DevTools Console (Menu -> Management -> Dev Tools)

# Commands
Commands Used:
GET _cat/indices

PUT /product
{
  "mappings": {
    "properties": {
      "name": { "type": "text" },
      "price": { "type": "long" },
      "color": { "type": "text" },
      "dateAdded": { "type": "date" }
    }
  }
}

PUT /person
{
  "mappings": {
    "properties": {
      "firstname": { "type": "text" },
      "lastname": { "type": "text" },
	  "email": { "type": "text" },
	  "country": { "type": "text" },
	  "dob": { "type": "text" },
	  "bio": { "type": "text" }
    }
  }
}


POST product/_doc/
{
  "name":"T-shirt", 
  "price": 15,
  "color": ["Yellow", "Blue"],
  "dateAdded": "2023-03-26T00:00:00"
}
POST person/_doc/
{
  "firstname":"Andrii", 
  "lastname":"Tkach", 
  "email":"andrii@email.com", 
  "country":"Ukraine", 
  "dob":"1981-01-16", 
  "bio":"I love to programm and to travel"
}


POST _bulk
{ "create" : { "_index" : "product" } }
{ "name":"Shoe", "price": 30, "color": ["Brown", "Blue"], "dateAdded": "2023-05-26T00:00:00" }
{ "create" : { "_index" : "product" } }
{ "name":"Jacket", "price": 45, "color": ["Red", "Black"], "dateAdded": "2023-07-26T00:00:00" }
  
GET /product/_search
GET /person/_search

GET /product/_doc/O6dpJYsBaNUsmfLZ8TJR

POST product/_doc/O6dpJYsBaNUsmfLZ8TJR
{
  "name":"T-shirt v2", 
  "price": 18,
  "color": ["Blue"],
  "dateAdded": "2023-03-24T00:00:00"
}

POST /product/_update/O6dpJYsBaNUsmfLZ8TJR
{
  "doc": 
  {
    "price": 20,
    "color": ["Black", "Blue"]
  } 
}

DELETE /product/_doc/O6dpJYsBaNUsmfLZ8TJR

DELETE /product




WEB API URLS
S https://localhost:7498/swagger/index.html
D https://localhost:7298/swagger/index.html
https://localhost:9200/person
http://localhost:9200/person