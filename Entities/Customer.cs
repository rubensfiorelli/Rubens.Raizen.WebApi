namespace Test.Rubens.Raizen.WebApi.Entities
{
    public sealed class Customer : BaseEntity
    {
        public Customer(string firstName,
                       string lastName,
                       string email,
                       string passwordHash,
                       DateTime birthDate,
                       string zipCode) : base()
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            BirthDate = birthDate;
            ZipCode = zipCode;

            IsDeleted = false;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string ZipCode { get; private set; }
        public bool IsDeleted { get; private set; }


        public void SetupCustomer(string firstName,
                                  string lastName,
                                  string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;

        }
        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
