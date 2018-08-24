using System.Collections.Generic;
using System.Threading.Tasks;
using HobbiesPortal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Models;
using SqLiteRepository.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HobbiesPortal.Controllers
{
    public class UsersController : Controller
    {
        IHobbiesRepository m_hobbiesRepository;
       
        public UsersController(IHobbiesRepository currRepository)
        {
            m_hobbiesRepository = currRepository;
        }

 //     [Route("ListUsers")]
        [HttpGet]
        public async Task<IActionResult> ListUsers()
        {
            return View(await m_hobbiesRepository.GetListUsers());
        }

        // GET: Users/Create
        public IActionResult AddUser(int TypeId, int NameId, int IsChangeType )
        {          
           return View();
        }

        [HttpPost]
        public IActionResult AddUser(User UserInfo)
        {
            m_hobbiesRepository.AddNewUser(UserInfo);
            return View();
        }

        // GET: Users/Edit
        public async Task<IActionResult> EditUser(int  UserId)
        {
            EditViewModel currEditView = new EditViewModel();
            currEditView.CurrUser= await m_hobbiesRepository.GetCurrentUser(UserId);
            currEditView.ListHobies= await m_hobbiesRepository.GetUserHobbies(UserId);

            return View(currEditView);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(User currUser)
        {
            await m_hobbiesRepository.EditUser(currUser);

            return View("ListUsers", await m_hobbiesRepository.GetListUsers());
        }

        // GET: Users/Delete
        public async Task<IActionResult> DeleteUser(int UserId)
        {
            ViewBag.CurrUser = await m_hobbiesRepository.GetCurrentUser(UserId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(User  currUser)
        {
            await m_hobbiesRepository.DeleteUser(currUser.Id);
            return View("ListUsers", await m_hobbiesRepository.GetListUsers());
        }

        // GET: Users/AddUserHooby
        public async Task<IActionResult> AddUserHobby(int UserId, int TypeId, int NameId)
        {
            ViewBag.TypeId = TypeId;
            ViewBag.NameId = NameId;

            Hobby currHobby = null;
            List<Hobby>  currHobbies = await m_hobbiesRepository.GetListHobbiesOnTypeId(TypeId);
            if (currHobbies.Count == 0)
            {
                currHobbies = await m_hobbiesRepository.GetListHobbies();
                TypeId = currHobbies[0].HobbyTypeId;
                currHobbies = await m_hobbiesRepository.GetListHobbiesOnTypeId(TypeId);

                ViewBag.NameId = currHobbies[0].HobbyNameId;
                ViewBag.TypeId = TypeId = currHobbies[0].HobbyTypeId;
            }

            if (NameId == 0)
                currHobby = currHobbies[0];
            else
                foreach (Hobby item in currHobbies)
                {
                    if (item.HobbyNameId == NameId)
                    {
                        currHobby = item;
                        break;
                    }
                }

            List<HobbyType> allHobbiesTypes = await m_hobbiesRepository.GetHobbyTypes();
            List<HobbyName> subListHobbyNames = await m_hobbiesRepository.GetHobbyNames(TypeId);

            AddUserHobbyViewModel AddHobbyViewModel = new AddUserHobbyViewModel();

            AddHobbyViewModel.UserId = UserId;
            AddHobbyViewModel.AddedHobby = currHobby;
            AddHobbyViewModel.ListHobbiesTypes = allHobbiesTypes;
            AddHobbyViewModel.SubListHobbyNames = subListHobbyNames;
 
            return View(AddHobbyViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserHobby(UserHobby UserHobbyInfo)
        {
            var User_Id = UserHobbyInfo.UserId;
            await m_hobbiesRepository.AddUserHobby(UserHobbyInfo);

            return  RedirectToAction("EditUser", "Users", new { UserId = User_Id });
        }

        // GET: Users/AddHobbyFromList
        public async Task<IActionResult> AddHobbyFromList(int UserId)
        {
            ViewBag.UserId = UserId;
            List<Hobby> listHobbies = await m_hobbiesRepository.GetListHobbies();
            
            return View(listHobbies);
        }

        // Post: Users/AddHobbyFromList
        [HttpPost]
        public async Task<IActionResult> AddHobbyFromList(UserHobby UserHobbyInfo)
        {
            var User_Id = UserHobbyInfo.UserId;
            await m_hobbiesRepository.AddUserHobby(UserHobbyInfo);

            return RedirectToAction("EditUser", "Users", new { UserId = User_Id });
        }

        // GET: UserHobby/Delete
        [HttpPost]
        public async Task<IActionResult> DeleteUserHobby(int UserId, int  HobbyId)
        {
            DeleteUserHobby delHobbyView = new DeleteUserHobby();
            delHobbyView.CurrUser = await m_hobbiesRepository.GetCurrentUser(UserId);
            delHobbyView.CurrHobby= await m_hobbiesRepository.GetCurrentHobby(HobbyId);

            return View(delHobbyView);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserHobby(UserHobby  removeItem)
        {
            await m_hobbiesRepository.DeleteUserHobby(removeItem);
            return RedirectToAction("EditUser", "Users", new { UserId = removeItem.UserId });
        }
    }
}
