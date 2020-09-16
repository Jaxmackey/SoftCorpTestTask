using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private ICityRepository _cityRepository;
        private IGeoMetricRepository _geoMetricRepository;
        private IRunRepository _runRepository;
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

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }

        public IQueryable<GeoMetric> GetGeoMetricsByCityName(string name)
        {
            var pageObject = (from g in _repoContext.GeoMetric
                              join r in _repoContext.Run on g.FkRunId equals r.Id
                              join c in _repoContext.City on r.FkCity equals c.Id
                              where c.CityName == name
                              orderby r.StartedAt descending
                              select new GeoMetric { MaxTempC = g.MaxTempC,
                              MaxTempF = g.MaxTempF,
                              MinTempC = g.MinTempC,
                              MinTempF = g.MinTempF,
                              DayName = g.DayName,
                              DayNumber = g.DayNumber,
                              KmH = g.KmH,
                              MiH = g.MiH,
                              Prec = g.Prec,
                              WindMs = g.WindMs}).Take(10);
            return pageObject.OrderBy(x => x.DayNumber); 
        }
    }
}
