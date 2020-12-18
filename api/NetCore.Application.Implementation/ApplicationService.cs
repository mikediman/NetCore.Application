using Microsoft.Extensions.Logging;
using NetCore.Application.Interfaces;
using NetCore.Application.Types;
using System;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCore.Application.Implementation
{
    public class ApplicationService : IApplicationService
    {
        ILogger logger;
        private readonly IDbConnection dbConnection;
        private readonly IHttpClientFactory httpClient;

        public ApplicationService(ILogger<ApplicationService> _looger, IDbConnection _dbConnection, IHttpClientFactory _httpClient)
        {
            logger = _looger;
            dbConnection = _dbConnection;           
            httpClient = _httpClient;
        }

        #region User Registration Service

        public async Task<UserRegistrationResponse> RegistrationUserAsync(UserRegistrationRequest request)
        {
            RegistrationUserWrapper wrapper = FillRegistrationUserWrapper(request);
            ValidateRequest(request);            
            try
            {
                using (var connection = dbConnection)
                {
                    connection.Open();
                    wrapper = await InsertApplicationToDB(connection, wrapper);
                }
                CreateRegistrationResponse(wrapper);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw ApplicationException.RegistrationFails;
            }
            return wrapper.response;
        }

        private RegistrationUserWrapper FillRegistrationUserWrapper(UserRegistrationRequest request)
        {
            RegistrationUserWrapper wrapper = new RegistrationUserWrapper();
            wrapper.request = request;
            wrapper.response = new UserRegistrationResponse();
            return wrapper;
        }

        private async Task<RegistrationUserWrapper> InsertApplicationToDB(IDbConnection connection, RegistrationUserWrapper wrapper)
        {
            RegisterUser _criteria = CreateInputCriteriaForRegistration(wrapper.request);
            wrapper.criteria = _criteria;
            DBProperties dbProps = new DBProperties(connection, null);
            using (IDbTransaction tran = dbProps.con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                dbProps = new DBProperties(dbProps.con, tran);
                try
                {
                    int result = await ApplicationQueries.RegisterUserInDB(dbProps, wrapper);
                    ValidateResultFromDB(result, wrapper.response);
                    dbProps.tran.Commit();
                    dbProps.con.Close();
                }
                catch (Exception exception) { HandleDBException(tran, exception); }
            }
            return wrapper;
        }

        private void ValidateRequest(UserRegistrationRequest request)
        {
            if (String.IsNullOrEmpty(request.FirstName) || String.IsNullOrEmpty(request.LastName) || String.IsNullOrEmpty(request.Email) || String.IsNullOrEmpty(request.Password)) throw new Exception("Please, fill in all the required fields.");
        }

        private RegisterUser CreateInputCriteriaForRegistration(UserRegistrationRequest request)
        {
            RegisterUser registeredUser = new RegisterUser();
            registeredUser.Id = Guid.NewGuid();
            registeredUser.FirstName = request.FirstName;
            registeredUser.LastName = request.LastName;
            registeredUser.Email = request.Email;
            registeredUser.TransactionDate = DateTime.Now;
            return registeredUser;
        }

        private void ValidateResultFromDB(int result, UserRegistrationResponse response)
        {
            if (result != 1)
            {
                response.IsCreated = false;
                throw ApplicationException.RegistrationFails;
            }
            else response.IsCreated = true;
        }

        private UserRegistrationResponse CreateRegistrationResponse(RegistrationUserWrapper wrapper)
        {
            wrapper.response.RegistrationDate = wrapper.criteria.TransactionDate.Date;
            return wrapper.response;
        }

        private void HandleDBException(IDbTransaction tran, Exception ex)
        {
            logger.LogError(ex.Message);
            tran.Rollback();
            throw TechnicalException.DbFails;
        }

        #endregion
    }
}
