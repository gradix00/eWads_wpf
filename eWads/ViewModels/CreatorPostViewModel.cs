using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using eWadsLib;
using eWadsLib.Structs;
using System.Windows.Controls;
using Caliburn.Micro;
using eWads.Structs;

namespace eWads.ViewModels
{
    public class CreatorPostViewModel : Screen
    {
        private BindableCollection<ImageData> _images = new BindableCollection<ImageData>();
        public static UserData UserData { get; set; }

        public string Title { get; set; }
        public string Description { get; set; } = "Write something...";
        public string Author { get; set; }
        public string SearchPhrase { get; set; }
        public BindableCollection<ImageData> Images 
        {
            get => _images;
            set
            {
                _images = value;
            }
        }

        public CreatorPostViewModel(UserData data)
        {
            UserData = data;
            Author = UserData.FirstName;

            Title = $"{Author} publish news on our website...";
        }

        public async void PublishPost()
        {
            var checkedItems = Images.Where(x => x.IsChecked);

            if (checkedItems.Count() > 1)
            {
                MessageBox.Show("You can select only one item!");
                return;
            }

            string url = checkedItems.Count() == 1 ? checkedItems.First().Source : "about:blank";
            await PostService.CreatePost(new PostData()
            {
                Title = Title,
                Description = Description,
                UrlImage = new Uri(url),
                Autor = UserData.FirstName + " " + UserData.LastName,
                CreationDate = DateTime.Now
            });

            AppShellViewModel.SetPage(new UserPanelViewModel(UserData), "eWads - News");
        }

        public void LoadImages()
        {
            if(_images.Count > 0) 
                _images.Clear();

            WebDataExtractor data = new WebDataExtractor();
            var imgs = data.GetWebImagesByPhrase(SearchPhrase);
            for (int x = 0; x < imgs.Length; x++)
            {
                _images.Add(new ImageData()
                {
                    Source = imgs[x]
                });
            }
        }
    }
}
