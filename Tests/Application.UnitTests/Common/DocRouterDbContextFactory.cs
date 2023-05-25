using DocRouter.Common.Enums;
using DocRouter.Domain.Entities;
using DocRouter.Persistence;
using Microsoft.EntityFrameworkCore;
using System;


namespace DocRouter.Application.UnitTests.Common
{
    public class DocRouterDbContextFactory
    {
        private const string ConnectionString = @"Data Source=PGPDDATA02.co.pg.md.us;Initial Catalog=DocRouter_Testing;Trusted_Connection=True";

        public static DocRouterContext Create()
        {
            var options = new DbContextOptionsBuilder<DocRouterContext>()
                .UseSqlServer(ConnectionString)
                .Options;
            var context = new DocRouterContext(options, new CurrentUserServiceTesting(), new DateTimeTestProvider());
            context.Database.EnsureCreated();

            var fakeSubmission1 = new Submission("Test Submission #1", @"C:\DocRouterTest\Test Submission 1", "Test Description.");
            var fakeItem1 = new SubmissionItem("Test Item #1", @"C:\DocRouterTest\Test Submission 1\Test Item #1");
            var fakeItem2 = new SubmissionItem("Test Item #2", @"C:\DocRouterTest\Test Submission 1\Test Item #2");
            var fakeTransaction1 = new SubmissionTransaction(new DateTime(2023, 1, 1), new DateTime(2023, 1, 1), TransactionStatus.Approved, "test@test.mail", "Test comments for #1");
            var fakeTransaction2 = new SubmissionTransaction(new DateTime(2023, 1, 2), new DateTime(2023, 1, 2), TransactionStatus.Rejected, "test@test.mail", "Test comments for #1");
            fakeSubmission1.AddItem(fakeItem1);
            fakeSubmission1.AddItem(fakeItem2);
            fakeSubmission1.AddTransaction(fakeTransaction1);
            fakeSubmission1.AddTransaction(fakeTransaction2);

            var fakeSubmission2 = new Submission("Test Submission #2", @"C:\DocRouterTest\Test Submission 2", "Test Description.");
            var fakeItem3 = new SubmissionItem("Test Item #3", @"C:\DocRouterTest\Test Submission 1\Test Item #3");
            var fakeItem4 = new SubmissionItem("Test Item #4", @"C:\DocRouterTest\Test Submission 1\Test Item #4");
            var fakeTransaction3 = new SubmissionTransaction(new DateTime(2023, 1, 1), new DateTime(2023, 1, 1), TransactionStatus.Approved, "test@test.mail", "Test comments for #2");
            var fakeTransaction4 = new SubmissionTransaction(new DateTime(2023, 1, 2), new DateTime(2023, 1, 2), TransactionStatus.Rejected, "test@test.mail", "Test comments for #2");
            fakeSubmission2.AddItem(fakeItem3);
            fakeSubmission2.AddItem(fakeItem4);
            fakeSubmission2.AddTransaction(fakeTransaction3);
            fakeSubmission2.AddTransaction(fakeTransaction4);

            var fakeSubmission3 = new Submission("Test Submission #3", @"C:\DocRouterTest\Test Submission 3", "Test Description.");
            var fakeItem5 = new SubmissionItem("Test Item #5", @"C:\DocRouterTest\Test Submission 1\Test Item #5");
            var fakeItem6 = new SubmissionItem("Test Item #6", @"C:\DocRouterTest\Test Submission 1\Test Item #6");
            var fakeTransaction5 = new SubmissionTransaction(new DateTime(2023, 1, 1), new DateTime(2023, 1, 1), TransactionStatus.Approved, "test@test.mail", "Test comments for #3");
            var fakeTransaction6 = new SubmissionTransaction(new DateTime(2023, 1, 2), new DateTime(2023, 1, 2), TransactionStatus.Rejected, "test@test.mail", "Test comments for #3");
            fakeSubmission3.AddItem(fakeItem5);
            fakeSubmission3.AddItem(fakeItem6);
            fakeSubmission3.AddTransaction(fakeTransaction5);
            fakeSubmission3.AddTransaction(fakeTransaction6);
            context.Submissions.Add(fakeSubmission1);
            context.Submissions.Add(fakeSubmission2);
            context.Submissions.Add(fakeSubmission3);
            context.SaveChanges();
            return context;
        }
        public static void Destroy(DocRouterContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
