using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace eWads.Helpers
{
    public class RequestData : Screen
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Button Accept { get; set; }
        public Button Reject { get; set; }
    }
}
