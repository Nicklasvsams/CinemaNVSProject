using CinemaNVS.DAL.Database.Entities.Transactions;
using CinemaNVS.DAL.Database.Entities.Users;
using CinemaNVS.DAL.Repositories.Users;
using CinemasNVS.BLL.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemasNVS.BLL.Services.UserServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync();
        Task<CustomerResponse> GetCustomerByIdAsync(int id);
        Task<CustomerResponse> UpdateCustomerActivationByIdAsync(int id);
        Task<CustomerResponse> UpdateCustomerByIdAsync(CustomerRequest customer, int id);
        Task<CustomerResponse> CreateCustomer(CustomerRequest customer);
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerResponse> CreateCustomer(CustomerRequest customer)
        {
            return MapEntityToResponse(await _customerRepository.InsertCustomer(MapRequestToEntity(customer)));
        }

        public async Task<IEnumerable<CustomerResponse>> GetAllCustomersAsync()
        {
            IEnumerable<Customer> customers = await _customerRepository.SelectAllCustomersAsync();

            return customers.Select(x => MapEntityToResponse(x)).ToList();
        }

        public async Task<CustomerResponse> GetCustomerByIdAsync(int id)
        {
            return MapEntityToResponse(await _customerRepository.SelectCustomerByIdAsync(id));
        }

        public async Task<CustomerResponse> UpdateCustomerActivationByIdAsync(int id)
        {
            Customer customerActivation = await _customerRepository.UpdateCustomerActivationByIdAsync(id);

            if (customerActivation != null)
            {
                return MapEntityToResponse(customerActivation);
            }

            return null;
        }

        public async Task<CustomerResponse> UpdateCustomerByIdAsync(CustomerRequest customer, int id)
        {
            return MapEntityToResponse(await _customerRepository.UpdateCustomerByIdAsync(MapRequestToEntity(customer), id));
        }

        private CustomerResponse MapEntityToResponse(Customer customer)
        {
            CustomerResponse cusRes = null;

            if (customer != null)
            {
                cusRes = new CustomerResponse()
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    PhoneNo = customer.PhoneNo,
                    LoginId = customer.LoginId
                };

                if (customer.Bookings != null)
                {
                    List<CustomerResponseBooking> booRes = new List<CustomerResponseBooking>();

                    foreach (Booking booking in customer.Bookings)
                    {
                        booRes.Add(new CustomerResponseBooking()
                        {
                            Id = booking.Id,
                            BookingDate = booking.BookingDate,
                            CustomerId = booking.CustomerId,
                            ShowingId = booking.ShowingId
                        });
                    }

                    cusRes.BookingResponses = booRes;
                }

                if (customer.IsActive == "yes") cusRes.IsActive = true;
                else cusRes.IsActive = false;

                if (customer.Login != null)
                {
                    cusRes.LoginResponse = new CustomerResponseLogin()
                    {
                        Id = customer.Login.Id,
                        Username = customer.Login.Username
                    };
                }
            }

            return cusRes;
        }

        private Customer MapRequestToEntity(CustomerRequest cusReq)
        {
            Customer cus = new Customer()
            {
                FirstName = cusReq.FirstName,
                LastName = cusReq.LastName,
                Email = cusReq.Email,
                PhoneNo = cusReq.PhoneNo
            };

            if (cusReq.IsActive) cus.IsActive = "yes";
            else cus.IsActive = "no";

            return cus;
        }
    }
}
