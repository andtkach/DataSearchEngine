FROM grafana/k6:latest
ENV SUT_URL=http://localhost:7498
WORKDIR /usr/src/app
COPY . /usr/src/app
CMD ["run", "DataSearchEngine.js"]