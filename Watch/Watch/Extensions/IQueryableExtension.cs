using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watch.Models.Users;
using Watch.Models.Watches;

namespace Watch.Extensions
{
    public static class IQueryableExtension
    {
        #region GLOBAL REQUESTS

        public static bool IsEmpty<TModel>(this IQueryable<TModel> queryable) where TModel : class
        {
            return (queryable.Count() == 0);
        }

        public static bool IsNotEmpty<TModel>(this IQueryable<TModel> queryable)
        {
            return (queryable.Count() > 0);
        }

        #endregion


        #region USERS REQUESTS        

        public static IUser FindByLogin(this IQueryable<IUser> users, string login)
        {
            var models = (from user in users where (user.Credentials.Login == login) select user);
            var model  = models?.Include(u => u.Watches)?.FirstOrDefault();

            return model;                
        }

        public static IUser FindById(this IQueryable<IUser> users, Guid id)
        {
            var models = (from user in users where user.Id == id select user);
            var model  = models?.Include(u => u.Watches)?.FirstOrDefault();

            return model;
        }

        public static List<IWatch> GetUserWatches(this IQueryable<IUser> users, Guid id)
        {
            var user = users.FindById(id);

            var watches = (user?.Watches is null) ? new List<IWatch>() : user.Watches.ToList<IWatch>();

            return watches;
        }

        public static bool ContainUserWithLogin(this IQueryable<IUser> users, string login)
        {
            return !(users.FindByLogin(login) is null);
        }


        #endregion


        #region WATCHES REQUESTS        

        public static IWatch FindById(this IQueryable<IWatch> watches, int id)
        {
            var models = (from watch in watches where watch.Id == id select watch);
            var model  = models.FirstOrDefault();

            return model;
        }

        #endregion
    }
}
