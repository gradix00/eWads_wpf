using Caliburn.Micro;
using eWads.Helpers;
using eWadsLib;
using eWadsLib.Structs;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace eWads.ViewModels
{
    public class UserPanelViewModel : Conductor<object>
    {
        private DispatcherTimer _timer;
        public ObservableCollection<PostData> Posts { get; set; } = new ObservableCollection<PostData>();
        public ObservableCollection<RequestData> Requests { get; set; } = new ObservableCollection<RequestData>();
        public ObservableCollection<UserData> NewUsers { get; set; } = new ObservableCollection<UserData>();
        public static UserData UserData { get; set; }

        //Panel with posts
        private Visibility _postsPanel;
        public Visibility PostsPanel
        {
            get => _postsPanel;
            set
            {
                _postsPanel = value;
                NotifyOfPropertyChange(() => PostsPanel);
            }
        }

        //Text loading...
        private Visibility _loadingText = Visibility.Hidden;
        public Visibility LoadingText
        {
            get => _loadingText;
            set
            {
                _loadingText = value;
                NotifyOfPropertyChange(() => LoadingText);
            }
        }

        public UserPanelViewModel(UserData data)
        {
            UserData = data;
            RefreshPosts();
            GetFriendRequests(null, null);

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Tick += GetFriendRequests;
            _timer.Start();
        }

        public void OpenCreatorPost()
        {
            AppShellViewModel.SetPage(new CreatorPostViewModel(UserData), "eWads - New post");
        }

        public void OpenWindowProfile()
        {
            string username = $"{UserData.FirstName} {UserData.LastName}";
            AppShellViewModel.SetPage(new ProfileViewModel(username), $"eWads - {username}");
        }

        public async void AcceptRequest(object sender)
        {
            var btn = sender as Button;
            
            FriendshipService request = new FriendshipService();
            request.CurrentUser = UserData;
            request.OtherUser = await UserService.GetInfoUser(int.Parse(btn.CommandParameter.ToString()));
            await request.SetFriendRequestStatus(eWadsLib.Helpers.Enums.InvitationStatus.Accepted);
        }

        public async void RejectRequest(object sender)
        {
            var btn = sender as Button;

            FriendshipService request = new FriendshipService();
            request.CurrentUser = UserData;
            request.OtherUser = await UserService.GetInfoUser(int.Parse(btn.CommandParameter.ToString()));
            await request.SetFriendRequestStatus(eWadsLib.Helpers.Enums.InvitationStatus.Rejected);
        }

        public async void RefreshPosts()
        {
            PostsPanel = Visibility.Hidden;
            LoadingText = Visibility.Visible;
            Posts.Clear();

            var getPosts = await PostService.LoadAllPosts(5);
            foreach (var post in getPosts)
            {
                Posts.Add(post);
            }

            PostsPanel = Visibility.Visible;
            LoadingText = Visibility.Hidden;
            GetNewUsers();
        }

        private async void GetNewUsers()
        {
            NewUsers.Clear();
            var users = await UserService.GetAllUsers();
            for(int x = 0; x < users.Length; x++)
            {
                NewUsers.Add(users[x]);
            }
        }

        private async void GetFriendRequests(object sender, EventArgs e)
        {           
            FriendshipService friends = new FriendshipService();
            friends.CurrentUser = UserData;
            var requests = await friends.GetAllFriendRequest();
            Requests.Clear();

            foreach (var req in requests) 
            {
                Requests.Add(new RequestData()
                {
                    FirstName = req.FirstName,
                    LastName = req.LastName,
                    Accept = new Button() { Content = "Accept" },
                    Reject = new Button() { Content = "Reject" },
                    UserId = req.Id
                });
            }
        }
    }
}
