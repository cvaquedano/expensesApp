using ExpensesApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpensesApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensesPage : ContentPage
    {
        ExpenseVM viewModel;
        public ExpensesPage()
        {
            InitializeComponent();
            viewModel = Resources["vm"] as ExpenseVM;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.GetExpenses();
        }
    }
}