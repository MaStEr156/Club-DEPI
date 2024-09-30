using MVC_1_Depi.Models;

namespace MVC_1_Depi.Interfaces
{
    public interface IClubRepo
    {
        //Crud
        Task<IEnumerable<Club>> GetAllAsync();
        Task<Club?> GetByIdAsync(int id);
        Task<IEnumerable<Club?>> GetClubByCityAsync(string city);
        Task<Club?> GetByIdAsyncNoTracking(int id);


        Task<bool> Add(Club club);
        bool Update(Club club);
        bool Delete(Club club);

        bool Save();
    }
}
