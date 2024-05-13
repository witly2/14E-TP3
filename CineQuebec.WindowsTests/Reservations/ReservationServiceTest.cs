using CineQuebec.Windows.BLL.Services.Reservations;
using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Exceptions;
using CineQuebec.Windows.DAL.Repositories.Reservation;
using MongoDB.Bson;
using Moq;

namespace CineQuebec.WindowsTests.Reservations;
[TestClass()]
public class ReservationServiceTest
{
    private ReservationService _reservationService;
    private Mock<IReservationRepository> _mockReservationRepository;
    
    [TestInitialize]
    public void Init()
    {
        _mockReservationRepository = new Mock<IReservationRepository>();
        _reservationService = new ReservationService(_mockReservationRepository.Object );
        
        
       
        
    }
    
    [TestMethod]
    public void AddReservation_ValidReservation_ShouldCallAddReservationInRepository()
    {
        // Arrange
        var reservation = new Reservation
        {
         
            Projection = new Projection(new DateTime(2020,3,10),new Salle(5,200),new Film("Inception","Inception","Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.",148,new DateTime(2010,7,16),8)),
            NombreBillets = 2,
            Abonne = new Abonne
            {
                Nom = "wilo",
                Prenom = "wilo",
                Email = "wil@test.com"
            }
        };
        _mockReservationRepository.Setup(repo => repo.AddReservation(reservation)).Returns(Task.CompletedTask);

        // Act
        _reservationService.AddReservation(reservation);

        // Assert
        _mockReservationRepository.Verify(repo => repo.AddReservation(reservation), Times.Once);
    }
    

    
    
    

    [TestMethod]
    public void AddReservation_ExistingReservation_ShouldThrowReservationExisteException()
    {
        // Arrange
        var reservation = new Reservation
        {
            Projection = new Projection(new DateTime(2020, 3, 10), new Salle(5, 200), new Film("Inception", "Inception", "Un voleur qui entre dans les rêves des autres pour voler leurs secrets de leur subconscient.", 148, new DateTime(2010, 7, 16), 8)),
            NombreBillets = 2,
            Abonne = new Abonne
            {
                Nom = "wilo",
                Prenom = "wilo",
                Email = "wil@test.com"
            },
        };
        _mockReservationRepository.Setup(repo => repo.AddReservation(reservation))
            .Throws(new ReservationExisteException("Vous avez deja une reservation pour cette projection"));

        // Act & Assert
        _reservationService.AddReservation(reservation);
    }
}