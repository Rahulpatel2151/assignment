using Moq;
using Newtonsoft.Json;
using PassengerManagement.Controllers;
using PassengerManagement.Data;
using PassengerManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Xunit;

namespace PassengerManagement.Tests
{
    public class PassengerTests
    {
        private readonly Mock<IPassengerRepository> mockPassengerRepository = new Mock<IPassengerRepository>();
        private readonly PassengersController _passengersController;
        public PassengerTests()
        {
            _passengersController = new PassengersController(mockPassengerRepository.Object);
        }
        [Fact]
        public void TestGetPassengers()
        {
            var mockresult = mockPassengerRepository.Setup(x => x.GetPassengers()).Returns(GetPassengers());
            var response = _passengersController.GetPassengers();
            //IList<Passenger> passengers = JsonConvert.DeserializeObject<IList<Passenger>>(response.Content.ReadAsStringAsync().Result);
            var isNull=Assert.IsType<OkNegotiatedContentResult<IList<Passenger>>>(response);
            Assert.NotNull(isNull);
        }
        [Fact]
        public void TestGetPassengerById()
        {
            Passenger passenger = new Passenger();
            passenger.Id = 1;
            passenger.FirstName = "test_firstname";
            passenger.LastName = "test_lastname";
            passenger.Phone = "test_phone";
            var mockresult = mockPassengerRepository.Setup(x => x.GetByPassengerId(passenger.Id)).Returns(passenger);
            var response = _passengersController.GetPassenger(passenger.Id);
            var contentResult = response as OkNegotiatedContentResult<Passenger>;

            var isNull = Assert.IsType<OkNegotiatedContentResult<Passenger>>(response);
            Assert.NotNull(isNull);
            Assert.Equal(passenger.Id, contentResult.Content.Id);
        }
        [Fact]
        public void TestNegativeGetPassengerById()
        {
            Passenger passenger = new Passenger();
            passenger.Id = 1;
            passenger.FirstName = "test_firstname";
            passenger.LastName = "test_lastname";
            passenger.Phone = "test_phone";
            var mockresult = mockPassengerRepository.Setup(x => x.GetByPassengerId(passenger.Id)).Returns(passenger);
            var response = _passengersController.GetPassenger(2);
            var isNull = Assert.IsType<NotFoundResult>(response);
            Assert.NotNull(isNull);
        }
        [Fact]
        public void TestPostPassenger()
        {
            Passenger passenger = new Passenger();
            passenger.Id = 1;
            passenger.FirstName = "test_firstname";
            passenger.LastName = "test_lastname";
            passenger.Phone = "test_phone";
           
            var mockresult = mockPassengerRepository.Setup(x => x.AddPassenger(passenger)).Returns(passenger);
            var response = _passengersController.PostPassenger(passenger);
            var contentResult = response as OkNegotiatedContentResult<Passenger>;

            var isNull = Assert.IsType<OkNegotiatedContentResult<Passenger>>(response);
            Assert.NotNull(isNull);
            Assert.Equal(passenger.Id, contentResult.Content.Id);
        }
        [Fact]
        public void TestNegativePostPassenger()
        {
            Passenger passenger = new Passenger();
            passenger.Id = 1;
            passenger.FirstName = "test_firstname";
            passenger.LastName = "test_lastname";
            passenger.Phone = "test_phone";
            var passenger2 = JsonConvert.DeserializeObject<Passenger>("{\"Id\": 1,\"FirstName\": \"xyz\",\"LastName\": \"Admin\"}");

            var mockresult = mockPassengerRepository.Setup(x => x.AddPassenger(passenger)).Returns(passenger);
            _passengersController.ModelState.AddModelError("Phone","Phone No is Required");
            var response = _passengersController.PostPassenger(passenger2);
            var isNull = Assert.IsType<InvalidModelStateResult>(response);
            Assert.NotNull(isNull);
        }
        [Fact]
        public void TestPutPassenger()
        {
            Passenger passenger = new Passenger();
            passenger.Id = 1;
            passenger.FirstName = "test_firstname";
            passenger.LastName = "test_lastname";
            passenger.Phone = "test_phone";

            var mockresult = mockPassengerRepository.Setup(x => x.EditPassenger(passenger)).Returns(passenger);
            var response = _passengersController.PutPassenger(passenger.Id,passenger);
            var isNull = Assert.IsType<OkNegotiatedContentResult<Passenger>>(response);
            Assert.NotNull(isNull);
        }
        [Fact]
        public void TestNegativePutPassenger()
        {
            Passenger passenger = new Passenger();
            passenger.Id = 1;
            passenger.FirstName = "test_firstname";
            passenger.LastName = "test_lastname";
            passenger.Phone = "test_phone";

            var mockresult = mockPassengerRepository.Setup(x => x.EditPassenger(passenger)).Returns(passenger);
            var response = _passengersController.PutPassenger(2, passenger);
            var isNull = Assert.IsType<BadRequestResult>(response);
            Assert.NotNull(isNull);
        }
        [Fact]
        public void TestDeletePassenger()
        {
            Passenger passenger = new Passenger();
            passenger.Id = 1;
            passenger.FirstName = "test_firstname";
            passenger.LastName = "test_lastname";
            passenger.Phone = "test_phone";

            var mockresult = mockPassengerRepository.Setup(x => x.Delete(passenger.Id)).Returns(true);
            var mockresult2 = mockPassengerRepository.Setup(x => x.GetByPassengerId(passenger.Id)).Returns(passenger);

            var response = _passengersController.DeletePassenger(passenger.Id);
            var isNull = Assert.IsType<OkNegotiatedContentResult<Passenger>>(response);
            Assert.NotNull(isNull);
        }
        [Fact]
        public void TestNegativeDeletePassenger()
        {
            Passenger passenger = new Passenger();
            passenger.Id = 1;
            passenger.FirstName = "test_firstname";
            passenger.LastName = "test_lastname";
            passenger.Phone = "test_phone";

            var mockresult = mockPassengerRepository.Setup(x => x.Delete(passenger.Id)).Returns(true);
            var mockresult2 = mockPassengerRepository.Setup(x => x.GetByPassengerId(passenger.Id)).Returns(passenger);

            var response = _passengersController.DeletePassenger(2);
            var isNull = Assert.IsType<NotFoundResult>(response);
            Assert.NotNull(isNull);
        }
        private static IList<Passenger> GetPassengers()
        {
            IList<Passenger> passengers = new List<Passenger>() { 
                new Passenger(){Id=1,FirstName="testdata1",LastName="testdata1",Phone="testdata1"},
                new Passenger(){Id=2,FirstName="testdata2",LastName="testdata2",Phone="testdata2"},
                new Passenger(){Id=3,FirstName="testdata3",LastName="testdata3",Phone="testdata3"},
                new Passenger(){Id=4,FirstName="testdata4",LastName="testdata4",Phone="testdata4"},
                new Passenger(){Id=5,FirstName="testdata5",LastName="testdata5",Phone="testdata5"}

            };
            return passengers;
        }
    }
}
