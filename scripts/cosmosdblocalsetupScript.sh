#!/bin/bash
# check if docker is installed
if ! [ -x "$(command -v docker)" ]; then
  echo 'Error: docker is not installed.' >&2
  exit 1
fi

# check if docker is running
if ! docker info >/dev/null 2>&1; then
  echo 'Error: docker is not running.' >&2

  # start docker
  echo 'Starting docker...'
  open --background -a Docker
  while ! docker info >/dev/null 2>&1; do
    sleep 1
  done
fi

echo 'Docker is running.'

# check if cosmosdb-emulator container is already running
if docker ps --format '{{.Names}}' | grep -Eq '^cosmosdb-emulator$'; then
  echo 'Info: cosmosdb-emulator container is already running.' >&2
  exit 0
fi

# check if cosmosdb-emulator container is already exists
if docker ps -a --format '{{.Names}}' | grep -Eq '^cosmosdb-emulator$'; then
  echo 'Info: cosmosdb-emulator container is already exists.' >&2
fi

# check if cosmosdb-emulator image is already exists
if docker images --format '{{.Repository}}' | grep -Eq '^mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator$'; then
  echo 'Info: cosmosdb-emulator image is already exists.' >&2
else
  # pull cosmosdb-emulator image
  docker pull mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator:vnext-preview
fi

# if cosmosdb-emulator container is already exists, start it
if docker ps -a --format '{{.Names}}' | grep -Eq '^cosmosdb-emulator$'; then
  echo 'Starting cosmosdb-emulator container...'
  docker start cosmosdb-emulator
else
  echo 'Info: cosmosdb-emulator container does not exist'
  echo 'Starting cosmosdb-emulator container...'
  docker run --name cosmosdb-emulator --detach --publish 8081:8081 --publish 1234:1234 mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator:vnext-preview
fi

# run cosmosdb-emulator container

# check if cosmosdb-emulator container is running
if docker ps --format '{{.Names}}' | grep -Eq '^cosmosdb-emulator$'; then
  echo 'CosmosDB Emulator is running.'

  # check if cosmosdb-emulator container is ready
  while ! curl --silent --fail --output /dev/null http://localhost:1234/; do
    echo 'Waiting for CosmosDB Emulator to be ready...'
    sleep 5
  done

  echo 'CosmosDB Emulator is ready.'

  # open Data Explorer
  open http://localhost:1234/

  exit 0
else
  echo 'Error: Failed to run cosmosdb-emulator container.' >&2
  exit 1
fi
