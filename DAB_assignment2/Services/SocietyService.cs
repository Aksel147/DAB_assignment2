using DAB_assignment2.Models;
using MongoDB.Driver;

namespace DAB_assignment2.Services;

public class SocietyService
{
    private IMongoCollection<Society> _societies;
    private readonly string collection = "Societies";

    public SocietyService(string dBString, string connString)
    {
        var client = new MongoClient(connString);
        var database = client.GetDatabase(dBString);
        _societies = database.GetCollection<Society>(collection);
    }

    public List<Society> GetByActivity()
    {
        return _societies.Find(Society => true).ToList().OrderBy(s => s.Activity).ToList();
    }

    public List<Society> GetByKeyResponsible(string phoneNumber)
    {
        return _societies.Find(s => s.KeyResponsible.PhoneNumber == phoneNumber).ToList();
    }

    public Society Create(Society Society)
    {
        _societies.InsertOne(Society);
        return Society;
    }

    public void Update(string id, Society SocietyIn)
    {
        _societies.ReplaceOne(Society => Society.Id == id, SocietyIn);
    }

    public void Remove(Society SocietyIn)
    {
        _societies.DeleteOne(Society => Society.Id == SocietyIn.Id);
    }
}