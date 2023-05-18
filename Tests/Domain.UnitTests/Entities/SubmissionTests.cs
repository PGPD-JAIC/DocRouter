using DocRouter.Domain.Entities;
using DocRouter.Domain.Exceptions.Entities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DocRouter.Domain.UnitTests.Entities
{
    public class SubmissionTests
    {
        [Fact]
        public void Given_Valid_Values_Can_Create_Submission()
        {
            // Arrange
            string title = "Test Title";
            string folderPath = @"C:\DocRouter\Testing";

            // Act
            var toTest = new Submission(title, folderPath);

            // Assert
            toTest.Title.ShouldBe(title);
            toTest.FolderUri.ShouldBe(folderPath);
            toTest.Items.ShouldBeEmpty();
            toTest.Transactions.ShouldBeEmpty();
        }
        [Theory]
        [InlineData("")]
        [InlineData("11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111")]
        public void Given_Invalid_Title_Throws_SubmissionArgumentException(string value)
        {
            // Arrange
            string title = value;
            string folderPath = @"C:\DocRouter\Testing";

            // Act/Assert
            Assert.Throws<SubmissionArgumentException>(() => new Submission(title, folderPath));
        }
        [Theory]
        [InlineData("")]
        [InlineData("11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111")]
        public void Given_Invalid_Path_Throws_SubmissionArgumentException(string value)
        {
            // Arrange
            string title = "Test Title";
            string folderPath = value;

            // Act/Assert
            Assert.Throws<SubmissionArgumentException>(() => new Submission(title, folderPath));
        }
        [Fact]
        public void Can_Add_Item()
        {
            // Arrange
            var toTest = new Submission("Test Title", @"C:\DocRouter\Testing");
            var item = new SubmissionItem("Test Item", @"C:\DocRouter\Testing\Test Item.pdf");

            // Act
            toTest.AddItem(item);

            // Assert
            toTest.Items.Count.ShouldBe(1);
        }
        [Fact]
        public void Can_Remove_Item()
        {
            // Arrange
            var toTest = new Submission("Test Title", @"C:\DocRouter\Testing");
            var item = new SubmissionItem("Test Item", @"C:\DocRouter\Testing\Test Item.pdf");
            toTest.AddItem(item);
            var itemCount = toTest.Items.Count;

            // Act
            toTest.RemoveItem(item);

            // Assert
            itemCount.ShouldBe(1);
            toTest.Items.Count.ShouldBe(0);
        }
        [Fact]
        public void Can_Add_Transaction()
        {
            // Arrange
            var toTest = new Submission("Test Title", @"C:\DocRouter\Testing");
            var transaction = new SubmissionTransaction(
                new DateTime(2023, 1, 1), 
                new DateTime(2023, 1, 1), 
                DocRouter.Common.Enums.TransactionStatus.Pending, 
                "test@mail.com", 
                "Test Comments");

            // Act
            toTest.AddTransaction(transaction);

            // Assert
            toTest.Transactions.Count.ShouldBe(1);
        }
        [Fact]
        public void Can_Remove_Transaction()
        {
            // Arrange
            var toTest = new Submission("Test Title", @"C:\DocRouter\Testing");
            var transaction = new SubmissionTransaction(
                new DateTime(2023, 1, 1),
                new DateTime(2023, 1, 1), 
                DocRouter.Common.Enums.TransactionStatus.Pending, 
                "test@mail.com", 
                "Test Comments");
            toTest.AddTransaction(transaction);
            var transactionCount = toTest.Transactions.Count;

            // Act
            toTest.RemoveTransaction(transaction);

            // Assert
            transactionCount.ShouldBe(1);
            toTest.Transactions.Count.ShouldBe(0);
        }
    }
    
}
