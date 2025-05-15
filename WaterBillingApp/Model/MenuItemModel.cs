using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WaterBillingApp.Model
{
    public class MenuItemModel
    {
        public string Title { get; set; }
        public ICommand NavigateCommand { get; set; }

        public MenuItemModel(string title, ICommand command)
        {
            Title = title;
            NavigateCommand = command;
        }
    }

}
