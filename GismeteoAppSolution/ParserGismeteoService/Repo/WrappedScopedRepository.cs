using Contracts;
using Entities;
using Entities.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ParserGismeteoService.Repo
{
    public class WrappedScopedRepository : IWrappedScopedRepository
    {
        private RepositoryContext _repoContext;
        private ICityRepository _cityRepository;
        private IGeoMetricRepository _geoMetricRepository;
        private IRunRepository _runRepository;
        public WrappedScopedRepository(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public ICityRepository City
        {
            get
            {
                if (_cityRepository == null)
                {
                    _cityRepository = new CityRepository(_repoContext);
                }
                return _cityRepository;
            }
        }
        public IGeoMetricRepository GeoMetric
        {
            get
            {
                if (_geoMetricRepository == null)
                {
                    _geoMetricRepository = new GeoMetricRepository(_repoContext);
                }
                return _geoMetricRepository;
            }
        }

        public IRunRepository Run
        {
            get
            {
                if (_runRepository == null)
                {
                    _runRepository = new RunRepository(_repoContext);
                }
                return _runRepository;
            }
        }

        public IQueryable<GeoMetric> GetGeoMetricsByCityName(string cityName)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
