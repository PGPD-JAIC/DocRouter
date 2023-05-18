using DocRouter.Common.Enums;
using DocRouter.Domain.Entities;
using DocRouter.Domain.Exceptions.Entities.SubmissionTransaction;
using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using Xunit;

namespace DocRouter.Domain.UnitTests.Entities
{
    public class SubmissionTransactionTests
    {
        [Fact]
        public void Given_Valid_Values_Can_Create_SubmissionTransaction()
        {
            // Arrange
            DateTime testDate = new DateTime(2023, 1, 1);
            TransactionStatus testStatus = TransactionStatus.Pending;
            string routedTo = "test@mail.net";
            string comments = "These are test comments.";

            // Act
            var toTest = new SubmissionTransaction(testDate, testDate, testStatus, routedTo, comments);

            // Assert
            toTest.TransactionDate.ShouldBe(testDate);
            toTest.Status.ShouldBe(testStatus);
            toTest.Comments.ShouldBe(comments);
            toTest.RoutedTo.ShouldBe(routedTo);
        }
        [Fact]
        public void Given_Invalid_TransactionDate_Throws_SubmissionTransactionArgumentException()
        {
            // Arrange
            DateTime testDate = new DateTime(2023, 1, 1);
            DateTime testCurrentDate = new DateTime(2022, 1, 1);
            TransactionStatus testStatus = TransactionStatus.Pending;
            string routedTo = "test@mail.net";
            string comments = "These are test comments.";

            // Act/Assert
            Assert.Throws<SubmissionTransactionArgumentException>(() => new SubmissionTransaction(testCurrentDate, testDate, testStatus, routedTo, comments));
        }
        [Theory]
        [InlineData("")]
        [InlineData("11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111")]
        public void Given_Invalid_RoutedTo_Throws_SubmissionTransactionArgumentException(string value)
        {
            // Arrange
            DateTime testDate = new DateTime(2023, 1, 1);
            TransactionStatus testStatus = TransactionStatus.Pending;
            string routedTo = value;
            string comments = "These are test comments.";

            // Act/Assert
            Assert.Throws<SubmissionTransactionArgumentException>(() => new SubmissionTransaction(testDate, testDate, testStatus, routedTo, comments));
        }
        [Fact]
        public void Given_Invalid_Comments_Throws_SubmissionTransactionArgumentException()
        {
            // Arrange
            DateTime testDate = new DateTime(2023, 1, 1);
            TransactionStatus testStatus = TransactionStatus.Pending;
            string routedTo = "test@mail.net";
            string comments = new string('a', 5001);

            // Act/Assert
            Assert.Throws<SubmissionTransactionArgumentException>(() => new SubmissionTransaction(testDate, testDate, testStatus, routedTo, comments));
        }
    }
}
