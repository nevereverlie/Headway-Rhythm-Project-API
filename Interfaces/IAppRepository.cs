using System.Threading.Tasks;

namespace Headway_Rhythm_Project_API.Interfaces
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        void Update<T>(T entity) where T: class;
        Task<bool> SaveAll(); 

    }
}