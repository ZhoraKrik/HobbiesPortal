using System.Collections.Generic;

namespace Models
{
    public class Hobby
    {
        public int Id { get; set; }

        public int HobbyTypeId { get; set; }
        public HobbyType HobbyTypes { get; set; }

        public int HobbyNameId { get; set; }
        public HobbyName HobbyNames { get; set; }
    }
} 
