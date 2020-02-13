using FlowerPlot.Common;
using FlowerPlot.LogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerPlot.ViewModel
{
    public class ProfileViewModel : BindableBase
    {
        private string name;
        private string surname;
        private string username;
        private string password;
        public static bool canSaveChanges;

        public MyICommand SaveCommand { get; set; }

        public ProfileViewModel()
        {
            SaveCommand = new MyICommand(Save, CanSave);
            Initialize();
        }

        private void Initialize()
        {
            if (MainWindow.currentUser != null)
            {
                Name = MainWindow.currentUser.Name;
                Surname = MainWindow.currentUser.Surname;
                Username = MainWindow.currentUser.Username;
                Password = MainWindow.currentUser.Password;
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
                }
            }
        }

        private bool CanSave()
        {
            return !String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(surname) && !String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password);
        }

        private void Save()
        {
            User newUser = new User
            {
                Name = Name,
                Surname = Surname,
                Password = Password,
                Username = Username,
                Id = MainWindow.currentUser.Id,
                Role = MainWindow.currentUser.Role
            };

            if (ConnectionService.Instance.proxy.EditUser(newUser))
            {
                MainWindow.currentUser = newUser;
                canSaveChanges = true;
                MainWindow.logMessage = "Your profile is changed in db.";
                LogService.Instance.LogInformation("Profile is changed.");
                LogService.Instance.SendServerInformation("Profile is changed.");
            }
            else
            {
                canSaveChanges = false;
                MainWindow.logMessage = "Error while changing profile.";
                LogService.Instance.LogError("Error while changing profile.");
                LogService.Instance.SendServerError("Error while changing profile.");
            }
        }
        
    }
}
