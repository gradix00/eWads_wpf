using System;
using System.Windows;
using eWadsLib;
using eWadsLib.Structs;

namespace eWads.ViewModels
{
    public class CreatorPostViewModel
    {
        public static UserData UserData { get; set; }

        public string Title { get; set; }
        public string Description { get; set; } = "Write something...";
        public string Author { get; set; }

        public CreatorPostViewModel(UserData data)
        {
            UserData = data;
            Author = UserData.FirstName;

            Title = $"{Author} publish news on our website...";
        }

        public async void PublishPost()
        {
            await PostService.CreatePost(new PostData()
            {
                Title = Title,
                Description = Description,
                UrlImage = new Uri("https://st4.depositphotos.com/14953852/24787/v/600/depositphotos_247872612-stock-illustration-no-image-available-icon-vector.jpg"),
                AutorId = UserData.Id,
                CreationDate = DateTime.Now
            });

            Title = string.Empty;
            Description = string.Empty;

            AppShellViewModel.SetPage(new UserPanelViewModel(UserData), "eWads - News");
        }
    }
}
