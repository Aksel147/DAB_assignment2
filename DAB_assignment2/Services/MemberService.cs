using DAB_assignment2.Models;
using MongoDB.Driver;

namespace DAB_assignment2.Services;

public class MemberService
{
    private IMongoCollection<Member> _members;
    private readonly string collection = "Members";

    public MemberService(string dBString, string connString)
    {
        var client = new MongoClient(connString);
        var database = client.GetDatabase(dBString);
        _members = database.GetCollection<Member>(collection);
    }

    public List<Member> Get()
    {
        return _members.Find(Member => true).ToList();
    }

    public Member Get(string id)
    {
        return _members.Find(Member => Member.Id == id).FirstOrDefault();
    }

    public Member Create(Member Member)
    {
        _members.InsertOne(Member);
        return Member;
    }

    public void Update(string id, Member MemberIn)
    {
        _members.ReplaceOne(Member => Member.Id == id, MemberIn);
    }

    public void Remove(Member MemberIn)
    {
        _members.DeleteOne(Member => Member.Id == MemberIn.Id);
    }
}