using OneInc.Common.Model;

namespace OneInc.Common.Tests
{
    public class EncodedStringShould
    {
        public EncodedStringShould()
        {

        }

        [Fact]
        public void BeValidModel()
        {
            //Arrange
            var sut = new EncodedString() { Id = "IdValue", EncodedValue = "EncodedValue" };

            //Act

            //Assert
            Assert.True(sut != null);
            Assert.Equal("IdValue", sut.Id);
            Assert.Equal("EncodedValue", sut.EncodedValue);
        }
    }
}