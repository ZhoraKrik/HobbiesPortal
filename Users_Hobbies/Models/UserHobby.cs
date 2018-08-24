
using System.Collections.Generic;

namespace Models
{
    public class UserHobby
    {
        public int Id      { get; set; } 
        public int UserId  { get; set; }
        public int HobbyId { get; set; }

        public Hobby  CurrHobby { get; set; }
    }
}
