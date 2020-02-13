using FlowerPlot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.Server.Access
{
    public class UsersDBManager : IUsersDBManager
    {
        private static UsersDBManager _instance;
        private UsersDBManager() { }
        public static UsersDBManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UsersDBManager();

                return _instance;
            }
        }

        public bool AddUser(User newUser)
        {
            using (var dbContext = new FlowerPlotsDBContext())
            {
                var isInDb = dbContext.Users.Any(u => u.Username.ToLower().Equals(newUser.Username.ToLower()));

                if (isInDb)
                {
                    return false;
                }

                User addedUser = dbContext.Users.Add(newUser);
                dbContext.SaveChanges();

                return true;
            }
        }

        public bool EditUser(User user)
        {
            using (var dbContext = new FlowerPlotsDBContext())
            {
                var isInDb = dbContext.Users.Any(u => u.Username.ToLower().Equals(user.Username.ToLower()));

                if (isInDb)
                {
                    var edit = dbContext.Users.FirstOrDefault(u => u.Username.ToLower().Equals(user.Username.ToLower()));

                    edit.Name = user.Name;
                    edit.Password = user.Password;
                    edit.Role = user.Role;
                    edit.Surname = user.Surname;
                    edit.Username = user.Username;

                    dbContext.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public User FindUser(string username)
        {
            using (var dbContext = new FlowerPlotsDBContext())
            {
                return dbContext.Users.FirstOrDefault(u => u.Username.ToLower().Equals(username.ToLower()));
            }
        }
    }
}
