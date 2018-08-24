using Models;
using System.Collections.Generic;

namespace HobbiesPortal.ViewModels
{
    public class AddUserHobbyViewModel
    {
        public int      UserId { get; set; }
        public Hobby    AddedHobby { get; set; }
        public List<HobbyType> ListHobbiesTypes { get; set; }
        public List<HobbyName> SubListHobbyNames { get; set; }
    }
}
