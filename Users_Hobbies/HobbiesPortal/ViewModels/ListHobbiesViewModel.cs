using Models;
using System.Collections.Generic;

namespace HobbiesPortal.ViewModels
{
    public class ListHobbiesViewModel
    {
		public List<HobbyType> ListHobbiesTypes { get; set; }
		public List<HobbyName> SubListHobbyNames { get; set; }

		public List<Hobby>	ListCreatedHobbies { get; set; }
	}
}
