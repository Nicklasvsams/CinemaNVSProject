using CinemaNVS.DAL.Database;
using CinemaNVS.DAL.Database.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaNVS.DAL.Repositories.Users
{
    public interface ILoginRepository
    {
        Task<IEnumerable<Login>> SelectAllLoginsAsync();
        Task<Login> SelectLoginByNameAsync(string name);
        Task<Login> InsertLoginAsync(Login login);
        Task<Login> UpdateLoginByUsernameAsync(Login login, string username);
        Task<Login> DeleteLoginByUsernameAsync(string username);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly CinemaDBContext _dBContext;

        public LoginRepository(CinemaDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Login> SelectLoginByNameAsync(string name)
        {
            return await _dBContext
                .Logins
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Username == name);
        }

        public async Task<Login> InsertLoginAsync(Login login)
        {
            await _dBContext.Logins.AddAsync(login);
            await _dBContext.SaveChangesAsync();

            return login;
        }

        public async Task<Login> UpdateLoginByUsernameAsync(Login login, string username)
        {
            Login loginToUpdate = await _dBContext.Logins.FirstOrDefaultAsync(x => x.Username == username);

            if (loginToUpdate != null)
            {
                loginToUpdate.Username = login.Username;
                loginToUpdate.Password = login.Password;
                loginToUpdate.IsAdmin = login.IsAdmin;
                loginToUpdate.CustomerId = login.CustomerId;

                await _dBContext.SaveChangesAsync();
            }

            return login;
        }

        public async Task<IEnumerable<Login>> SelectAllLoginsAsync()
        {
            return await _dBContext
                .Logins
                .Include(x => x.Customer)
                .ToListAsync();
        }

        public async Task<Login> DeleteLoginByUsernameAsync(string username)
        {
            Login loginToDelete = await _dBContext.Logins.FirstOrDefaultAsync(x => x.Username == username);

            if (loginToDelete != null)
            {
                _dBContext.Logins.Remove(loginToDelete);

                await _dBContext.SaveChangesAsync();
            }

            return loginToDelete;
        }
    }
}
