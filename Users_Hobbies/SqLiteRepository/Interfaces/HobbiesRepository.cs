using Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SqLiteRepository.Interfaces
{  
    public interface IHobbiesRepository
    {
        Task<List<UserInfo>>  GetListUsers();
        Task<User> GetCurrentUser(int nUserId);

        Task<int> AddNewUser(User UserInfo);
        Task<int> EditUser(User User);
        Task<int> DeleteUser(int UserId);
        Task<int> AddUserHobby(UserHobby addHobby);
        Task<int> DeleteUserHobby(UserHobby removeItem);
        Task<int> AddHobby(Hobby addHobby);
        Task<int> DeleteHobby(int  nHobbyId);
        Task<Hobby> GetCurrentHobby(int nHobbyId);
        Task<List<HobbyType>> AddHobbyType(string strAddTypeName);
        Task<List<HobbyType>> DeleteHobbyType(int nTypeId);
        Task<List<HobbyName>> AddHobbyName(string strAddNameName);
        Task<List<HobbyName>> DeleteHobbyName(int nNameId);

        Task<List<HobbyType>> GetHobbyTypes();
        Task<List<HobbyName>> GetHobbyNames();
        Task<List<HobbyName>> GetHobbyNames(int nHobbyType);
		Task<List<HobbyName>> GetNamesNotLinkHobby();

		Task<List<Hobby>> GetUserHobbies(int nUserId);
		Task <List<Hobby>> GetListHobbies();
		Task <List<Hobby>> GetListHobbiesOnTypeId(int nTypeId);
	}
}
  