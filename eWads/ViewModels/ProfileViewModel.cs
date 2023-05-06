using Caliburn.Micro;
using eWadsLib;
using eWadsLib.Helpers.Enums;
using eWadsLib.Structs;
using System.Windows;
using System.Windows.Controls;

namespace eWads.ViewModels
{
    public class ProfileViewModel : Conductor<object>
    {
        private string _name;
        private AccountStatus _userStatus;
        private Gender _gender;
        private Button _invite;
        public static UserData UserData { get; set; }
        public Button Invite 
        {
            get => _invite;
            set
            {
                _invite = value;
                NotifyOfPropertyChange(() => Invite);
            }
        }

        public string Name 
        {
            get => _name;
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
        public AccountStatus UserStatus 
        {
            get => _userStatus;
            set
            {
                _userStatus = value;
                NotifyOfPropertyChange(() => UserStatus);
            }
        }
        public Gender Gender 
        {
            get => _gender;
            set
            {
                _gender = value;
                NotifyOfPropertyChange(() => Gender);
            }
        }

        public ProfileViewModel(string username)
        {
            LoadInfoProfile(username);
        }

        public async void SentRequestFriend()
        {
            FriendshipService request = new FriendshipService(UserData, UserData);
            var res = await request.SendFriendRequest();

            if (res)
                MessageBox.Show("Wysłano zaproszenie!");
            else
                MessageBox.Show("Nie udało się wysłać zaproszenia...");
        }

        public void ReturnToMainPage()
        {
            AppShellViewModel.SetPage(new UserPanelViewModel(UserData), "eWads - News");
        }

        private async void LoadInfoProfile(string username)
        {
            string firstName = username.Split(' ')[0];
            string lastName = username.Split(' ')[1];
            UserData = await UserService.GetInfoUser(firstName, lastName);

            Name = UserData.FirstName + " " + UserData.LastName;
            UserStatus = UserData.Status;
            Gender = UserData.Gender;

            FriendshipService request = new FriendshipService(UserData, UserData);
            var res = await request.GetFriendRequestStatus();

            if (res == InvitationStatus.Waiting)
            {
                Invite = new Button()
                {
                    Content = "Already invited",
                    IsEnabled = false
                };
            }
            else
            {
                Invite = new Button()
                {
                    Content = "Add friend",
                    IsEnabled = true
                };
            }
        }
    }
}
