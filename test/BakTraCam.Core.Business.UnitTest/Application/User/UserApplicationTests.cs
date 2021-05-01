using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoBogus;
using BakTraCam.Core.Business.Application.User;
using BakTraCam.Core.Business.Domain.User;
using BakTraCam.Core.DataAccess.UnitOfWork;
using BakTraCam.Service.DataContract;
using FluentAssertions;
using Moq;
using Xunit;

namespace BakTraCam.Core.Business.UnitTest.Application.User
{
    public class UserApplicationTests:IDisposable
    {
        private readonly MockRepository _mockRepository;

        private readonly Mock<IDatabaseUnitOfWork> _mockDatabaseUnitOfWork;
        private readonly Mock<IUserDomain> _mockUserDomain;

        public UserApplicationTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _mockDatabaseUnitOfWork = _mockRepository.Create<IDatabaseUnitOfWork>();
            _mockUserDomain = _mockRepository.Create<IUserDomain>();
        }

        private UserApplication CreateUserApplication()
        {
            return new(
                _mockDatabaseUnitOfWork.Object,
                _mockUserDomain.Object);
        }

        [Fact]
        public async Task KullaniciListesiGetirAsync_ExpectedBehavior()
        {
            // Arrange
            var userApplication = CreateUserApplication();
            _mockUserDomain
                .Setup(st => st.KullanicilariGetirAsync())
                .ReturnsAsync(It.IsAny<IEnumerable<UserModel>>());

            // Act
            var result = await userApplication.KullaniciListesiGetirAsync();

            // Assert
            result
                .Should()
                .As<UserModel>();
        }

        [Fact]
        public async Task KaydetKullaniciAsync_ExpectedBehavior()
        {
            // Arrange
            var userApplication = CreateUserApplication();
            var kullaniciModel = AutoFaker.Generate<UserModel>();
            _mockUserDomain
                .Setup(st => st.KaydetKullaniciAsync(It.IsAny<UserModel>()))
                .ReturnsAsync(It.IsAny<UserModel>());

            // Act
            var result = await userApplication.KaydetKullaniciAsync(
                kullaniciModel);

            // Assert
            result
                .Should()
                .As<UserModel>();
        }

        [Fact]
        public async Task SilKullaniciAsync_ExpectedBehavior()
        {
            // Arrange
            var userApplication = CreateUserApplication();
            var id = 0;
            _mockUserDomain
                .Setup(st => st.SilKullaniciAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<int>());

            // Act
            var result = await userApplication.SilKullaniciAsync(id);

           // Assert
           result
               .Should()
               .As<int>();
        }

        [Fact]
        public async Task KullaniciGetirAsync_ExpectedBehavior()
        {
            // Arrange
            var userApplication = CreateUserApplication();
            var id = 0;
            _mockUserDomain
                .Setup(st => st.KullaniciGetirAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<UserModel>());
            // Act
            var result = await userApplication.KullaniciGetirAsync(id);

            // Assert
            result
                .Should()
                .As<UserModel>();
        }

        public void Dispose()
        {
            _mockUserDomain.VerifyAll();
            
        }
    }
}
