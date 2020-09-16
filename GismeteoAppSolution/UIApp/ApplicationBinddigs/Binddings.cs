using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UIApp.Presenters;
using UIApp.Views;

namespace UIApp.ApplicationBinddigs
{
    public class Binddings : NinjectModule
    {
        public override void Load()
        {
            Bind<IMainView>().To<MainView>();
            Bind<MainPresenter>().ToSelf();
        }
    }
}
