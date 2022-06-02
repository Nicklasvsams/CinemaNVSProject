﻿using CinemaNVS.DAL.Database.Entities.Users;
using CinemaNVS.DAL.Repositories.Users;
using CinemasNVS.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemasNVS.BLL.Services.UserServices
{
    public interface ILoginService
    {
        Task<LoginResponse> AuthorizeLoginByUsernameAsync(string name, string password);
        Task<LoginResponse> CreateLoginAsync(LoginRequest login);
        Task<LoginResponse> UpdateLoginByUsernameAsync(LoginRequest login, string username);
    }

    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<LoginResponse> AuthorizeLoginByUsernameAsync(string username, string password)
        {
            Login login = await _loginRepository.SelectLoginByNameAsync(username);
            LoginResponse logRes = new LoginResponse();

            if (login != null)
            {
                if (login.Username == username && login.Password == password)
                {
                    logRes.IsAuthorized = true;
                }
                else
                {
                    logRes.IsAuthorized = false;
                }

                return logRes;
            }

            return null;
        }

        public async Task<LoginResponse> CreateLoginAsync(LoginRequest login)
        {
            return MapEntityToResponse(await _loginRepository.InsertLoginAsync(MapRequestToEntity(login)));
        }

        public async Task<LoginResponse> UpdateLoginByUsernameAsync(LoginRequest login, string username)
        {
            return MapEntityToResponse(await _loginRepository.UpdateLoginByUsernameAsync(MapRequestToEntity(login), username));
        }

        private LoginResponse MapEntityToResponse(Login login)
        {
            LoginResponse logRes = null;

            if (login != null)
            {
                logRes = new LoginResponse()
                {
                    Id = login.Id,
                    Username = login.Username,
                    CustomerId = login.CustomerId,
                    IsAuthorized = false
                };

                if (login.IsAdmin == "yes") logRes.IsAdmin = true;
                else logRes.IsAdmin = false;
            }

            return logRes;
        }

        private Login MapRequestToEntity(LoginRequest logReq)
        {
            Login log = new Login()
            {
                Username = logReq.Username,
                Password = logReq.Password
            };

            if (logReq.CustomerId != null) log.CustomerId = logReq.CustomerId;

            if (logReq.IsAdmin) log.IsAdmin = "yes";
            else log.IsAdmin = "no";

            return log;
        }
    }
}
