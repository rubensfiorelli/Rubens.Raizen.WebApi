using Microsoft.EntityFrameworkCore;
using Test.Rubens.Raizen.WebApi.Contracts;
using Test.Rubens.Raizen.WebApi.Database;
using Test.Rubens.Raizen.WebApi.Entities;

namespace Test.Rubens.Raizen.WebApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context) => _context = context;
       

        public async Task AddAsync(Customer model)
        {
            try
            {
                await _context.AddAsync(model);

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateException)
            {

                throw new Exception("Error inserting customer");
            }
        }

        public async Task<Customer> Delete(Guid customerId)
        {
            try
            {
                var existing = await _context.Customers
                     .AsTracking()
                     .SingleOrDefaultAsync(x => x.Id.Equals(customerId));

                if (existing is null)
                    return null;

                existing.Delete();
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return existing;
            }
            catch (DbUpdateException)
            {

                throw new Exception("Error removing client");
            }
        }

        public async Task<Customer> GetCustomerId(Guid customerId)
        {
            try
            {
                var existing = await _context.Customers
                .AsTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(customerId));

                if (existing is null)
                    return null;

                return existing;
            }
            catch (Exception)
            {

                throw new Exception("Unknown error accessing database");
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            try
            {
                var customersList = await _context.Customers
               .Where(x => !x.IsDeleted)
               .ToListAsync();

                return customersList;
            }
            catch (Exception)
            {
                throw new Exception("Unknown error accessing database");
            }
        }

        public async Task<Customer> Update(Guid customerId, Customer customer)
        {
            try
            {
                var existing = await _context.Customers
                    .AsTracking()
                    .SingleOrDefaultAsync(x => x.Id.Equals(customer.Id));

                if (existing is null)
                    return null;

                customer.UpdatedAt = DateTimeOffset.UtcNow;
                customer.CreatedAt = existing.CreatedAt;

                existing.SetupCustomer(customer.FirstName, customer.LastName, customer.Email);

                _context.Update(existing);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return existing;
            }
            catch (DbUpdateException)
            {
                throw new Exception("Error finding customer");
            }
        }
    }
}
