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

            var fakeTransaction1 = new SubmissionTransaction(new DateTime(2023, 1, 1), new DateTime(2023, 1, 1), "test@test.mail", "test2@test.mail", "Test comments for Submission #1, Transaction #1");
            var fakeSubmission1 = new Submission("Test Submission #1", "Test Description #1.", "TestDriveId #1", "TestListId #1", fakeTransaction1);
            var fakeItem1 = new SubmissionItem("Test Item #1", "TestItemUri", "TestItemId", "TestDriveId");
            var fakeItem2 = new SubmissionItem("Test Item #2", "TestItemUri", "TestItemId", "TestDriveId");
            
            var fakeTransaction2 = new SubmissionTransaction(new DateTime(2023, 1, 2), new DateTime(2023, 1, 2), "test@test.mail", "test2@test.mail", "Test comments for Submission #1, Transaction #2");
            fakeSubmission1.AddItem(fakeItem1);
            fakeSubmission1.AddItem(fakeItem2);
            fakeSubmission1.AddTransaction(fakeTransaction2);

            var fakeTransaction3 = new SubmissionTransaction(new DateTime(2023, 1, 1), new DateTime(2023, 1, 1), "test@test.mail", "test2@test.mail", "Test comments for Submission #2, Transaction #1");
            var fakeSubmission2 = new Submission("Test Submission #2", "Test Description.", "TestDriveId #2", "TestListId #2", fakeTransaction3);
            var fakeItem3 = new SubmissionItem("Test Item #3", "TestItemUri", "TestItemId", "TestDriveId");
            var fakeItem4 = new SubmissionItem("Test Item #4", "TestItemUri", "TestItemId", "TestDriveId");
            
            var fakeTransaction4 = new SubmissionTransaction(new DateTime(2023, 1, 2), new DateTime(2023, 1, 2), "test@test.mail", "test2@test.mail", "Test comments for Submission #2, Transaction #2");
            fakeSubmission2.AddItem(fakeItem3);
            fakeSubmission2.AddItem(fakeItem4);
            fakeSubmission2.AddTransaction(fakeTransaction4);

            var fakeTransaction5 = new SubmissionTransaction(new DateTime(2023, 1, 2), new DateTime(2023, 1, 2), "test@test.mail", "test2@test.mail", "Test comments for Submission #3, Transaction #1");
            var fakeSubmission3 = new Submission("Test Submission #3", "Test Description #3.", "TestDriveId #3", "TestListId #3", fakeTransaction5);
            var fakeItem5 = new SubmissionItem("Test Item #5", "TestItemUri", "TestItemId", "TestDriveId");
            var fakeItem6 = new SubmissionItem("Test Item #6", "TestItemUri", "TestItemId", "TestDriveId");
            
            var fakeTransaction6 = new SubmissionTransaction(new DateTime(2023, 1, 2), new DateTime(2023, 1, 2), "test@test.mail", "test2@test.mail", "Test comments for Submission #3, Transaction #2");
            fakeSubmission3.AddItem(fakeItem5);
            fakeSubmission3.AddItem(fakeItem6);
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
