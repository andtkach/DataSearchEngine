npx @apideck/postman-to-k6 ../doc/DataSearchEngine.postman_collection.json --separate --iterations 1 -o src/DataSearchEngine.js
k6 run src/DataSearchEngine.js


const N = 10000;

export let options = { 
  maxRedirects: 4, 
  vus: 10,
  iterations: N, 
};

----------------------------------DOCKER----------------------------------

Run:
docker build . -t andreytkach/demo-datasearchengine-tests
docker run --rm -e SUT_URL=http://localhost:8080/ andreytkach/demo-datasearchengine-tests

docker login --username andreytkach
docker push andreytkach/demo-datasearchengine-tests

--------------------------------------------------------------------------