using EntityLayer.Interfaces;
using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ExpenseService
    {
        IExpenseRepo _expenseRepo;

        public ExpenseService(IExpenseRepo expenseRepo)
        {
            _expenseRepo = expenseRepo;
        }
        public Expenses AddExpense(Expenses expenses)
        {
            return _expenseRepo.AddExpense(expenses);
        }

        public List<Expenses> GetAllExpenses()
        {
            return _expenseRepo.GetAllExpenses();
        }
        public List<Expenses> GetExpenseByUserId(int userId)
        {
            return _expenseRepo.GetExpenseByUserId(userId);
        }

        public object UpdateExpenses(Expenses expenses)
        {
            return _expenseRepo.UpdateExpenses(expenses);
        }

        public object DeleteExpenseById(int id)
        {
            return _expenseRepo.DeleteExpenseById(id);
        }

        public bool DeleteExpenseByUserIdAndExpenseId(int userId, int expenseId)
        {
            return _expenseRepo.DeleteExpenseByUserIdAndExpenseId(userId, expenseId);
        }

        public List<Expenses> GetDailyExpenses()
        {
            return _expenseRepo.GetDailyExpenses();
        }

        public List<object> GetExpensesByDateRange(DateTime startDate, DateTime endDate,int userId)
        {
            return _expenseRepo.GetExpensesByDateRange(startDate, endDate, userId);
        }

    }
}
