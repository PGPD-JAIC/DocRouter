using DocRouter.Domain.Entities;
using DocRouter.Domain.Exceptions.Entities.SubmissionItem;
using Shouldly;
using Xunit;

namespace DocRouter.Domain.UnitTests.Entities
{
    public class SubmissionItemTests
    {
        [Fact]
        public void Given_Valid_Values_Can_Create_SubmissionItem()
        {
            // Arrange
            var name = "Test Item #1";
            var path = @"C:\DocRouter\Test\Test Item #1";
            var itemId = "12345ABCDE";
            var driveId = "67890FGHIJ";

            // Act
            var toTest = new SubmissionItem(name, path, itemId, driveId);

            // Assert
            toTest.ItemName.ShouldBe(name);
            toTest.ItemUri.ShouldBe(path);
            toTest.ItemId.ShouldBe(itemId);
            toTest.DriveId.ShouldBe(driveId);
                
        }
        [Theory]
        [InlineData("")]
        [InlineData("11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111")]
        public void Given_Invalid_Name_Throws_SubmissionItemException(string value)
        {
            // Arrange
            var name = value;
            var path = @"C:\DocRouter\Test\Test Item #1";
            var itemId = "12345ABCDE";
            var driveId = "67890FGHIJ";

            // Act/Assert
            Assert.Throws<SubmissionItemArgumentException>(() => new SubmissionItem(name, path, itemId, driveId));
        }
        [Theory]
        [InlineData("")]
        [InlineData("11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111")]
        public void Given_Invalid_Path_Throws_SubmissionItemException(string value)
        {
            // Arrange
            var name = "Test Item #1";
            var path = value;
            var itemId = "12345ABCDE";
            var driveId = "67890FGHIJ";

            // Act/Assert
            Assert.Throws<SubmissionItemArgumentException>(() => new SubmissionItem(name, path, itemId, driveId));
        }
    }
}
