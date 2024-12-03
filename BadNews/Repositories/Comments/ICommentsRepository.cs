using System.Collections.Generic;
using System;

namespace BadNews.Repositories.Comments
{
    public interface ICommentsRepository
    {
        public IReadOnlyCollection<Comment> GetComments(Guid newsId);
    }
}
