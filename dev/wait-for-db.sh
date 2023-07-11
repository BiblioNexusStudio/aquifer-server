#!/bin/bash

if timeout 75s bash -c 'until yarn query "select 1" &> /dev/null; do sleep 1; done' ; then
    echo "Database up"
    exit
else
    echo "Error waiting for database"
    docker-compose logs
    exit 1
fi
