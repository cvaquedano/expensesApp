using System.Threading.Tasks;

namespace ExpensesApp.Interfaces
{
    public interface IShare
    {
        Task Show(string tittle, string message, string filePath);
    }
}
