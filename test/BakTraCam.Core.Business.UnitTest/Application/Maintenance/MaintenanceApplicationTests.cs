using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoBogus;
using BakTraCam.Core.Business.Application.Maintenance;
using BakTraCam.Core.Business.Domain.Maintenance;
using BakTraCam.Core.DataAccess.UnitOfWork;
using BakTraCam.Service.DataContract;
using FluentAssertions;
using Moq;
using Xunit;

namespace BakTraCam.Core.Business.UnitTest.Application.Maintenance
{
    public class MaintenanceApplicationTests : IDisposable
    {
        private readonly MockRepository _mockRepository;

        private readonly Mock<IDatabaseUnitOfWork> _mockDatabaseUnitOfWork;
        private readonly Mock<IMaintenanceDomain> _mockMaintenanceDomain;

        public MaintenanceApplicationTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _mockDatabaseUnitOfWork = _mockRepository.Create<IDatabaseUnitOfWork>();
            _mockMaintenanceDomain = _mockRepository.Create<IMaintenanceDomain>();
        }

        private MaintenanceApplication CreateMaintenanceApplication()
        {
            return new(
                _mockDatabaseUnitOfWork.Object,
                _mockMaintenanceDomain.Object);
        }

        [Fact]
        public async Task OnBesGunYaklasanBakimlariGetirAsync_ExpectedBehavior()
        {
            // Arrange
            var maintenanceApplication = CreateMaintenanceApplication();
            _mockMaintenanceDomain
                .Setup(st => st.OnBesGunYaklasanBakimlariGetirAsync())
                .ReturnsAsync(It.IsAny<IEnumerable<MaintenanceModelBasic>>());

            // Act
            var result = await maintenanceApplication.OnBesGunYaklasanBakimlariGetirAsync();

            // Assert
            result
                .Should()
                .As<IEnumerable<MaintenanceModelBasic>>();
        }

        [Fact]
        public async Task BakimlistesiGetirAsync_ExpectedBehavior()
        {
            // Arrange
            var maintenanceApplication = CreateMaintenanceApplication();
            _mockMaintenanceDomain
                .Setup(st => st.BakimlariGetirAsync())
                .ReturnsAsync(It.IsAny<IEnumerable<MaintenanceModelBasic>>());

            // Act
            var result = await maintenanceApplication.BakimlistesiGetirAsync();

            // Assert
            result
                .Should()
                .As<IEnumerable<MaintenanceModelBasic>>();
        }

        [Fact]
        public async Task GetirBakimAsync_ExpectedBehavior()
        {
            // Arrange
            var maintenanceApplication = CreateMaintenanceApplication();
            var id = 0;
            _mockMaintenanceDomain
                .Setup(st => st.GetirBakimAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<MaintenanceModel>());

            // Act
            var result = await maintenanceApplication.GetirBakimAsync(id);

            // Assert
            result
                .Should()
                .As<MaintenanceModel>();
        }

        [Fact]
        public async Task GetirBakimListesiTipFiltreliAsync_ExpectedBehavior()
        {
            // Arrange
            var maintenanceApplication = CreateMaintenanceApplication();
            var tip = 0;
            _mockMaintenanceDomain
                .Setup(st => st.getirBakimListesiTipFiltreliAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<IEnumerable<MaintenanceModelBasic>>());
            // Act
            var result = await maintenanceApplication.GetirBakimListesiTipFiltreliAsync(
                tip);

            // Assert
            result
                .Should()
                .As<IEnumerable<MaintenanceModelBasic>>();
        }

        [Fact]
        public async Task GetirBakimListesiDurumFiltreliAsync_ExpectedBehavior()
        {
            // Arrange
            var maintenanceApplication = CreateMaintenanceApplication();
            var durum = 0;
            _mockMaintenanceDomain
                .Setup(st => st.getirBakimListesiDurumFiltreliAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<IEnumerable<MaintenanceModelBasic>>());

            // Act
            var result = await maintenanceApplication.GetirBakimListesiDurumFiltreliAsync(
                durum);

            // Assert
            result
                .Should()
                .As<IEnumerable<MaintenanceModelBasic>>();
        }

        [Fact]
        public async Task KaydetBakimAsync_ExpectedBehavior()
        {
            // Arrange
            var maintenanceApplication = CreateMaintenanceApplication();
            var bakimModel = AutoFaker.Generate<MaintenanceModel>();
            _mockMaintenanceDomain
                .Setup(st => st.KaydetBakimAsync(It.IsAny<MaintenanceModel>()))
                .ReturnsAsync(It.IsAny<MaintenanceModel>());

            // Act
            var result = await maintenanceApplication.KaydetBakimAsync(
                bakimModel);

            // Assert
            result
                .Should()
                .As<MaintenanceModel>();
        }

        [Fact]
        public async Task SilBakimAsync_ExpectedBehavior()
        {
            // Arrange
            var maintenanceApplication = CreateMaintenanceApplication();
            var id = 0;
            _mockMaintenanceDomain
                .Setup(st => st.SilBakimAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<int>());

            // Act
            var result = await maintenanceApplication.SilBakimAsync(id);

            // Assert
            result
                .Should()
                .As<int>();
        }

        public void Dispose()
        {
            _mockRepository.VerifyAll();
        }
    }
}
