using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Queries;

[TestClass]
public class StudentQueriesTests
{
    private IList<Student> _students;

    public StudentQueriesTests()
    {
        _students = new List<Student>();
        
        for (byte i = 0; i <= 10; i++)
        {
            _students.Add(new Student(
                new Name("Aluno", i.ToString()), 
                new Document("8888888888" + i.ToString(), EDocumentType.CPF), 
                new Email(i.ToString() + "@wayne.com")));
        }
    }
    
    [TestMethod]
    public void ShouldReturnNullWhenDocumentNotExits()
    {
        var exp = StudentsQueries.GetStudentInfo("12345678910");
        var student = _students.AsQueryable().Where(exp).FirstOrDefault();

        Assert.AreEqual(null, student);
    }
    
    [TestMethod]
    public void ShouldReturnStudentWhenDocumentExits()
    {
        var exp = StudentsQueries.GetStudentInfo("88888888880");
        var student = _students.AsQueryable().Where(exp).FirstOrDefault();

        Assert.AreNotEqual(null, student);
    }
}