1
docker-compose -f create-certs.yml run --rm create_certs

2
docker-compose up -d

3
docker cp es01:/usr/share/elasticsearch/config/certificates/ca/ca.crt /

4
curl --cacert c:\temp\ca.crt -u elastic:password https://localhost:9200
