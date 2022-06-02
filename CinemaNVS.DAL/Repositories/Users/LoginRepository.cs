using CinemaNVS.DAL.Database;
using CinemaNVS.DAL.Database.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CinemaNVS.DAL.Repositories.Users
{
    public interface ILoginRepository
    {
        Task<Login> GetLoginByNameAsync(string name);
        Task<Login> InsertLoginAsync(Login login);
        Task<Login> UpdateLoginByUsernameAsync(Login login, string username);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly CinemaDBContext _dBContext;

        public LoginRepository(CinemaDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Login> GetLoginByNameAsync(string name)
        {
            return await _dBContext.Logins.FirstOrDefaultAsync(x => x.Username == name);
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

                await _dBContext.SaveChangesAsync();
            }

            return login;
        }
    }
}
