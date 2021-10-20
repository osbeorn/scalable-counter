CREATE KEYSPACE test WITH replication = {'class':'SimpleStrategy', 'replication_factor' : 2};

USE test;

CREATE TABLE counters
(
    id         int,
    count      counter,
    PRIMARY KEY (id)
);
