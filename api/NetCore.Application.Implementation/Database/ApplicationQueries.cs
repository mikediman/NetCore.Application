using Dapper;
using System.Threading.Tasks;

namespace NetCore.Application.Implementation
{
    public static class ApplicationQueries
    {
        public static async Task<int> RegisterUserInDB(DBProperties dbProps, RegistrationUserWrapper wrapper)
        {
            string query = "INSERT INTO [dbo].[Customers] ([Id],[FirstName], [LastName], [Email], [TransactionDate]) VALUES (@id, @FirstName, @LastName, @Email, @TransactionDate)";
            int result = dbProps.con.Execute(query, wrapper.criteria, dbProps.tran);
            return result;
        }
    }
}
