using Ninject.Modules;
using BookingHotels.Domain.Interfaces;
using BookingHotels.DAL.Repositories;
 
namespace NLayerApp.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {

        // Специальный модуль Ninject для организации сопоставления зависимостей.
        private string connectionString;
        // Через конструктор передается название подключения
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        // Устанавливает использование EFUnitOfWork в качестве объекта IUnitOfWork.
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}