﻿using DataFlowWebservice.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DataFlowWebservice.Controllers
{
    public class EventsController : ApiController
    {
        private MongoDatabase database;

        public EventsController()
        {
            // Bouwt een connection string (optie 1)
            MongoServerSettings settings = new MongoServerSettings();
            settings.Server = new MongoServerAddress("localhost", 27017); //145.24.222.160
            // Maakt een MongoDB server object, met dit object kunnen we communiceren met de server. Het maakt kortom verbinding met de mongodb server
            MongoServer server = new MongoServer(settings);
            // Vraagt onze database op van de server, zodat we read/write kunnen toepassen
            database = server.GetDatabase("Dataflow");


            //Declareerd een directe connection string (optie 2)
            //string connectionString = "mongodb://localhost"; //145.24.222.160:27017
            //var client = new MongoClient(connectionString);
            //database = client.GetDatabase("Dataflow");
        }
        // GET: api/Events
        public IEnumerable<Event> Get()
        {
            MongoCursor<Event> cursor = database.GetCollection<Event>("events").FindAll();
            return cursor.ToList();
        }

        // GET: api/Events/5
        public IEnumerable<Event> Get(int id)
        {
            IMongoQuery query = Query<Event>.EQ(e => e.unitId, id); // Gebruikt event (e), van e check hij of het unitId en het opgegeven id hetzelfde zijn (EQ)
            return database.GetCollection<Event>("events").Find(query);
        }

        // POST: api/Events
        public void Post([FromBody]Event document)
        {
            var collection = database.GetCollection<BsonDocument>("events");
            collection.Insert(document);
        }

        // PUT: api/Events/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Events/5
        public void Delete(int id)
        {
        }
    }
}
