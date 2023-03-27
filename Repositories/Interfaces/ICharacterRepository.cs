using PracticeFullstackApp.Entities;
using PracticeFullstackApp.Models;

namespace PracticeFullstackApp.Repositories.Interfaces
{
    public interface ICharacterRepository
    {
        IEnumerable<Characters> GetAll();

        Characters GetByID(int id);

        void Insert(Characters character);

        bool Delete(int id);

        bool Update(Characters character);
    }
}
