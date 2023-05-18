using DocRouter.Application.Common.Models;
using DocRouter.Application.Submissions.Commands.CreateSubmission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace DocRouter.WebUI.Models
{
    public class SubmissionViewModel
    {
        public List<SelectListItem> Users { get; set; }
        /// <summary>
        /// The Email address of the person who will receive the submission.
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// The Name to assign to the submission.
        /// </summary>
        public string SubmissonName { get; set; }
        /// <summary>
        /// A string containing comments about the submission.
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// A list of files.
        /// </summary>
        public List<IFormFile> Files { get; set; }

        public CreateSubmissionCommand CreateSubmissionCommand()
        {
         return new CreateSubmissionCommand
            {
                Recipient = Recipient,
                SubmissonName = SubmissonName,
                Comments = Comments,
                Files = Files.Select(f =>
                    new FileSubmissionDto
                    {
                        Content = f.OpenReadStream(),
                        FileName = f.FileName,
                        ContentType = f.ContentType
                    }).ToList()
            };
        }
    }
}
