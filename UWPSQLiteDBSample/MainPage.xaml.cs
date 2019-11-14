using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UWPSQLiteDBSample.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPSQLiteDBSample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private ObservableCollection<Post> _posts;

        public ObservableCollection<Post> Posts
        {
            get { return _posts; }
            set
            {
                _posts = value;
                OnPropertyChanged(nameof(Posts));
            }
        }

        public MainPage()
        {
            this.InitializeComponent();

            Posts = new ObservableCollection<Post>();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Posts.Clear();

                var posts = await GetAsync();
                posts.ForEach(p => Posts.Add(p));
            }
            catch (Exception)
            {
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var post = new Post()
                {
                    Title = TitleTextBox.Text,
                    Content = ContentTextBox.Text,
                };

                if (await AddAsync(post))
                {
                    Posts.Add(post);
                }
            }
            catch (Exception)
            {
            }
        }

        private async Task<bool> AddAsync(Post post)
        {
            try
            {
                using (var db = new UWPSQLiteDbContext())
                {
                    db.Posts.Add(post);
                    return (await db.SaveChangesAsync()) > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<Post>> GetAsync()
        {
            try
            {
                using (var db = new UWPSQLiteDbContext())
                {
                    return await db.Posts.ToListAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
