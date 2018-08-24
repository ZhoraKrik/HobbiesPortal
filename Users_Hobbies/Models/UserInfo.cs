using System.Collections.Generic;

namespace Models
{
    public class UserInfo
    {
        public int  Id { get; set; }

        public User  CurrUser   { get; set; }

        public string HobbyType  { get; set; }
        public string HobbyName  { get; set; }
    }
}
