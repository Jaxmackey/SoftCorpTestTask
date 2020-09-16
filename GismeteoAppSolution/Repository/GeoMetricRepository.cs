using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class GeoMetricRepository: RepositoryBase<GeoMetric>, IGeoMetricRepository
    {
        public GeoMetricRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
