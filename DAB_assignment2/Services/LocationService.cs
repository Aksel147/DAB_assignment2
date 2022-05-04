using DAB_assignment2.Models;
using MongoDB.Driver;

namespace DAB_assignment2.Services;

public class LocationService
{
    private IMongoCollection<Location> _locations;
    private readonly string collection = "Locations";

    public LocationService(string dBString, string connString)
    {
        var client = new MongoClient(connString);
        var database = client.GetDatabase(dBString);
        _locations = database.GetCollection<Location>(collection);
    }

    public List<Location> Get()
    {
        return _locations.Find(Location => true).ToList();
    }

    public Location Get(string id)
    {
        return _locations.Find(Location => Location.Id == id).FirstOrDefault();
    }

    public Location Create(Location Location)
    {
        _locations.InsertOne(Location);
        return Location;
    }

    public void Update(string id, Location LocationIn)
    {
        _locations.ReplaceOne(Location => Location.Id == id, LocationIn);
    }

    public void Remove(Location LocationIn)
    {
        _locations.DeleteOne(Location => Location.Id == LocationIn.Id);
    }
}