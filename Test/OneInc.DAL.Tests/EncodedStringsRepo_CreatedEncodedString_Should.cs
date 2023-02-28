using Moq;
using OneInc.Common.Model;
using OneInc.DAL.Repository;
using StackExchange.Redis;

namespace OneInc.DAL.Tests
{
    public class EncodedStringsRepo_CreatedEncodedString_Should
    {
       private readonly Mock<IConnectionMultiplexer> _mockConnMul;

        public EncodedStringsRepo_CreatedEncodedString_Should()
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
        public void ReturnVoidWhenSuccess()
        {

            //Arrange
            
            var sut = new Mock<EncodedStringsRepo>(_mockConnMul.Object);

            var encStr = new EncodedString() { Id = "IdTest", EncodedValue = "EncodedValueTest" };

            //Act // Assert

            sut.Verify();  
        }
    }
}