
using Models;
using System.Collections.Generic;

namespace HobbiesPortal.ViewModels
{
    public class EditViewModel
    {
        public User CurrUser { get; set; }
        public List <Hobby> ListHobies { get; set; }
    }
}
