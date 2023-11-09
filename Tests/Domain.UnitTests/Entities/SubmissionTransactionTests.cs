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
            string routedTo = "test@mail.net";
            string routedFrom = "test2@mail.net";
            string comments = "These are test comments.";

            // Act
            var toTest = new SubmissionTransaction(testDate, testDate, routedTo, routedFrom, comments);

            // Assert
            toTest.SubmitDate.ShouldBe(testDate);
            toTest.Status.ShouldBe(TransactionStatus.Pending);
            toTest.Comments.ShouldBe(comments);
            toTest.RoutedTo.ShouldBe(routedTo);
            toTest.RoutedFrom.ShouldBe(routedFrom);
        }
        [Fact]
        public void Given_Invalid_TransactionDate_Throws_SubmissionTransactionArgumentException()
        {
            // Arrange
            DateTime testDate = new DateTime(2023, 1, 1);
            DateTime testCurrentDate = new DateTime(2022, 1, 1);
            string routedTo = "test@mail.net";
            string routedFrom = "test2@mail.net";
            string comments = "These are test comments.";

            // Act/Assert
            Assert.Throws<SubmissionTransactionArgumentException>(() => new SubmissionTransaction(testCurrentDate, testDate, routedTo, routedFrom, comments));
        }
        [Theory]
        [InlineData("")]
        [InlineData("11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111")]
        public void Given_Invalid_RoutedTo_Throws_SubmissionTransactionArgumentException(string value)
        {
            // Arrange
            DateTime testDate = new DateTime(2023, 1, 1);
            string routedTo = value;
            string routedFrom = "test2@mail.net";
            string comments = "These are test comments.";

            // Act/Assert
            Assert.Throws<SubmissionTransactionArgumentException>(() => new SubmissionTransaction(testDate, testDate, routedTo, routedFrom, comments));
        }
        [Theory]
        [InlineData("")]
        [InlineData("11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111")]
        public void Given_Invalid_RoutedFrom_Throws_SubmissionTransactionArgumentException(string value)
        {
            // Arrange
            DateTime testDate = new DateTime(2023, 1, 1);
            string routedTo = "test@mail.net";
            string routedFrom = value;
            string comments = "These are test comments.";

            // Act/Assert
            Assert.Throws<SubmissionTransactionArgumentException>(() => new SubmissionTransaction(testDate, testDate, routedTo, routedFrom, comments));
        }
        [Fact]
        public void Given_Invalid_Comments_Throws_SubmissionTransactionArgumentException()
        {
            // Arrange
            DateTime testDate = new DateTime(2023, 1, 1);
            string routedTo = "test@mail.net";
            string routedFrom = "test2@mail.net";
            string comments = new string('a', 5001);

            // Act/Assert
            Assert.Throws<SubmissionTransactionArgumentException>(() => new SubmissionTransaction(testDate, testDate, routedTo, routedFrom, comments));
        }
    }
}
