1
docker-compose -f create-certs.yml run --rm create_certs

2
docker-compose up

3
docker cp es01:/usr/share/elasticsearch/config/certificates/ca/ca.crt c:\temp

4
curl --cacert c:\temp\ca.crt -u elastic:password https://localhost:9200

5
Connect Kibana
docker run --name kibana --net devnet -p 5601:5601 docker.elastic.co/kibana/kibana:8.12.0