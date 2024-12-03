using System;
using System.Linq;
using BadNews.Models.Comments;
using BadNews.Repositories.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BadNews.Controllers
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsRepository commentsRepository;

        public CommentsController(ICommentsRepository commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        // GET
        [HttpGet("api/news/{id}/comments")]
        public ActionResult<CommentsDto> GetCommentsForNews(Guid newsId)
        {
            var comms = commentsRepository.GetComments(newsId);
            var dtos = comms.Select(com => new СommentDto { User = com.User, Value = com.Value }).ToArray();
            return new CommentsDto { 
                NewsId = newsId,
                Comments = dtos
            };
        }
    }
}