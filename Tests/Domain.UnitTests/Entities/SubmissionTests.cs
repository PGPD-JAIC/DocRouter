using DocRouter.Common;
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
        private readonly IDateTime _dateTime;
        public SubmissionTests(IDateTime dateTime)
        { 
            _dateTime = dateTime;   
        }    
        [Fact]
        public void Given_Valid_Values_Can_Create_Submission()
        {
            // Arrange
            string title = "Test Title";
            string description = "This is a test description.";
            string driveId = @"C:\DocRouter\Testing";
            string listId = "12345ABCDE";
            SubmissionTransaction initial = new SubmissionTransaction(
                _dateTime.Now,
                _dateTime.Now,
                "Recipient",
                "Sender",
                "Test Comments"
                );
            

            // Act
            var toTest = new Submission(title, description, driveId, listId, initial);

            // Assert
            toTest.Title.ShouldBe(title);
            toTest.Description.ShouldBe(description);
            toTest.DriveId.ShouldBe(driveId);
            toTest.ListId.ShouldBe(listId);
            toTest.Items.ShouldBeEmpty();
            toTest.Transactions.Count.ShouldBe(1);
        }
        [Theory]
        [InlineData("")]
        [InlineData("11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111")]
        public void Given_Invalid_Title_Throws_SubmissionArgumentException(string value)
        {
            // Arrange
            string title = value;
            string description = "This is a test description.";
            string driveId = @"C:\DocRouter\Testing";
            string listId = "12345ABCDE";
            SubmissionTransaction initial = new SubmissionTransaction(
                _dateTime.Now,
                _dateTime.Now,
                "Recipient",
                "Sender",
                "Test Comments"
                );

            // Act/Assert
            Assert.Throws<SubmissionArgumentException>(() => new Submission(title, description, driveId, listId, initial));
        }
        [Theory]
        [InlineData("")]
        [InlineData("11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111" +
            "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111")]
        public void Given_Invalid_DriveId_Throws_SubmissionArgumentException(string value)
        {
            // Arrange
            string title = "Test Title";
            string description = "This is a test description.";
            string driveId = value;
            string listId = "12345ABCDE";
            SubmissionTransaction initial = new SubmissionTransaction(
                _dateTime.Now,
                _dateTime.Now,
                "Recipient",
                "Sender",
                "Test Comments"
                );

            // Act/Assert
            Assert.Throws<SubmissionArgumentException>(() => new Submission(title, description, driveId, listId, initial));
        }
        [Fact]
        public void Can_Add_Item()
        {
            // Arrange
            string title = "Test Title";
            string description = "This is a test description.";
            string driveId = @"C:\DocRouter\Testing";
            string listId = "12345ABCDE";
            SubmissionTransaction initial = new SubmissionTransaction(
                _dateTime.Now,
                _dateTime.Now,
                "Recipient",
                "Sender",
                "Test Comments"
                );
            var toTest = new Submission(title, description, driveId, listId, initial);
            var item = new SubmissionItem("Test Item", @"C:\DocRouter\Testing\Test Item.pdf", "FGHIJ67890", driveId);

            // Act
            toTest.AddItem(item);

            // Assert
            toTest.Items.Count.ShouldBe(2);
        }
        [Fact]
        public void Can_Remove_Item()
        {
            // Arrange
            string title = "Test Title";
            string description = "This is a test description.";
            string driveId = @"C:\DocRouter\Testing";
            string listId = "12345ABCDE";
            SubmissionTransaction initial = new SubmissionTransaction(
                _dateTime.Now,
                _dateTime.Now,
                "Recipient",
                "Sender",
                "Test Comments"
                );
            var toTest = new Submission(title, description, driveId, listId, initial);
            var item = new SubmissionItem("Test Item", @"C:\DocRouter\Testing\Test Item.pdf", "FGHIJ67890", driveId);
            toTest.AddItem(item);
            var itemCount = toTest.Items.Count;

            // Act
            toTest.RemoveItem(item);

            // Assert
            itemCount.ShouldBe(2);
            toTest.Items.Count.ShouldBe(1);
        }
        [Fact]
        public void Can_Add_Transaction()
        {
            // Arrange
            string title = "Test Title";
            string description = "This is a test description.";
            string driveId = @"C:\DocRouter\Testing";
            string listId = "12345ABCDE";
            SubmissionTransaction initial = new SubmissionTransaction(
                _dateTime.Now,
                _dateTime.Now,
                "Recipient",
                "Sender",
                "Test Comments"
                );
            var toTest = new Submission(title, description, driveId, listId, initial);
            var transaction = new SubmissionTransaction(
                new DateTime(2023, 1, 1), 
                new DateTime(2023, 1, 1), 
                "test@mail.com",
                "test2@mail.com",
                "Test Comments");

            // Act
            toTest.AddTransaction(transaction);

            // Assert
            toTest.Transactions.Count.ShouldBe(2);
        }
        [Fact]
        public void Can_Remove_Transaction()
        {
            // Arrange
            string title = "Test Title";
            string description = "This is a test description.";
            string driveId = @"C:\DocRouter\Testing";
            string listId = "12345ABCDE";
            SubmissionTransaction initial = new SubmissionTransaction(
                _dateTime.Now,
                _dateTime.Now,
                "Recipient",
                "Sender",
                "Test Comments"
                );
            var toTest = new Submission(title, description, driveId, listId, initial);
            var transaction = new SubmissionTransaction(
                new DateTime(2023, 1, 1),
                new DateTime(2023, 1, 1),
                "test@mail.com",
                "test2@mail.com",
                "Test Comments");
            toTest.AddTransaction(transaction);
            var transactionCount = toTest.Transactions.Count;

            // Act
            toTest.RemoveTransaction(transaction);

            // Assert
            transactionCount.ShouldBe(2);
            toTest.Transactions.Count.ShouldBe(1);
        }
    }
    
}
