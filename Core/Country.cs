using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Country
    {
        [Key]
        public Int64 CountryID {  get; set; }
        public string CountryName { get; set; }
        public virtual List<State> States { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
