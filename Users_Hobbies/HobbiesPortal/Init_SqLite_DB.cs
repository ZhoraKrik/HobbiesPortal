using Models;
using System.Linq;
using SqLiteRepository;

namespace HobbiesPortal
{
    public class Init_SqLite_DB
    {
        public static void Initialize(RepositoryContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                 var listUsers = new User[]
                 {
                    new User{ Name="Петренко Евгений", Sex="Муж",  BirthDay="20.10.2000", Age=18 },
                    new User{ Name="Зырянов Василий", Sex="Муж",  BirthDay="20.10.2001", Age=17 },
                    new User{ Name="Федоравичус Виктор", Sex="Муж",  BirthDay="20.10.2002", Age=16 },
                    new User{ Name="Антонов Евгений", Sex="Жен", BirthDay="20.10.2003", Age=15 },
                    new User{ Name="Царёва Светлана", Sex="Жен", BirthDay="20.10.2004", Age=14 },
                    new User{ Name="Зырянова Ольга", Sex="Жен", BirthDay="20.10.1999", Age=19 },
                    new User{ Name="Смирнова Тоня", Sex="Жен", BirthDay="20.10.1998", Age=20 }
                 };

                foreach (User currUser in listUsers)
                {
                    dbContext.Users.Add(currUser);
                }
                dbContext.SaveChanges();
            }

            if (!dbContext.HobbyTypes.Any())
            {

                var listTypesHobbies = new HobbyType[]
                {
                    new HobbyType{Name="Спорт"},
                    new HobbyType{Name="Кино"},
                    new HobbyType{Name="Рукоделие"},
                };
                foreach (HobbyType currType in listTypesHobbies)
                {
                    dbContext.HobbyTypes.Add(currType);
                }
                dbContext.SaveChanges();
            }

            if (!dbContext.HobbyNames.Any())
            {
                var listNamesHobbies = new HobbyName[]
                {
                    new HobbyName{Name="Хоккей"},
                    new HobbyName{Name="Футбол"},
                    new HobbyName{Name="Теннис"},
                    new HobbyName{Name="Боевик"},
                    new HobbyName{Name="Драма"},
                    new HobbyName{Name="Кинокомедия"},
                    new HobbyName{Name="Вышивание"},
                    new HobbyName{Name="Выжигание"},
                    new HobbyName{Name="Рисование"},
                };
                foreach (HobbyName currName in listNamesHobbies)
                {
                    dbContext.HobbyNames.Add(currName);
                }
                dbContext.SaveChanges();
            }

            if (!dbContext.Hobbies.Any())
            {
                var listHobbies = new Hobby[]
                {
                    new Hobby{HobbyTypeId=1, HobbyNameId=1},
                    new Hobby{HobbyTypeId=1, HobbyNameId=2},
                    new Hobby{HobbyTypeId=1, HobbyNameId=3},

                    new Hobby{HobbyTypeId=2, HobbyNameId=4},
                    new Hobby{HobbyTypeId=2, HobbyNameId=5},
                    new Hobby{HobbyTypeId=2, HobbyNameId=6},

                    new Hobby{HobbyTypeId=3, HobbyNameId=7},
                    new Hobby{HobbyTypeId=3, HobbyNameId=8},
                    new Hobby{HobbyTypeId=3, HobbyNameId=9},
                };
                foreach (Hobby currType in listHobbies)
                {
                    dbContext.Hobbies.Add(currType);
                }
                dbContext.SaveChanges();
            }

            if (!dbContext.UserHobbies.Any())
            {
                var listUserHobbies = new UserHobby[]
                {
                    new UserHobby{UserId=1,HobbyId=1},
                    new UserHobby{UserId=1,HobbyId=4},
                    new UserHobby{UserId=1,HobbyId=8},

                    new UserHobby{UserId=2,HobbyId=2},
                    new UserHobby{UserId=2,HobbyId=6},

                    new UserHobby{UserId=3,HobbyId=3},
                    new UserHobby{UserId=3,HobbyId=4},

                    new UserHobby{UserId=4,HobbyId=2},
                    new UserHobby{UserId=4,HobbyId=6},
                    new UserHobby{UserId=4,HobbyId=7},

                    new UserHobby{UserId=5,HobbyId=5},
                    new UserHobby{UserId=5,HobbyId=7},
                };
                foreach (UserHobby currUserHobby in listUserHobbies)
                {
                    dbContext.UserHobbies.Add(currUserHobby);
                }
                dbContext.SaveChanges();
            }
        }
    }
}
