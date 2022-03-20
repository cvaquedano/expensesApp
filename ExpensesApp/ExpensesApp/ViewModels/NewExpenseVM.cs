using ExpensesApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace ExpensesApp.ViewModels
{
    public class NewExpenseVM : INotifyPropertyChanged
    {
        private string expenseName;
        public string ExpenseName { get { return expenseName; } set { expenseName = value; OnPropertyChanged("ExpenseName"); } }

        private string expenseDescription;
        public string ExpenseDescription { get { return expenseDescription; } set { expenseDescription = value; OnPropertyChanged("Description"); } }

        private float expenseAmmount;
        public float ExpenseAmmount { get { return expenseAmmount; } set { expenseAmmount = value; OnPropertyChanged("Ammount"); } }

        private DateTime expenseDate;
        public DateTime ExpenseDate { get { return expenseDate; } set { expenseDate = value; OnPropertyChanged("ExpenseDate"); } }

        private string expenseCategory;
        public string ExpenseCategory { get { return expenseCategory; } set { expenseCategory = value; OnPropertyChanged("Category"); } }

        public Command SaveExpenseCommand { get; set; }

        public ObservableCollection<string> Categories { get; set; } = new ObservableCollection<string>();
        public NewExpenseVM()
        {
            ExpenseDate = DateTime.Today;
            GetCategories();
            SaveExpenseCommand = new Command(InserExpense);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void InserExpense()
        {
            Expense expense = new Expense()
            {
                Name = ExpenseName,
                Description = ExpenseDescription,
                Category = ExpenseCategory,
                Ammount = ExpenseAmmount,
                Date = ExpenseDate
            };
            var response = Expense.InsertExpense(expense);
            if (response > 0)
                Application.Current.MainPage.Navigation.PopAsync();
            else
                Application.Current.MainPage.DisplayAlert("Error", "No items were inserter", "ok");
        }
        private void GetCategories()
        {
            Categories.Clear();
            Categories.Add("Housing");
            Categories.Add("Debt");
            Categories.Add("Health");
            Categories.Add("Food");
            Categories.Add("Personal");
            Categories.Add("Travel");
            Categories.Add("Other");
        }

       
    }
}
