using Models;
using Microsoft.AspNetCore.Mvc;
using SqLiteRepository.Interfaces;
using System.Threading.Tasks;
using HobbiesPortal.ViewModels;
using System.Collections.Generic;

namespace HobbiesPortal.Controllers
{
    public class HobbiesController : Controller
    {
        IHobbiesRepository m_hobbiesRepository;

        public HobbiesController(IHobbiesRepository currRepository)
        {
            m_hobbiesRepository = currRepository;
        }

        public IActionResult MainHobbies()
        {
            return View();
        }

        public async Task<IActionResult> HobbiesTypes()
        {
            return View(await m_hobbiesRepository.GetHobbyTypes());
        }

        [HttpPost]
        public async Task<IActionResult> HobbiesTypes( int Mode, int TypeId, string strAddTypeName)
        {
            List<HobbyType> currentList = null;

            if ( Mode == 1 )
            {
                currentList = await m_hobbiesRepository.AddHobbyType(strAddTypeName);
            }
            else
            {
                currentList = await m_hobbiesRepository.DeleteHobbyType(TypeId);
            }
            return View(currentList);
        }

        public async Task<IActionResult> HobbiesNames()
        {
            return View(await m_hobbiesRepository.GetHobbyNames());
        }

        [HttpPost]
        public async Task<IActionResult> HobbiesNames(int Mode, int NameId, string strAddNameName)
        {
            List<HobbyName> currentList = null;
            if (Mode == 1)
            {
                currentList = await m_hobbiesRepository.AddHobbyName(strAddNameName);
            }
            else
            {
                currentList = await m_hobbiesRepository.DeleteHobbyName(NameId);
            }
            return View(currentList);
        }

        public async Task<IActionResult> EditHobbies()
        {
			ListHobbiesViewModel currModel = new ListHobbiesViewModel();

			currModel.ListHobbiesTypes  = await m_hobbiesRepository.GetHobbyTypes();
			currModel.SubListHobbyNames = await m_hobbiesRepository.GetNamesNotLinkHobby();
			currModel.ListCreatedHobbies=  await m_hobbiesRepository.GetListHobbies();

            return View(currModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditHobbies(int Mode, int HobbyId, int TypeId, int NameId)
        {
            if (Mode == 1)
            {
                Hobby currHobby = new Hobby();
                currHobby.HobbyTypeId = TypeId;
                currHobby.HobbyNameId = NameId;

                await m_hobbiesRepository.AddHobby(currHobby);
            }
            else
            {
                await m_hobbiesRepository.DeleteHobby(HobbyId);
            }

            ListHobbiesViewModel currModel = new ListHobbiesViewModel();

            currModel.ListHobbiesTypes = await m_hobbiesRepository.GetHobbyTypes();
            currModel.SubListHobbyNames = await m_hobbiesRepository.GetNamesNotLinkHobby();
            currModel.ListCreatedHobbies = await m_hobbiesRepository.GetListHobbies();

            return View(currModel);
        }
    }
}