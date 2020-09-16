using Entities.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ParserGismeteoService.Driver.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParserGismeteoService.Driver
{
    public interface IDriverService
    {
        Task GetCityGeometrics();
        Task<List<IWebElement>> GetElementsCollection(IWebDriver driver, string xpath);
        Task<List<GeometricDto>> ParseData(IWebDriver driver);
        void Exit(IWebDriver driver, ChromeDriverService chromeDriverService);
        void Create(GeoMetric geoMetric);
        void Create(Run run);
        List<City> GetAllCities();
        void Save();
    }
}
