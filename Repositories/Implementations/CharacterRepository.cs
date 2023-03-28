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
            //var allCharacters = (from characters in _context.Characters
            //                     select characters).ToList();
            var allCharacters = (from character in _context.CharactersALT
                                    join char_av in _context.Character_Avatar on character.Id equals char_av.Character_ID
                                    join avatar in _context.Avatars on char_av.Avatar_ID equals avatar.Id
                                    select new Characters()
                                    {
                                        Id = character.Id,
                                        Name = character.Name,
                                        Image = avatar.Image,
                                        Class = character.Class,
                                        Level = character.Level,
                                        KE = character.KE,
                                        TE = character.TE,
                                        VE = character.VE,
                                        FP = character.PF,
                                        EP = character.EP,
                                        SFE = character.SFE,
                                        SPJ = character.SPJ,
                                        SPB = character.SPB,
                                    }).ToList();


            return allCharacters;    
        }
        
        public Characters GetByID(int id)
        {
            var character = (from anyad in _context.Characters
                             where anyad.Id == id
                             select anyad).FirstOrDefault();

            return character;
        }

        public void Insert(CharactersALT character)
        {
            var characterToSave = new CharactersALT()
            { 
                Name = character.Name,  
                Image = character.Image,
                Class = character.Class,
                Level = character.Level,
                KE = character.KE,
                VE = character.VE,  
                TE = character.TE,
                PF = character.PF,
                EP = character.EP,
                SFE = character.SFE,
                SPJ = character.SPJ,    
                SPB = character.SPB,
            };

            _context.CharactersALT.Add(characterToSave);

            _context.SaveChanges();

            var Char_Avatar = new Character_Avatar()
            {
                Avatar_ID = characterToSave.Image,
                Character_ID = characterToSave.Id
            };

            _context.Character_Avatar.Add(Char_Avatar);

            _context.SaveChanges();
        }

        public bool Delete(int id) 
        {
            var Char_AvatarToRemove = (from char_av in _context.Character_Avatar
                                       where char_av.Character_ID == id
                                       select char_av).FirstOrDefault();

            if (Char_AvatarToRemove == null)
            {
                return false;
            }

            _context.Character_Avatar.Remove(Char_AvatarToRemove);
            _context.SaveChanges();

            var characterToRemove = (from character in _context.CharactersALT
                                     where character.Id == id
                                     select character).FirstOrDefault();

            if (characterToRemove == null) 
            { 
                return false;
            }

            _context.CharactersALT.Remove(characterToRemove);
            _context.SaveChanges();



            return true;

        }

        public bool Update(CharactersALT character)
        {
            var characterToUpdate = (from saved in _context.CharactersALT
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
                characterToUpdate.PF = character.PF;
                characterToUpdate.EP = character.EP;
                characterToUpdate.SFE = character.SFE;
                characterToUpdate.SPJ = character.SPJ;
                characterToUpdate.SPB = character.SPB;

                _context.CharactersALT.Update(characterToUpdate);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
