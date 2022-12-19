# XLab tech test

This project contains:
 - ASP.NET API
 - Docker container running MongoDB
 - UI running React with Typescript

All tests for both API and UI can be run without the need of the database but if you want to sping everything up to try it with sample data you will need to follow the steps below for the db.

## DB setup

1. run either the `startupdb.ps1` file or `docker-compose up -d` to startup the mongodb instance. It does not need any external volumes or networks created.
2. copy the `data.csv` and `types.txt` files into the container using the following command (change the file as needed) `docker cp [data.csv/headers.txt] {containerId}:/[data.csv/headers.txt]`, to get the container ID run `docker container ls` and copy the ID for the container named `xlabtest_database`
3. you will then need to import the csv data into mongo, this can be done by running the following command: `docker exec xlabtest_database mongoimport --collection='businesses' --file=data.csv --type=csv --fieldsFile=headers.txt --columnsHaveTypes`

### Next you will need to run two aggregations on the data so it plays nicely with the API:
1. open the shell prompt for your container by running `docker exec -it xlabtest_database sh`
2. next open up the mongodb shell prompt by running `mongosh` and use the correct database, `use test` inside the docker shell prompt
3. run the aggregation: `db.businesses.aggregate([
   {
   $project: {
   date: {
   $dateFromString: {
   dateString: "$date",
   },
   },
   tags: {
   $cond: [
   {
   $eq: [
   {
   $type: "$tags",
   },
   "string",
   ],
   },
   {
   $split: ["$tags", ","],
   },
   "$tags",
   ],
   },
   name: "$﻿name",
   category: 1,
   url: 1,
   excerpt: 1,
   thumbnail: 1,
   lat: 1,
   lng: 1,
   address: 1,
   phone: 1,
   twitter: 1,
   stars_beer: 1,
   stars_atmosphere: 1,
   stars_amenities: 1,
   stars_value: 1,
   },
   },
   {
   $addFields: {
   _id: {
   $function: {
   body: function () {
   return UUID();
   },
   args: [],
   lang: "js",
   },
   },
   },
   },
   {
   $out: "businesses",
   },
   ]);`

## Run the API

With the mongodb container running open up your IDE and start the API using the `Xlab.Test.Api` profile. (Or run using `dotnet run` in the terminal)

## Run the UI

To run the UI follow these steps:

1. Navigate to `xlab.test.ui` in a terminal window
2. Run either `npm i` or `yarn` to install the required packages
3. Run `npm run dev` or `yarn dev` to run the development server version of the UI

To test the UI run the `npm test` or `yarn test` commands to open cypress, once in cypress navigate to "E2E" and open it with your preferred browser. Once open run the App.cy spec file. 
