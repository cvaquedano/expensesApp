using ExpensesApp.Interfaces;
using ExpensesApp.Models;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ExpensesApp.ViewModels
{
    public class CategoriesVM
    {
        public ObservableCollection<string> Categories { get; set; }
        public ObservableCollection<CategoryExpenses> CategoryExpensesCollection { get; set; }
        public Command ExportCommand { get; set; }

        public CategoriesVM()
        {
            ExportCommand = new Command(ShareReport);
            Categories = new ObservableCollection<string>();
            CategoryExpensesCollection = new ObservableCollection<CategoryExpenses>();
            GetCategories();
            GetExpensesPerCategory();
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

        public void GetExpensesPerCategory()
        {
            CategoryExpensesCollection.Clear();
            float totalExpensesAmmount = Expense.TotalExpensesAmmount();
            foreach (var c in Categories)
            {
                var expenses = Expense.GetExpenses(c);
                float expensesAmmountInCategory = expenses.Sum(e => e.Ammount);
                CategoryExpenses ce = new CategoryExpenses()
                {
                    Category = c,
                    EspensesPercentage = expensesAmmountInCategory / totalExpensesAmmount
                };
                CategoryExpensesCollection.Add(ce);
            }
        }
        public class CategoryExpenses
        {
            public string Category { get; set; }
            public float EspensesPercentage { get; set; }
        }



        public async void ShareReport()
        {
            IFileSystem fileSystem = FileSystem.Current;
            IFolder rootFolder = fileSystem.LocalStorage;
            IFolder reportsFolder = await rootFolder.CreateFolderAsync("reports", CreationCollisionOption.OpenIfExists);

            var txtFile = await reportsFolder.CreateFileAsync("report.txt", CreationCollisionOption.ReplaceExisting);

            using(StreamWriter sw = new StreamWriter(txtFile.Path))
            {
                foreach (var ce in CategoryExpensesCollection)
                {
                    sw.WriteLine($"{ce.Category} - {ce.EspensesPercentage}");
                }
            }

            var shareDependency = DependencyService.Get<IShare>();

            await shareDependency.Show("Expense Report", "Here is your expenses report", txtFile.Path);

        }
    }
}
