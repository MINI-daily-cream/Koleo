//using API.Services.Interfaces;
//using Koleo.Services;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace UnitTestsProject
//{
//    public class ComplaintServiceTests
//    {
//        [Fact]
//        public async Task MakeComplaint_Returns_True_When_Complaint_Made_Successfully()
//        {
//            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
//            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
//                               .ReturnsAsync((null, true));

//            var complaintService = new ComplaintService(mockDatabaseService.Object);
//            var result = await complaintService.MakeComplaint("userId", "ticketId", "content");
            
//            Assert.True(result);
//        }

//        [Fact]
//        public async Task EditComplaint_Returns_True_When_Complaint_Edited_Successfully()
//        {
//            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
//            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
//                               .ReturnsAsync((null, true));

//            var complaintService = new ComplaintService(mockDatabaseService.Object);
//            var result = await complaintService.EditComplaint("complaintId", "ticketId", "content");
//            Assert.True(result);
//        }

//        [Fact]
//        public async Task RemoveComplaint_Returns_True_When_Complaint_Removed_Successfully()
//        {
//            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
//            mockDatabaseService.SetupSequence(x => x.ExecuteSQL(It.IsAny<string>()))
//                               .ReturnsAsync((null, true))
//                               .ReturnsAsync((null, true));

//            var complaintService = new ComplaintService(mockDatabaseService.Object);
//            var result = await complaintService.RemoveComplaint("complaintId");
//            Assert.True(result);
//        }

//        [Fact]
//        public async Task ListComplaints_Returns_Complaints_When_Exist()
//        {
//            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
//            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
//                               .ReturnsAsync((new List<string[]> { new string[] { "ticketId", "content", "response" } }, true));

//            var complaintService = new ComplaintService(mockDatabaseService.Object);
//            var result = await complaintService.ListComplaints("userId");
//            Assert.True(result.Item2);
//            Assert.NotEmpty(result.Item1);
//        }

//        [Fact]
//        public async Task AnswerComplaint_Returns_True_When_Complaint_Answered_Successfully()
//        {
//            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
//            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
//                               .ReturnsAsync((null, true));

//            var complaintService = new ComplaintService(mockDatabaseService.Object);
//            var result = await complaintService.AnswerComplaint("adminId", "complaintId", "response");
//            Assert.True(result);
//        }

//        [Fact]
//        public async Task ListUnansweredComplaints_Returns_Unanswered_Complaints_When_Exist()
//        {
//            var mockDatabaseService = new Mock<IDatabaseServiceAPI>();
//            mockDatabaseService.Setup(x => x.ExecuteSQL(It.IsAny<string>()))
//                               .ReturnsAsync((new List<string[]> { new string[] { "complaintId", "userId", "ticketId", "content" } }, true));

//            var complaintService = new ComplaintService(mockDatabaseService.Object);
//            var result = await complaintService.ListUnansweredComplaints();
//            Assert.True(result.Item2);
//            Assert.NotEmpty(result.Item1);
//        }
//    }
//}
