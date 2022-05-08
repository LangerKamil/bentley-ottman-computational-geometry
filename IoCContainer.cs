using GeometriaObliczeniowa.Views;
using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using GeometriaObliczeniowa.Engines;
using GeometriaObliczeniowa.Engines.Interface;
using Prism.Events;

namespace GeometriaObliczeniowa
{
    public class IoCContainer : PrismBootstrapper
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterScoped(typeof(IIntersectionEngine), typeof(IntersectionEngine));
            containerRegistry.RegisterSingleton(typeof(IEventAggregator), typeof(EventAggregator));
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell(DependencyObject shell)
        {
            Application.Current.MainWindow.Show();
        }
    }
}
