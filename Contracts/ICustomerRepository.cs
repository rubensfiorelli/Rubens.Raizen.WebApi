using Test.Rubens.Raizen.WebApi.Entities;

namespace Test.Rubens.Raizen.WebApi.Contracts
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomerId(Guid customerId);
        Task AddAsync(Customer model);
        Task<Customer> Update(Guid customerId, Customer customer);
        Task<Customer> Delete(Guid customerId);

    }
}
