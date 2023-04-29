using PracticeFullstackApp.Entities;
using PracticeFullstackApp.Models;

namespace PracticeFullstackApp.Repositories.Interfaces
{
    public interface ICharacterRepository
    {
        IEnumerable<Characters> GetAll();

        Characters GetByID(int id);

        void Insert(CharactersALT character);

        bool Delete(int id);

        bool Update(CharactersALT character);
    }
}
