using System.Collections.Generic;

namespace Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string BirthDay { get; set; }
        public int Age { get; set; }

        public List <UserHobby> UserHobbies { get; set; }
 
        public User()
        {
            UserHobbies = new List<UserHobby>();
        }
    }
}
