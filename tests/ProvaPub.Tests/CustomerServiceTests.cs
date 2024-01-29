using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Tests
{
    [TestFixture]
    public class PurchaseServiceTests
    {
        private Mock<TestDbContext> _dbContextMock;
        private CustomerService _customerService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "Teste")
            .Options;

            _dbContextMock = new Mock<TestDbContext>(options);
            _customerService = new CustomerService(_dbContextMock.Object);

        }

        [Test]
        public async Task CustomerIdEqualsOrLessThenZero_ShouldThrowException()
        {
            //Arrange
            var customerId = 0;
            var purchaseValue = 50.0m;

            //Assert 
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _customerService.CanPurchase(customerId, purchaseValue));
        }

        [Test]
        public async Task PurchaseValueEqualsOrLessThenZero_ShouldThrowException()
        {
            //Arrange
            var customerId = 2;
            var purchaseValue = 0.0m;

            //Assert 
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _customerService.CanPurchase(customerId, purchaseValue));
        }

        [Test]
        public async Task CustomerNotFound_ShouldThrowException()
        {
            // Arrange
            var customerId = 1;
            var purchaseValue = 1.0m;

            var dbSetCustomer = new Mock<DbSet<Customer>>();

            dbSetCustomer.Setup(x => x.FindAsync(It.Is<int>(q => q == customerId))).ReturnsAsync(null as Customer);

            _dbContextMock.Setup(x => x.Customers).Returns(dbSetCustomer.Object);

            //Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _customerService.CanPurchase(customerId, purchaseValue));

        }

        [Test]
        public async Task CustomerHasOrderInThisMonth_ShouldReturnFalse()
        {
            // Arrange
            var customerId = 1;
            var purchaseValue = 1.0m;

            var customer = new Customer()
            {
                Id = customerId,
                Name = "CustomerMocked"
            };

            var dbSetCustomer = new Mock<DbSet<Customer>>();

            dbSetCustomer.Setup(x => x.FindAsync(It.Is<int>(q => q == customerId))).ReturnsAsync(customer);
            _dbContextMock.Setup(x => x.Customers).Returns(dbSetCustomer.Object);

            var order = new Order()
            {
                OrderDate = DateTime.Now.AddDays(-0),
                CustomerId = customerId,
            };

            var orders = new List<Order>()
            {
                order
            };

            var dbSetOrder = orders.AsQueryable().BuildMockDbSet();
            _dbContextMock.Setup(x => x.Orders).Returns(dbSetOrder.Object);

            var result = await _customerService.CanPurchase(customerId, purchaseValue);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task CustomerHasNoOrderAndPurchaseValueLessThenAHundred_ShouldReturnFalse()
        {
            // Arrange
            var customerId = 1;
            var purchaseValue = 1.0m;

            var customer = new Customer()
            {
                Id = customerId,
                Name = "CustomerMocked"
            };

            var dbSetCustomer = new Mock<DbSet<Customer>>();

            dbSetCustomer.Setup(x => x.FindAsync(It.Is<int>(q => q == customerId))).ReturnsAsync(customer);
            _dbContextMock.Setup(x => x.Customers).Returns(dbSetCustomer.Object);

            var order = new Order()
            {
                OrderDate = DateTime.Now.AddDays(-0),
                CustomerId = customerId,
            };

            var orders = new List<Order>()
            {
                order
            };

            var dbSetOrder = orders.AsQueryable().BuildMockDbSet();
            _dbContextMock.Setup(x => x.Orders).Returns(dbSetOrder.Object);

            var result = await _customerService.CanPurchase(customerId, purchaseValue);

            Assert.That(result, Is.False);
        }
    }
}
