using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace UIApp.Views
{
    public interface IMainView: IView
    {
        event Action InitComboboxies;
        event Action SelectedItemInComboBox;
        string[] SetCitiesCombobox { set; }
        string SelectedCity { get; }
        ComboBox ComboBoxCities { get; }
        DataGridView DataGridView { get; }
        void GridInvoke(Delegate method);
        new void Show();
    }
}
