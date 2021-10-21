# scalable-counter
> A GENEPLANET tech assignment

## Task description

Create a system of services where each request to the services is counted and stored into DB.
System should consist of one DB instance, N instances of counter services and a load balancer.

## Solution overview

- Load balancer: [Traefik](https://traefik.io/traefik/)
- Database: [Cassandra](https://cassandra.apache.org)

## Prerequisites

- .NET 5.0
- Docker

## Usage

Run the following `docker-compose` command in the root directory of the solution:

`docker-compose up -d --scale scalable-counter=3`

Docker will automatically build and setup everything for you.

When containers are up and running you can access the API:

`curl --location --request GET 'http://localhost/scalable-counter/v1/Counters'`

This should produce a result of the following form:

```json
[
  {
    "id":"192.168.48.4",
    "count":16
  },
  {
    "id":"192.168.48.9",
    "count":20
  },
  {
    "id":"192.168.48.7",
    "count":21
  }
]
```