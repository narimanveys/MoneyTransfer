using System;
using System.Collections.Generic;
using System.Linq;
using InternalMoneyTransfer.Core.DataModel;

namespace InternalMoneyTransfer.DAL.Repository.UserRepository
{
    public class UserRepository : IRepository<User>
    {
        #region Constructor

        public UserRepository(IDbContext appContext)
        {
            _appContext = appContext;
        }

        #endregion

        #region Fields

        private readonly IDbContext _appContext;
        public static decimal DefaultPwAmount = 500;

        #endregion

        #region Methods

        public User Get(int id)
        {
            return _appContext.Users
                .FirstOrDefault(entity => entity.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _appContext.Users.ToList();
        }

        public void Insert(User entity)
        {
            using (var context = new ApplicationContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        _appContext.Users.Add(entity);
                        _appContext.SaveChanges();

                        var account = new UserAccount
                        {
                            AvailableAmount = DefaultPwAmount,
                            User = entity
                        };

                        _appContext.UsersAccounts.Add(account);

                        _appContext.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Save user exception: {ex.Message}");
                    }
                }
            }
        }

        #endregion
    }
}