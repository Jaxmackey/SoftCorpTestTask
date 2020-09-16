using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class RunRepository : RepositoryBase<Run>, IRunRepository
    {
        public RunRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
