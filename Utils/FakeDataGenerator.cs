using Bogus;

namespace SeleniumTestProject.Utils
{
    public static class FakeDataGenerator
    {
        private static readonly Faker faker = new();

        public static string GetFakeEmail() => faker.Internet.Email();
        public static string GetFakePassword() => faker.Internet.Password();
        public static string GetFakeName() => faker.Name.FullName();
        public static string GetFakePhoneNumber() => faker.Phone.PhoneNumber();
    }
}