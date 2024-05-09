using CineQuebec.Windows.DAL.Data;
using Moq;

namespace CineQuebec.WindowsTests.ReservationTest;

[TestClass]
public class ReservationServiceTest
{
    private Mock<ReservationRepository> _mockReservationRepository;

    [TestInitialize]
    public void Init()
    {
        _mockReservationRepository = new Mock<ReservationRepository>();
    }

    [TestMethod]
    public async Task AddReservation_Success()
    {
        //Arrange
        string email = "john.doe@example.com";
        // var expectedReservation = new Reservation( new Abonne
        // {
        //     Username = "John Doe", Email = email, DateAdhesion = new DateTime(2010, 7, 16)
        // });
        //
    }
    
}

public class ReservationRepository
{
}

