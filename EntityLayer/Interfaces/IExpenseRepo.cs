using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Interfaces
{
    public interface IExpenseRepo
    {
        Expenses AddExpense(Expenses expenses);
        List<Expenses> GetAllExpenses();

        object UpdateExpenses(Expenses expenses);

        object DeleteExpenseById(int id);
        List<Expenses> GetExpenseByUserId(int userId);

        bool DeleteExpenseByUserIdAndExpenseId(int userId, int expenseId);

        List<Expenses> GetDailyExpenses();
     
        List<object> GetExpensesByDateRange(DateTime startDate, DateTime endDate,int userId);
    }
}
