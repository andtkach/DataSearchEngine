# DataSearchEngine

Searching through one million records is very fast thanks to Elasticsearch and asynchronous data mutation operations

Demo: https://youtu.be/COaL6vhwMKA

Data Catalog with employee records
- Create, Update, and Delete data
- Read individual record details
- Search over all catalog
- Frontend to work with data catalog

The estimated data catalog size is about 1 mln records

Data record structure
- First name
- Last name
- Email address
- Country
- Some personal info
(Search over all catalog fields)

Infrastructure
1. Run Databases in docker\DockerDatabases
2. Run Search Engine in docker\DockerElasticsearch