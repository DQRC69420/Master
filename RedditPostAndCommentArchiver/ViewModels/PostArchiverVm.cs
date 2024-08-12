using RedditPostAndCommentArchiver.Data;
using RedditPostAndCommentArchiver.RedditServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RedditPostAndCommentArchiver.ViewModels
{
    internal class PostArchiverVm : INotifyPropertyChanged
    {

        private string _postAsJson = "";

        public string PostAsJson
        {
            get => _postAsJson;
            set
            {
                _postAsJson = value;
                NotifyPropertyChanged();
            }
        }


        private string _selectedPath = string.Empty;
        public string SelectedPath
        {
            get => _selectedPath;
            set
            {
                _selectedPath = value;
                NotifyPropertyChanged();
            }
        }


        public ICommand SubmitJson
        {
            get
            {
                var command = new Command(() =>
                {
                    RedditServices.RedditServices.ConvertToPost(PostAsJson, SelectedPath);
                    PostAsJson = string.Empty;
                });

                return command;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
