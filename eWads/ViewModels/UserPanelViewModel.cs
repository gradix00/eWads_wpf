using Caliburn.Micro;
using eWadsLib;
using eWadsLib.Structs;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace eWads.ViewModels
{
    public class UserPanelViewModel : Conductor<object>
    {
        public ObservableCollection<object> Posts { get; set; } = new ObservableCollection<object>();

        public async void RefreshPosts()
        {
            var getPosts = await PostService.LoadAllPosts();
            foreach (var post in getPosts)
            {
                Posts.Add(CreatePost(post));
            }
        }

        private StackPanel CreatePost(PostData post)
        {
            StackPanel panel = new StackPanel();

            TextBlock titleBlock = new TextBlock()
            {
                Text = post.Title,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 25
            };

            Image image = new Image()
            {
                Source = new BitmapImage(post.UrlImage)
            };

            StackPanel bgDescription = new StackPanel()
            {
                Background = new SolidColorBrush(Colors.CornflowerBlue),
                Margin = new Thickness(20)
            };

            TextBlock descriptionBlock = new TextBlock()
            {
                Text = post.Description,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 20,
                Foreground = new SolidColorBrush(Colors.White)
            };
            bgDescription.Children.Add(descriptionBlock);

            panel.Children.Add(titleBlock);
            panel.Children.Add(image);
            panel.Children.Add(bgDescription);

            return panel;
        }
    }
}
