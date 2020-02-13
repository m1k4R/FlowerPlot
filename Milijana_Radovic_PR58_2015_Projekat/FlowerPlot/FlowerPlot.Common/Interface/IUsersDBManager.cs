using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.Common
{
    public interface IUsersDBManager
    {
        User FindUser(string username);
        bool AddUser(User newUser);
        bool EditUser(User user);
    }
}
