using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoBogus;
using BakTraCam.Core.Business.Application.Common;
using BakTraCam.Core.Business.Domain.Common;
using BakTraCam.Core.DataAccess.UnitOfWork;
using BakTraCam.Service.DataContract;
using FluentAssertions;
using Moq;
using Xunit;

namespace BakTraCam.Core.Business.UnitTest.Application.Common
{
    public class CommonApplicationTests:IDisposable
    
    {
        private readonly MockRepository _mockRepository;

        private readonly Mock<IDatabaseUnitOfWork> _mockDatabaseUnitOfWork;
        private readonly Mock<ICommonDomain> _mockCommonDomain;

        public CommonApplicationTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _mockDatabaseUnitOfWork = _mockRepository.Create<IDatabaseUnitOfWork>();
            _mockCommonDomain = _mockRepository.Create<ICommonDomain>();
        }

        private CommonApplication CreateCommonApplication()
        {
            return new(_mockDatabaseUnitOfWork.Object, _mockCommonDomain.Object);
        }

        [Fact]
        public async Task KullaniciListesiGetirAsync_ExpectedBehavior()
        {
            // Arrange
            var commonApplication = CreateCommonApplication();
            _mockCommonDomain
                .Setup(st => st.KullanicilariGetirAsync())
                .ReturnsAsync(It.IsAny<IEnumerable<SelectModel>>());
            // Act
            var result = await commonApplication.KullaniciListesiGetirAsync();

            // Assert
            result
                .Should()
                .As<IEnumerable<SelectModel>>();
        }

        [Fact]
        public async Task DuyuruListesiGetirAsync_ExpectedBehavior()
        {
            // Arrange
            var commonApplication = CreateCommonApplication();
            _mockCommonDomain
                .Setup(st => st.DuyurulariGetirAsync())
                .ReturnsAsync(It.IsAny<IEnumerable<NoticeModel>>());

            // Act
            var result = await commonApplication.DuyuruListesiGetirAsync();

            // Assert
            result
                .Should()
                .As<IEnumerable<NoticeModel>>();
        }

        [Fact]
        public async Task KaydetDuyuruAsync_ExpectedBehavior()
        {
            // Arrange
            var commonApplication = CreateCommonApplication();
            var noticeModel = AutoFaker.Generate<NoticeModel>();
            _mockCommonDomain
                .Setup(st => st.KaydetDuyuruAsync(noticeModel))
                .ReturnsAsync(It.IsAny<NoticeModel>());

            // Act
            var result = await commonApplication.KaydetDuyuruAsync(noticeModel);

            // Assert
            result
                .Should()
                .As<NoticeModel>();
        }

        [Fact]
        public async Task SilDuyuruAsync_ExpectedBehavior()
        {
            // Arrange
            var commonApplication = CreateCommonApplication();
            var id = 0;
            _mockCommonDomain
                .Setup(st => st.SilDuyuruAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<int>());
            // Act
            var result = await commonApplication.SilDuyuruAsync(id);

            // Assert
            result
                .Should()
                .As<int>();

        }

        [Fact]
        public async Task DuyuruGetirAsync_ExpectedBehavior()
        {
            // Arrange
            var commonApplication = CreateCommonApplication();
            var id = 0;
            _mockCommonDomain
                .Setup(st => st.DuyuruGetirAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<NoticeModel>());


            // Act
            var result = await commonApplication.DuyuruGetirAsync(id);

            // Assert
            result
                .Should()
                .As<NoticeModel>();
        }

        public void Dispose()
        {
            _mockRepository.VerifyAll();
        }
    }
}
