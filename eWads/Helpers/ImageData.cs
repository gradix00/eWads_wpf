using Caliburn.Micro;
using System.ComponentModel;

namespace eWads.Structs
{
    public class ImageData : Screen
    {
        private bool _isChecked;
        public bool IsChecked 
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                NotifyOfPropertyChange(()=> IsChecked);
            }
        }
        public string Source { get; set; }
    }
}
