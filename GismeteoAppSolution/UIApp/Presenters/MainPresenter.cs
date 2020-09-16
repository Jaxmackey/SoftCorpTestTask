
using Entities.Models;
using Newtonsoft.Json;
using ParserGismeteoService.Driver.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;
using UIApp.Views;

namespace UIApp.Presenters
{
    public class MainPresenter : IPresenter
    {
        private readonly IMainView _mainView;
        private delegate void EnableComboboxDelegate();
        private delegate void ChangeDataGridDelegate();
        public MainPresenter(IMainView mainView)
        {
            _mainView = mainView;
            this._mainView.InitComboboxies += () => Load();
            this._mainView.SelectedItemInComboBox += () => GetDataByCity();
        }

        public async void GetDataByCity()
        {
            List<GeoMetric> geoList = new List<GeoMetric>();
            Cursor.Current = Cursors.WaitCursor;
            var request = new HttpRequestMessage(HttpMethod.Get,
            ConfigurationManager.AppSettings["Host"] + "city/" + this._mainView.SelectedCity);
            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                var responseBody = response.Content.ReadAsStringAsync().Result;
                geoList = JsonConvert.DeserializeObject<List<GeoMetric>>(responseBody);
            }
            this._mainView.GridInvoke(new ChangeDataGridDelegate(() => SetDataToGrid(geoList)));
            Cursor.Current = Cursors.Default;
            this._mainView.ComboBoxCities.Invoke(new EnableComboboxDelegate(EnableComboBox));
        }

        public async void Load()
        {
            List<City> citiesList = new List<City>();
            Cursor.Current = Cursors.WaitCursor;
            this._mainView.ComboBoxCities.Invoke(new EnableComboboxDelegate(DisableComboBox));
            var request = new HttpRequestMessage(HttpMethod.Get,
            ConfigurationManager.AppSettings["Host"] + "city");
            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                var responseBody = response.Content.ReadAsStringAsync().Result;
                citiesList = JsonConvert.DeserializeObject<List<City>>(responseBody);
            }
            this._mainView.SetCitiesCombobox = citiesList.Select(x => x.CityName).ToArray();
            Cursor.Current = Cursors.Default;
            this._mainView.ComboBoxCities.Invoke(new EnableComboboxDelegate(EnableComboBox));
        }

        public void SetDataToGrid(List<GeoMetric> list)
        {
            this._mainView.ComboBoxCities.Invoke(new EnableComboboxDelegate(ClearGrid));
            foreach (var item in list)
            {
                int rowIndex = this._mainView.DataGridView.Rows.Add();
                var row = this._mainView.DataGridView.Rows[rowIndex];
                row.Cells["DayName"].Value = item.DayName;
                row.Cells["DayNumber"].Value = item.DayNumber;
                row.Cells["MaxTempC"].Value = item.MaxTempC;
                row.Cells["MaxTempF"].Value = item.MaxTempF;

                row.Cells["MinTempC"].Value = item.MinTempC;
                row.Cells["MinTempF"].Value = item.MinTempF;
                row.Cells["WindMs"].Value = item.WindMs;
                row.Cells["MiH"].Value = item.MiH;

                row.Cells["KmH"].Value = item.KmH;
                row.Cells["Prec"].Value = item.Prec;
            }
        }

        public void ClearGrid()
        {
            this._mainView.DataGridView.Rows.Clear();
        }

        public void DisableComboBox()
        {
            this._mainView.ComboBoxCities.Enabled = false;
        }

        public void EnableComboBox()
        {
            this._mainView.ComboBoxCities.Enabled = true;
        }

        public void Run()
        {
            this._mainView.Show();
        }
    }
}
