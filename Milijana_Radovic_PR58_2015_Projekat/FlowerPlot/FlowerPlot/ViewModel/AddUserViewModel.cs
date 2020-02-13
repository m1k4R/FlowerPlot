using FlowerPlot.Common;
using FlowerPlot.LogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.ViewModel
{
    public class AddUserViewModel : BindableBase
    {
        private string name;
        private string surname;
        private string username;
        private string password;
        private string role;
        private List<string> roleList;
        public MyICommand AddUserCommand { get; set; }

        public AddUserViewModel()
        {
            AddUserCommand = new MyICommand(AddUser, CanAddUser);
            RoleList = new List<string> { "admin", "customer" };
        }

        public List<string> RoleList
        {
            get
            {
                return roleList;
            }

            set
            {
                if (roleList != value)
                {
                    roleList = value;
                }
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
                    AddUserCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged("Username");
                    AddUserCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                    AddUserCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (surname != value)
                {
                    surname = value;
                    OnPropertyChanged("Surname");
                    AddUserCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Role
        {
            get
            {
                return role;
            }
            set
            {
                if (role != value)
                {
                    role = value;
                    OnPropertyChanged("Surname");
                    AddUserCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private bool CanAddUser()
        {
            return !String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(surname) && !String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password);
        }

        private void AddUser()
        {
            User newUser = new User
            {
                Name = Name,
                Surname = Surname,
                Password = Password,
                Username = Username,
                Role = Role
            };

            if (ConnectionService.Instance.proxy.AddNewUser(newUser))
            {
                MainWindow.logMessage = "New user is added to db.";
                LogService.Instance.LogInformation("New user is added to db.");
                LogService.Instance.SendServerInformation("New user is added to db.");
                MainWindowViewModel.Instance.OnNav("flowerPlots");
            }
            else
            {
                MainWindow.logMessage = "Error while adding new user.";
                LogService.Instance.LogError("Error while adding new user.");
                LogService.Instance.SendServerError("Error while adding new user.");
            }
        }
    }
}
