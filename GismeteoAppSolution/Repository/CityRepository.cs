using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class CityRepository: RepositoryBase<City>, ICityRepository
    {
        public CityRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
