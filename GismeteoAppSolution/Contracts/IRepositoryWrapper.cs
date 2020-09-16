using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        ICityRepository City { get; }
        IGeoMetricRepository GeoMetric { get; }

        IRunRepository Run { get; }
        IQueryable<GeoMetric> GetGeoMetricsByCityName(string name);
        void Save();
    }
}
