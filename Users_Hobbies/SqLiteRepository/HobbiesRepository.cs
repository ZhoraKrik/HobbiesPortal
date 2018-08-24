using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using SqLiteRepository.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SqLiteRepository
{
    public class HobbiesRepository : BaseRepository, IHobbiesRepository
    {
        public HobbiesRepository(string connectionString, IRepositoryContextFactory contextFactory) 
                : base(connectionString, contextFactory)
                { }
      
        public async Task<List<UserInfo>> GetListUsers()
        {
            var  resList=  new List <UserInfo> ();

            using (var currContext = ContextFactory.CreateDbContext(ConnectionString))
            {
                var currQuery = from u in currContext.Users
                                join uh in currContext.UserHobbies on u.Id equals uh.UserId into usershobbies
                                from item in usershobbies.DefaultIfEmpty()
                                join h in currContext.Hobbies on item.HobbyId equals h.Id  into hobbies
                                from item2 in hobbies.DefaultIfEmpty()
                                join th in currContext.HobbyTypes on item2.HobbyTypeId equals th.Id into typehobbies
                                from item3 in typehobbies.DefaultIfEmpty()
                                join nh in currContext.HobbyNames on item2.HobbyNameId equals nh.Id into namehobbies
                                from item4 in namehobbies.DefaultIfEmpty()
                                select new UserInfo
                                {
                                    CurrUser= u,

                                    HobbyType = (item3 == null) ? "" : item3.Name,
                                    HobbyName=  (item4 == null) ? "" : item4.Name
                                };


                resList = await currQuery.AsNoTracking().ToListAsync();
            }

            return resList;
        }

        public async Task<List<HobbyType>> GetHobbyTypes()
        {
            var retList = new List<HobbyType>();

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var  query=  context.HobbyTypes.AsQueryable();
                retList = await query.AsNoTracking().ToListAsync();
            }

            return retList;
        }

        public async Task<List<HobbyName>> GetHobbyNames()
        {
            var retList = new List<HobbyName>();

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var query = context.HobbyNames.AsQueryable();
                retList = await query.AsNoTracking().ToListAsync();
            }

            return retList;
        }

        public async Task<List<HobbyName>> GetHobbyNames(int nHobbyType)
        {
            var retList = new List<HobbyName>();

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var query = context.Hobbies.AsQueryable();
                query = query.Where(h => h.HobbyTypeId.Equals(nHobbyType));

                var HobbiesNames = query.Select(h => h.HobbyNames);
                retList = await HobbiesNames.AsNoTracking().ToListAsync();
            }

            return retList;
        }

        public async Task<int> AddNewUser(Models.User UserInfo)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Users.Add(UserInfo);
                await context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<User> GetCurrentUser( int  nUserId )
        {
            var retUser = new User();

            using (var currContext = ContextFactory.CreateDbContext(ConnectionString))
            {
                var currQuery = from u in currContext.Users where u.Id.Equals(nUserId)
                                select new User
                                {
                                    Id = u.Id,
                                    Name = u.Name,
                                    Sex = u.Sex,
                                    BirthDay = u.BirthDay,
                                    Age = u.Age
                                };
                
                var currUser = await currQuery.AsNoTracking().ToListAsync();
                retUser = currUser.First();
            }

            return retUser;
        }

        public async Task<List<Hobby>> GetUserHobbies(int nUserId)
        {
            var retListHobbies = new List <Hobby>();
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var currQuery = from uh in context.UserHobbies
                                where (uh.UserId.Equals(nUserId))
                                join h in context.Hobbies on uh.HobbyId equals h.Id
                                select new Hobby
                                {
                                    Id= h.Id,

                                    HobbyTypeId= h.HobbyTypeId,
                                    HobbyTypes= h.HobbyTypes,

                                    HobbyNameId= h.HobbyNameId,
                                    HobbyNames= h.HobbyNames
                                };

                retListHobbies = await currQuery.AsNoTracking().ToListAsync(); ;
            }

            return retListHobbies;
        }

        public async Task<int> EditUser(User currUser)
        {
            int retResult = 0;

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Entry(currUser).State= EntityState.Modified;
                await context.SaveChangesAsync();
            }

            return retResult;
        }

        public async Task<int> DeleteUser(int UserId)
        {
            int  retResult = 0;

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                User  currUser= context.Users.FirstOrDefault(u => u.Id == UserId);
                if (currUser == null)
                    retResult = -1;
                else
                {
                    context.Users.Remove(currUser);
                    await context.SaveChangesAsync();
                }
            }

            return retResult;
        }

        public async Task<List<Hobby>> GetListHobbies()
        {
            var resList = new List<Hobby>();

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var query = context.Hobbies.AsQueryable();
                query = query.Include(h => h.HobbyTypes).Include(h => h.HobbyNames);
                resList = await query.AsNoTracking().ToListAsync(); 
            }

            return resList;
        }

		public async Task<List<HobbyName>> GetNamesNotLinkHobby()
		{
			var resHobbies = new List<HobbyName>();

			using (var context = ContextFactory.CreateDbContext(ConnectionString))
			{
				var query = from nh in context.HobbyNames
							where !context.Hobbies.Any(h => (h.HobbyNameId==nh.Id))
							select new HobbyName
							{
								Id= nh.Id,
								Name= nh.Name
							};
				
				resHobbies = await query.AsNoTracking().ToListAsync();
			}

			return resHobbies;
		}

		public async Task<List<Hobby>> GetListHobbiesOnTypeId(int nTypeId)
        {
            var resHobbies = new List<Hobby>();

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var query = context.Hobbies.AsQueryable();
                query = query.Where(h=>h.HobbyTypeId.Equals(nTypeId)).
                            Include(h => h.HobbyTypes).Include(h => h.HobbyNames);
                resHobbies = await query.AsNoTracking().ToListAsync();
            }

            return resHobbies;
        }

        public async Task<int> AddUserHobby(UserHobby addHobby)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.UserHobbies.Add(addHobby);
                await context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<Hobby> GetCurrentHobby(int nHobbyId)
        {
            var retHobby = new Hobby();

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                var query = context.Hobbies.AsQueryable();
                query = query.Where(h => h.Id.Equals(nHobbyId)).
                            Include(h => h.HobbyTypes).Include(h => h.HobbyNames);
                var  tmpResult = await query.AsNoTracking().ToListAsync();

                if (tmpResult != null)
                    retHobby = tmpResult.First();
            }

            return retHobby;
        }

        public async Task<int> DeleteUserHobby(UserHobby removeItem)
        {
            int retResult = 0;

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                UserHobby currUserHobby = context.UserHobbies.FirstOrDefault(uh => uh.UserId.Equals(removeItem.UserId)
                                                                        & uh.HobbyId.Equals(removeItem.HobbyId));
                if (currUserHobby == null)
                    retResult = -1;
                else
                {
                    context.UserHobbies.Remove(currUserHobby);
                    await context.SaveChangesAsync();
                }
            }

            return retResult;
        }

        public async Task<List<HobbyType>> AddHobbyType(string strAddTypeName)
        {
            List<HobbyType> retList = new List<HobbyType>();

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                HobbyType currType = new HobbyType();
                currType.Name = strAddTypeName;

                context.HobbyTypes.Add(currType);
                await context.SaveChangesAsync();

                retList = await GetHobbyTypes();
            }

            return retList;
        }

        public async Task<List<HobbyType>> DeleteHobbyType(int nTypeId)
        {
            List<HobbyType> retList = new List<HobbyType>();

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                HobbyType currType = context.HobbyTypes.FirstOrDefault(ht => ht.Id.Equals(nTypeId));                                                     
                if (currType != null)
                {
                    context.HobbyTypes.Remove(currType);
                    await context.SaveChangesAsync();

                    retList = await GetHobbyTypes();
                }
            }

            return retList;
        }

        public async Task<List<HobbyName>> AddHobbyName(string strAddNameName)
        {
            List<HobbyName> retList = new List<HobbyName>();

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                HobbyName currName = new HobbyName();
                currName.Name = strAddNameName;

                context.HobbyNames.Add(currName);
                await context.SaveChangesAsync();

                retList = await GetHobbyNames();
            }

            return retList;
        }

        public async Task<List<HobbyName>> DeleteHobbyName(int nNameId)
        {
            List<HobbyName> retList = new List<HobbyName>();

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                HobbyName currName = context.HobbyNames.FirstOrDefault(ht => ht.Id.Equals(nNameId));
                if (currName != null)
                {
                    context.HobbyNames.Remove(currName);
                    await context.SaveChangesAsync();

                    retList = await GetHobbyNames();
                }
            }

            return retList;
        }

        public async Task<int> AddHobby(Hobby addHobby)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Hobbies.Add(addHobby);
                await context.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> DeleteHobby(int nHobbyId)
        {
            int nResult = -1;

            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                Hobby  delHobby= context.Hobbies.FirstOrDefault(h => h.Id.Equals(nHobbyId));
                if (delHobby != null)
                {
                    context.Hobbies.Remove(delHobby);
                    await context.SaveChangesAsync();

                    nResult = 0;
                }
            }

            return nResult;
        }
    }
}
