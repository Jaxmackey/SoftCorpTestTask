using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace UIApp.Presenters
{
    public interface IPresenter
    {
        void Run();
        void Load();
        void GetDataByCity();
    }
}
