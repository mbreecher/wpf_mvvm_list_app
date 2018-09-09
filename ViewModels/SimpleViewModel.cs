using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_MVVM_App_Shell.Models;

namespace WPF_MVVM_App_Shell.ViewModels
{
    public class SimpleViewModel : INotifyPropertyChanged
    {
        // declare variables
        private ObservableCollection<ListItem> _listOfThings = new ObservableCollection<ListItem>();

        public ObservableCollection<ListItem> ListOfThings
        {
            get
            {
                return _listOfThings;
            }
        }


        private ListItem selectedItem;

        public ListItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                NotifyPropertyChanged();
            }
        }

        private String myVar;

        public String MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }




        // constructor
        public SimpleViewModel()
        {
            btnAddItem = new RelayCommand(new Action<object>(AddItem));
            btnRemoveItem = new RelayCommand(new Action<object>(RemoveItem));
        }

#region Button Action Methods
        public void AddItem(object Name)
        {
            try
            {
                string newName = Microsoft.VisualBasic.Interaction.InputBox("New Item Name?");
                if (!string.IsNullOrEmpty(newName))
                {
                    ListItem newItem = new ListItem(newName);
                    _listOfThings.Add(newItem);
                    SelectedItem = newItem;
                    NotifyPropertyChanged("ListOfThings");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"failed to add item {Environment.NewLine}{e.InnerException}");
            }
        }

        public void RemoveItem(object Name)
        {
            try
            {
                if (SelectedItem != null)
                {
                    int position = Math.Min(_listOfThings.IndexOf(SelectedItem), _listOfThings.Count() - 2);
                    _listOfThings.Remove(SelectedItem); // <-- this is all we need to do to remove a thing, but having nothing selected is undesirable.
                    SelectedItem = _listOfThings.ElementAtOrDefault(position);
                    NotifyPropertyChanged("ListOfThings");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"failed to remove item {Environment.NewLine}{e.InnerException}");
            }
        }
#endregion

#region Button Action Commands
        public ICommand btnAddItem { get; set; }
        public ICommand btnRemoveItem { get; set; }
#endregion

        // Implement the INotifyPropertyChanged Interface
        // I typically factor everything below into a base view model class (like I've done in the model folder)
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Implement ICommand Interface to connect button actions to methods
        protected class RelayCommand : ICommand
        {
            private Action<object> _action;

            public RelayCommand(Action<object> action)
            {
                _action = action;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                _action(parameter);
            }
        }
    }
}
