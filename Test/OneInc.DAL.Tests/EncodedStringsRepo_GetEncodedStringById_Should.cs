using Moq;
using System.Text.Json;
using OneInc.Common.Model;
using OneInc.DAL.Repository;
using StackExchange.Redis;

namespace OneInc.DAL.Tests
{
    public class EncodedStringsRepo_GetEncodedStringById_Should
    {
        private readonly Mock<IConnectionMultiplexer> _mockConnMul;

        public EncodedStringsRepo_GetEncodedStringById_Should()
        {
            _mockConnMul = new Mock<IConnectionMultiplexer>();
        }

        [Fact]
        public void ThrowArgunmentOutOfRangeWhenEncStrIsNull()
        {
            //Arrange

            var sut = new EncodedStringsRepo(_mockConnMul.Object);

            //Act & Assert

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.CreatedEncodeString(null));
        }


        [Fact]
        public void ReturnValidEncodedStringObjWhenFound()
        {
            //Arrange

            var mockMultiplexer = new Mock<IConnectionMultiplexer>();

            mockMultiplexer.Setup(_ => _.IsConnected).Returns(false);

            var mockDatabase = new Mock<IDatabase>();

            mockMultiplexer
                .Setup(_ => _.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(mockDatabase.Object);

            var obj = new EncodedString() { Id = "IdValue", EncodedValue = "EncodedValue" };

            RedisValue expecteValue = JsonSerializer.Serialize(obj);

            mockDatabase.Setup(db => db.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
            .Returns(expecteValue);

            var sut = new EncodedStringsRepo(mockMultiplexer.Object);

            //Act 
            var res = sut.GetEncodedStringById("anyValue");

            //Assert

            Assert.NotNull(res);
            Assert.Equal(obj.Id, res.Id);
            Assert.Equal(obj.EncodedValue, res.EncodedValue);
            mockDatabase.Verify(x => x.StringGet(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()), Times.Once());
        }
    }
}
