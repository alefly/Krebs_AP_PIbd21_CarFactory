using CarFactoryService.WorkerList;
using CarFactoryService.Interfaces;
using System;
using CarFactoryService;
using CarFactoryService.WorkDB;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using System.Data.Entity;
using CarServiceService.WorkDB;

namespace AbstractShopView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
			currentContainer.RegisterType<DbContext, DbContext>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IConsumer, ConsumerServiceDB>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IIngridient, IngridientServiceDB>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IWorker, WorkerServiceDB>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ICommodity, CommodityServiceDB>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IStorage, StorageServiceDB>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IMain, MainServiceDB>(new HierarchicalLifetimeManager());

			return currentContainer;
        }
    }
}
