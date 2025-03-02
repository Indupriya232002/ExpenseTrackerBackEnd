using DataAccessLayer.Data;
using EntityLayer.Interfaces;
using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ExpenseRepo : IExpenseRepo
    {
        ExpenseDbContext _dbContext;
        public ExpenseRepo(ExpenseDbContext expenseDb)
        {
            _dbContext = expenseDb;
        }
        public Expenses AddExpense(Expenses expenses)
        {
            _dbContext.Expenses.Add(expenses);
            _dbContext.SaveChanges();
            return expenses;
        }


        public object DeleteExpenseById(int id)
        {
            var obj = _dbContext.Expenses.Find(id)
;
            if (obj != null)
            {
                _dbContext.Expenses.Remove(obj);
                _dbContext.SaveChanges();
            }
            return "Expense Deleted Successfully";
        }

        public bool DeleteExpenseByUserIdAndExpenseId(int userId, int expenseId)
        {
            var expense = _dbContext.Expenses.FirstOrDefault(t => t.expenseId == expenseId && t.userId == userId);
            if (expense != null)
            {
                _dbContext.Expenses.Remove(expense);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Expenses> GetAllExpenses()
        {
            return _dbContext.Expenses.OrderByDescending(x => x.Date).ToList();
        }

        public List<Expenses> GetExpenseByUserId(int userId)
        {
            return _dbContext.Expenses.Where(t => t.userId == userId).OrderByDescending(x => x.Date).ToList();
        }

        public object UpdateExpenses(Expenses expenses)
        {
            _dbContext.Expenses.Update(expenses);
            _dbContext.SaveChanges();
            return expenses;
        }

        public List<Expenses> GetDailyExpenses()
        {
            DateTime today = DateTime.Today.Date; // Get today's date

            var expenses = _dbContext.Expenses
                .Where(e => e.Date.Date == today).OrderByDescending(x => x.Date) // Filter today's expenses
                .ToList();

            return expenses; // Return all today's expenses
        }

        public List<object> GetExpensesByDateRange(DateTime startDate, DateTime endDate, int userId)
        {
            var expenses = _dbContext.Expenses
                .Where(e => e.Date >= startDate && e.Date <= endDate && e.userId == userId) // Ensure only logged-in user's data is fetched
                .ToList();

            var totalAmount = expenses.Sum(e => e.expenditure);

            var result = new
            {
                TotalAmount = totalAmount,
                Expenses = expenses.Select(e => new
                {
                    e.name,
                    e.category,
                    e.expenditure,
                    e.Date
                }).OrderByDescending(x => x.Date).ToList()
            };

            return new List<object> { result };
        }


    }

}