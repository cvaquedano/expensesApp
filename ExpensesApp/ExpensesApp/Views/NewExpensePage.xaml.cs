using ExpensesApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpensesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewExpensePage : ContentPage
    {
        NewExpenseVM ViewModel;
        public NewExpensePage()
        {
            InitializeComponent();
            ViewModel = Resources["vm"] as NewExpenseVM;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.GetExpensesStatus();
            int count = 0;
            foreach (var es in ViewModel.ExpenseStatuses)
            {
                var cell = new SwitchCell { Text = es.Name };
                Binding binding = new Binding
                {
                    Source = ViewModel.ExpenseStatuses[count],
                    Path = nameof(es.Status),
                    Mode = BindingMode.TwoWay,
                };
                cell.SetBinding(SwitchCell.OnProperty, binding);
                var section = new TableSection("Statues")
                {
                    cell
                };
                expenseTableView.Root.Add(section);
                count++;
            }
        }
    }
}