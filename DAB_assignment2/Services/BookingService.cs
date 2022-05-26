using DAB_assignment2.Models;
using MongoDB.Driver;

namespace DAB_assignment2.Services;

public class BookingService
{
    private IMongoCollection<Booking> _bookings;
    private readonly string collection = "Bookings";

    public BookingService(string dBString, string connString)
    {
        var client = new MongoClient(connString);
        var database = client.GetDatabase(dBString);
        _bookings = database.GetCollection<Booking>(collection);
    }

    public List<Booking> GetRooms()
    {
        return _bookings.Find(b => b.Room != null).ToList();
    }

    public List<Booking> GetByKeyResponsible(string phoneNumber)
    {
        return _bookings.Find(Booking => Booking.Society.KeyResponsible.PhoneNumber == phoneNumber).ToList();
    }

    public Booking Create(Booking Booking)
    {
        _bookings.InsertOne(Booking);
        return Booking;
    }

    public void Update(string id, Booking BookingIn)
    {
        _bookings.ReplaceOne(Booking => Booking.Id == id, BookingIn);
    }

    public void Remove(Booking BookingIn)
    {
        _bookings.DeleteOne(Booking => Booking.Id == BookingIn.Id);
    }
}