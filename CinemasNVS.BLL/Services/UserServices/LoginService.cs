using CinemaNVS.DAL.Database.Entities.Transactions;
using CinemaNVS.DAL.Database.Entities.Users;
using CinemaNVS.DAL.Repositories.Users;
using CinemasNVS.BLL.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasNVS.BLL.Services.UserServices
{
    public interface ILoginService
    {
        Task<IEnumerable<LoginResponse>> GetAllLoginsAsync();
        Task<LoginResponse> GetLoginByUsernameAsync(string username);
        Task<LoginResponse> AuthorizeLoginAsync(string name, string password);
        Task<LoginResponse> CreateLoginAsync(LoginRequest login);
        Task<LoginResponse> UpdateLoginByUsernameAsync(LoginRequest login, string username);
        Task<LoginResponse> DeleteLoginByUsernameAsync(string username);
    }

    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<LoginResponse> AuthorizeLoginAsync(string username, string password)
        {
            Login login = await _loginRepository.SelectLoginByNameAsync(username);
            LoginResponse logRes = new LoginResponse();

            if (login != null)
            {
                logRes = MapEntityToResponse(login);

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

        public async Task<LoginResponse> DeleteLoginByUsernameAsync(string username)
        {
            return MapEntityToResponse(await _loginRepository.DeleteLoginByUsernameAsync(username));
        }

        public async Task<IEnumerable<LoginResponse>> GetAllLoginsAsync()
        {
            IEnumerable<Login> logins = await _loginRepository.SelectAllLoginsAsync();

            return logins.Select(x => MapEntityToResponse(x)).ToList();
        }

        public async Task<LoginResponse> GetLoginByUsernameAsync(string username)
        {
            return MapEntityToResponse(await _loginRepository.SelectLoginByNameAsync(username));
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
                    IsAuthorized = false,
                    CustomerId = login.CustomerId
                };

                if (login.IsAdmin == "yes") logRes.IsAdmin = true;
                else logRes.IsAdmin = false;

                if (login.Customer != null)
                {
                    logRes.CustomerResponse = new LoginResponseCustomer()
                    {
                        Id = login.Customer.Id,
                        FirstName = login.Customer.FirstName,
                        LastName = login.Customer.LastName,
                        Email = login.Customer.Email,
                        PhoneNo = login.Customer.PhoneNo
                    };

                    if(login.Customer.Bookings != null)
                    {
                        List<CustomerResponseBooking> cusResBooList = new List<CustomerResponseBooking>();

                        foreach (Booking boo in login.Customer.Bookings)
                        {
                            CustomerResponseBooking cusResBoo = new CustomerResponseBooking();

                            cusResBoo.Id = boo.Id;
                            cusResBoo.BookingDate = boo.BookingDate;
                            cusResBoo.CustomerId = boo.CustomerId;
                            cusResBoo.ShowingId = boo.ShowingId;

                            if(boo.BookingSeating != null)
                            {
                                List<BookingResponseSeating> BooResSeaList = new List<BookingResponseSeating>();

                                foreach(BookingSeating booSea in boo.BookingSeating)
                                {
                                    BookingResponseSeating booResSea = new BookingResponseSeating();

                                    booResSea.Id = booSea.Seating.Id;
                                    booResSea.Seat = booSea.Seating.Seat;

                                    BooResSeaList.Add(booResSea);
                                }

                                cusResBoo.SeatingResponses = BooResSeaList;
                            }

                            if(boo.Showing != null)
                            {
                                BookingResponseShowing booResSho = new BookingResponseShowing();

                                booResSho.Id = boo.Showing.Id;
                                booResSho.Price = boo.Showing.Price;
                                booResSho.TimeOfShowing = boo.Showing.TimeOfShowing;
                                booResSho.MovieId = boo.Showing.MovieId;

                                cusResBoo.ShowingResponse = booResSho;

                                if (boo.Showing.Movie != null)
                                {
                                    ShowingResponseMovie shoResMov = new ShowingResponseMovie();

                                    shoResMov.Id = boo.Showing.Movie.Id;
                                    shoResMov.Title = boo.Showing.Movie.Title;
                                    shoResMov.ReleaseDate = boo.Showing.Movie.ReleaseDate;
                                    shoResMov.RuntimeMinutes = boo.Showing.Movie.RuntimeMinutes;
                                    shoResMov.TrailerLink = boo.Showing.Movie.TrailerLink;
                                    shoResMov.ImdbLink = boo.Showing.Movie.ImdbLink;
                                    shoResMov.DirectorId = boo.Showing.Movie.DirectorId;

                                    if (boo.Showing.Movie.IsRunning == 1) shoResMov.IsRunning = true;
                                    else shoResMov.IsRunning = false;

                                    cusResBoo.ShowingResponse.MovieResponse = shoResMov;
                                }
                            }

                            cusResBooList.Add(cusResBoo);
                        };

                        logRes.CustomerResponse.bookingResponses = cusResBooList;
                    }

                    if (login.Customer.IsActive == "yes") logRes.CustomerResponse.IsActive = true;
                    else logRes.CustomerResponse.IsActive = false;
                }
            }

            return logRes;
        }

        private Login MapRequestToEntity(LoginRequest logReq)
        {
            Login log = new Login()
            {
                Username = logReq.Username,
                Password = logReq.Password,
                CustomerId = logReq.CustomerId
            };

            if (logReq.IsAdmin) log.IsAdmin = "yes";
            else log.IsAdmin = "no";

            return log;
        }
    }
}
