using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_App_Shell.Models
{
    public class ListItem : ObjectModelBase
    {
        private String name;

        public String Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }

        private String notes;

        public String Notes
        {
            get
            {
                return notes;
            }
            set
            {
                notes = value;
                NotifyPropertyChanged();
            }
        }

        public ListItem(String Name)
        {
            this.Name = Name;
        }

    }
}
