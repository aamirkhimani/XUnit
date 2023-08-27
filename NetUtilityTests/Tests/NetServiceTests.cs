using System;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetUtility.DNS;
using NetUtility.Objects;
using NetUtility.Services;

namespace NetUtilityTests.Tests
{
    public class NetServiceTests
    {
        private readonly IDNS _dNs;
        private readonly NetService _NetService;

        public NetServiceTests()
        {
            //Using FakeItEasy for creating fake Dependencies
            _dNs = A.Fake<IDNS>();

            //SUT
            _NetService = new NetService(_dNs);
        }

        [Fact]
        public void NetServices_Ping_ReturnString()
        {
            //Arrange
            A.CallTo(() => _dNs.SendDNS()).Returns(true);

            //Act
            var result = _NetService.SendPing();

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().Contain("Success");
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(3, 4, 7)]
        public void NetServices_PingTimeout_ReturnInt(int time, int space, int output)
        {
            //Arrange

            //Act
            var result = _NetService.PingTimeout(time, space);

            //Assert
            result.Should().Be(output);
            result.Should().BeGreaterThan(2);
            result.Should().NotBeInRange(-100000, 0);
        }

        [Fact]
        public void NetService_LastPingDate_ReturnDateTime()
        {
            //Arrange

            //Act
            var result = _NetService.LastPingDate();

            //Assert
            result.Should().BeAfter(1.January(2021));
            result.Should().BeBefore(1.December(2023));
        }

        [Fact]
        public void NetService_GetPingOptions_ReturnsObject()
        {
            //Arrange
            var pingOptions = new PingOptions()
            {
                IsAvailable = true,
                Ttl = 1
            };

            //Act
            var result = _NetService.GetPingOptions();

            //Assert
            result.Should().BeEquivalentTo(pingOptions);
            result.IsAvailable.Should().BeTrue();
        }

        [Fact]
        public void NetService_GetPingOptionsList_GetIEnumerable()
        {
            //Arrange
            var pingOptiontObj = new PingOptions()
            {
                IsAvailable = true,
                Ttl = 2
            };

            //Act
            var result = _NetService.GetPingOptionsList();

            //Assert
            result.Should().AllBeOfType<PingOptions>();
            result.Should().ContainEquivalentOf(pingOptiontObj);
            result.Should().Contain(x => x.IsAvailable == true);
        }
    }
}

