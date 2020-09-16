using Contracts;
using Entities.Models;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ParserGismeteoService.Driver.Models;
using ParserGismeteoService.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserGismeteoService.Driver
{
    public class DriverService : IDriverService
    {
        private string _xpathWidgetDayName = "(//div[@class='widget__container'])[1]/div[@class='widget__row widget__row_date']/div[@class='widget__item']/div[@class='w_date']/a/div[@class='w_date__day']";
        private string _xpathWidgetDate = "(//div[@class='widget__container'])[1]/div[@class='widget__row widget__row_date']/div[@class='widget__item']/div[@class='w_date']/a/span[contains(@class, 'w_date__date')]";
        private string _xpathMaxTempC = "(//div[@class='widget__container'])[1]/div[@class='widget__row widget__row_table widget__row_temperature']/div/div/div/div/div[@class='maxt']/span[@class='unit_temperature_c']";
        private string _xpathMaxTempF = "(//div[@class='widget__container'])[1]/div[@class='widget__row widget__row_table widget__row_temperature']/div/div/div/div/div[@class='maxt']/span[@class='unit_temperature_f']";
        private string _xpathMinTempC = "(//div[@class='widget__container'])[1]/div[@class='widget__row widget__row_table widget__row_temperature']/div/div/div/div/div[@class='mint']/span[@class='unit_temperature_c']";
        private string _xpathMinTempF = "(//div[@class='widget__container'])[1]/div[@class='widget__row widget__row_table widget__row_temperature']/div/div/div/div/div[@class='mint']/span[@class='unit_temperature_f']";
        private string _xpathWindMs = "(//div[@class='widget__container'])[1]/div[@class='widget__row widget__row_table widget__row_wind-or-gust']/div/div/div/span[@class='unit_wind_m_s']";
        private string _xpathWindMiH = "(//div[@class='widget__container'])[1]/div[@class='widget__row widget__row_table widget__row_wind-or-gust']/div/div/div/span[@class='unit_wind_mi_h']";
        private string _xpathWindKmH = "(//div[@class='widget__container'])[1]/div[@class='widget__row widget__row_table widget__row_wind-or-gust']/div/div/div/span[@class='unit_wind_km_h']";
        private string _xpathPrec = "(//div[@class='widget__container'])[1]/div[@class='widget__row widget__row_table widget__row_precipitation']/div/div/div[@class='w_prec__value']|(//div[@class='widget__container'])[1]/div[@class='widget__row widget__row_table widget__row_precipitation']/div";
        private readonly IWrappedScopedRepository _wrappedScopedRepository;
        private readonly ILogger<Worker> _logger;

        public DriverService(IWrappedScopedRepository wrappedScopedRepository, ILogger<Worker> logger)
        {
            _logger = logger;
            _wrappedScopedRepository = wrappedScopedRepository;
        }
        public async Task<List<GeometricDto>> ParseData(IWebDriver driver)
        {
            var result = new List<GeometricDto>();
            await Task.Run(() => {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("for (let item of document.getElementsByTagName('span')) {item.classList.remove('unit');}");
                var dayNameElementCollections = GetElementsCollection(driver, _xpathWidgetDayName).Result;
                var dateElementCollections = GetElementsCollection(driver, _xpathWidgetDate).Result;
                var maxTempCElementCollections = GetElementsCollection(driver, _xpathMaxTempC).Result;
                var maxTempFElementCollections = GetElementsCollection(driver, _xpathMaxTempF).Result;
                var minTempCElementCollections = GetElementsCollection(driver, _xpathMinTempC).Result;
                var minTempFElementCollections = GetElementsCollection(driver, _xpathMinTempF).Result;
                var windMsElementCollections = GetElementsCollection(driver, _xpathWindMs).Result;
                var miHElementCollections = GetElementsCollection(driver, _xpathWindMiH).Result;
                var kMhElementCollections = GetElementsCollection(driver, _xpathWindKmH).Result;
                var precElementCollections = GetElementsCollection(driver, _xpathPrec).Result;
                int index = 0;
                foreach (var item in dayNameElementCollections)
                {
                    result.Add(new GeometricDto
                    {
                        DayName = item.Text.Trim(),
                        DayNumber = dateElementCollections[index].Text.Trim(),
                        MaxTempC = maxTempCElementCollections[index].Text.Trim(),
                        MaxTempF = maxTempFElementCollections[index].Text.Trim(),
                        MinTempC = minTempCElementCollections[index].Text.Trim(),
                        MinTempF = minTempFElementCollections[index].Text.Trim(),
                        WindMs = windMsElementCollections[index].Text.Trim(),
                        MiH = miHElementCollections[index].Text.Trim(),
                        KmH = kMhElementCollections[index].Text.Trim(),
                        Prec = precElementCollections.Count != 1 ? precElementCollections[index].Text.Trim() : precElementCollections[0].Text.Trim(),
                    });
                    index++;
                }
            });
            return result;
        }
        public void Exit(IWebDriver driver, ChromeDriverService chromeDriverService)
        {
            driver.Quit();
            driver.Dispose();
            chromeDriverService.Dispose();
        }
        public async Task<List<IWebElement>> GetElementsCollection(IWebDriver driver, string xpath)
        {
            await Task.Run(() => {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(OpenQA.Selenium.By.XPath(xpath)));
            });
            return new List<IWebElement>(driver.FindElements(By.XPath(xpath)));
        }

        public async Task GetCityGeometrics()
        {
            var cities = GetAllCities();
            var citiesgeoMetricsList = new List<CityGeometricDto>();
            await Task.Run(() => {
                ParallelOptions po = new ParallelOptions();
                po.MaxDegreeOfParallelism = 7;
                Parallel.For(0, cities.Count, po, i => {
                    ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = true;
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("--headless");
                    var driver = new ChromeDriver(chromeDriverService, options, TimeSpan.FromSeconds(120));
                    var currentRun = new RunDto
                    {
                        StartedAt = DateTime.Now
                    };
                    driver.Navigate().GoToUrl("https://www.gismeteo.ru/" + cities[i].Url + "10-days/");
                    _logger.LogInformation("https://www.gismeteo.ru/" + cities[i].Url + "10-days/");
                    citiesgeoMetricsList.Add(new CityGeometricDto
                    {
                        CityId = cities[i].Id,
                        Geometrics = ParseData(driver).Result,
                        Run = currentRun
                    });
                    currentRun.EndedAt = DateTime.Now;
                    Exit(driver, chromeDriverService);
                });
            });
            foreach (var city in citiesgeoMetricsList)
            {
                var newRun = new Run
                {
                    Id = Guid.NewGuid(),
                    StartedAt = city.Run.StartedAt,
                    EndedAt = city.Run.EndedAt,
                    FkCity = city.CityId
                };
                Create(newRun);
                foreach (var gm in city.Geometrics)
                {
                    Create(new GeoMetric
                    {
                        Id = Guid.NewGuid(),
                        DayName = gm.DayName,
                        DayNumber = gm.DayNumber,
                        MaxTempC = gm.MaxTempC,
                        MaxTempF = gm.MaxTempF,
                        MinTempC = gm.MinTempC,
                        MinTempF = gm.MinTempF,
                        WindMs = gm.WindMs,
                        MiH = gm.MiH,
                        KmH = gm.KmH,
                        Prec = gm.Prec,
                        FkRunId = newRun.Id
                    });
                }
            }
            Save();
        }

        public void Create(GeoMetric geoMetric)
        {
            _wrappedScopedRepository.GeoMetric.Create(geoMetric);
        }

        public void Create(Run run)
        {
            _wrappedScopedRepository.Run.Create(run);
        }

        public List<City> GetAllCities()
        {
            return _wrappedScopedRepository.City.GetAll().ToList();
        }

        public void Save()
        {
            _wrappedScopedRepository.Save();
        }
    }
}
