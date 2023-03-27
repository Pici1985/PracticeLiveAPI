using System.ComponentModel.DataAnnotations;

namespace PracticeFullstackApp.Entities
{
    public class Characters
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Class { get; set; }
        public int Level { get; set; }
        public int KE { get; set; }
        public int TE { get; set; }
        public int VE { get; set; }
        public int FP { get; set; }
        public int EP { get; set; }
        public int SFE { get; set; }
        public int SPJ { get; set; }
        public int SPB { get; set; }

    }
}
