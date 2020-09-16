using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIApp.Views;

namespace UIApp
{
    public partial class MainView : Form, IMainView
    {
        public string[] SetCitiesCombobox
        {
            set { comboBoxCities.Items.AddRange(value); }
        }

        public string SelectedCity
        {
            get { return comboBoxCities.Text; }
        }

        public ComboBox ComboBoxCities
        {
            get { return comboBoxCities; }
        }

        public DataGridView DataGridView
        {
            get { return dataGridViewCitiesData; }
        }

        public MainView()
        {
            InitializeComponent();
            this.Load += (sender, args) => Invoke(InitComboboxies);
        }

        public new void Show()
        {
            Application.Run(this);
        }
        public event Action InitComboboxies;
        public event Action SelectedItemInComboBox;
        private delegate void ChangeStatusDelegate();

        private void comboBoxCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            Invoke(SelectedItemInComboBox);
        }

        public void GridInvoke(Delegate method)
        {
            dataGridViewCitiesData.Invoke(method);
        }
    }
}
