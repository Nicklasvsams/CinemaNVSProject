using CinemaNVS.DAL.Database;
using CinemaNVS.DAL.Database.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaNVS.DAL.Repositories.Users
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> SelectAllCustomersAsync();
        Task<Customer> SelectCustomerByIdAsync(int id);
        Task<Customer> UpdateCustomerActivationByIdAsync(int id);
        Task<Customer> UpdateCustomerByIdAsync(Customer customer, int id);
        Task<Customer> InsertCustomer(Customer customer);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly CinemaDBContext _dBContext;

        public CustomerRepository(CinemaDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<Customer> UpdateCustomerActivationByIdAsync(int id)
        {
            Customer customerActivation = await _dBContext
                .Customers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customerActivation != null)
            {
                if (customerActivation.IsActive == "yes")
                {
                    customerActivation.IsActive = "no";
                }
                else
                {
                    customerActivation.IsActive = "yes";
                }

                await _dBContext.SaveChangesAsync();
            }

            return customerActivation;
        }

        public async Task<Customer> InsertCustomer(Customer customer)
        {
            await _dBContext.Customers.AddAsync(customer);
            await _dBContext.SaveChangesAsync();

            return customer;
        }

        public async Task<IEnumerable<Customer>> SelectAllCustomersAsync()
        {
            return await _dBContext
                .Customers
                .Include(x => x.Bookings)
                .ToListAsync();
        }

        public async Task<Customer> SelectCustomerByIdAsync(int id)
        {
            return await _dBContext
                .Customers
                .Include(x => x.Bookings)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer> UpdateCustomerByIdAsync(Customer customer, int id)
        {
            Customer customerToUpdate = await _dBContext
                .Customers
                .FirstOrDefaultAsync(x => x.Id == id);

            if (customerToUpdate != null)
            {
                customerToUpdate.FirstName = customer.FirstName;
                customerToUpdate.LastName = customer.LastName;
                customerToUpdate.Email = customer.Email;
                customerToUpdate.PhoneNo = customer.PhoneNo;

                await _dBContext.SaveChangesAsync();
            }

            return customerToUpdate;
        }
    }
}
