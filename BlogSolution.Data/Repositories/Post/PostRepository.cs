using BlogSolution.Data.EF;
using BlogSolution.Data.Entities;
using BlogSolution.Data.Infrastructure;

namespace BlogSolution.Data.Repositories.Attribute
{
    public class PostRepository : CommonRepository<PostEntity>, IPostRepository
    {
        public PostRepository(DataContext context) : base(context){}
    }
}
