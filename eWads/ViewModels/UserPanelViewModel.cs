﻿using Caliburn.Micro;
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
        public ObservableCollection<PostData> Posts { get; set; } = new ObservableCollection<PostData>();
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
        }

        public async void RefreshPosts()
        {
            PostsPanel = Visibility.Hidden;
            LoadingText = Visibility.Visible;

            //CreateItem(new CreatorPostViewModel(UserData));
            var getPosts = await PostService.LoadAllPosts();
            foreach (var post in getPosts)
                Posts.Add(post);

            PostsPanel = Visibility.Visible;
            LoadingText = Visibility.Hidden;
        }

        private StackPanel CreateItem(object item)
        {
            StackPanel panel = new StackPanel();
            ContentControl content = new ContentControl()
            {
                Name = "ActiveItem",
                Height = 250
            };

            panel.Children.Add(content);
            ActivateItemAsync(item);
            return panel;
        }
    }
}
