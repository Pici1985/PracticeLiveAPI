using PracticeFullstackApp.Contexts;
using PracticeFullstackApp.Entities;
using PracticeFullstackApp.Models;
using PracticeFullstackApp.Repositories.Interfaces;

namespace PracticeFullstackApp.Repositories.Implementations
{
    public class CharacterRepository : ICharacterRepository
    {

        internal readonly PracticeDbContext _context;

        public CharacterRepository(PracticeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Characters> GetAll()
        {
            var allCharacters = (from characters in _context.Characters
                                 select characters).ToList();

            return allCharacters;    
        }
        
        public Characters GetByID(int id)
        {
            var character = (from anyad in _context.Characters
                             where anyad.Id == id
                             select anyad).FirstOrDefault();

            return character;
        }

        public void Insert(Characters character)
        {
            var characterToSave = new Characters()
            { 
                Name = character.Name,  
                Image = character.Image,
                Class = character.Class,
                Level = character.Level,
                KE = character.KE,
                VE = character.VE,  
                TE = character.TE,
                FP = character.FP,
                EP = character.EP,
                SFE = character.SFE,
                SPJ = character.SPJ,    
                SPB = character.SPB,
            };

            _context.Characters.Add(characterToSave);
            _context.SaveChanges();
        }

        public bool Delete(int id) 
        {
            var characterToRemove = (from character in _context.Characters
                                     where character.Id == id
                                     select character).FirstOrDefault();

            if (characterToRemove != null) 
            { 
                _context.Characters.Remove(characterToRemove);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public bool Update(Characters character)
        {
            var characterToUpdate = (from saved in _context.Characters
                                     where character.Id == saved.Id
                                     select saved).FirstOrDefault();

            if (characterToUpdate != null)
            { 
                characterToUpdate.Name = character.Name;
                characterToUpdate.Image = character.Image;
                characterToUpdate.Class = character.Class;  
                characterToUpdate.Level = character.Level;  
                characterToUpdate.KE = character.KE; 
                characterToUpdate.TE = character.TE;
                characterToUpdate.VE = character.VE;
                characterToUpdate.FP = character.FP;
                characterToUpdate.EP = character.EP;
                characterToUpdate.SFE = character.SFE;
                characterToUpdate.SPJ = character.SPJ;
                characterToUpdate.SPB = character.SPB;

                _context.Characters.Update(characterToUpdate);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
