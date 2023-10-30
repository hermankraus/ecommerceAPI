using ecommerceAPI.DBContexts;
using ecommerceAPI.Entities;

namespace ecommerceAPI.Services
{
    public class UserService
    {
        private readonly EcommerceContext _ecommerceContext;

        public UserService(EcommerceContext ecommerceContext)
        {
            _ecommerceContext = ecommerceContext;
        }

        public Tuple<bool,User?> ValidateUser(string email, string password)
        {
            User? userForLogin = _ecommerceContext.Users.SingleOrDefault(u => u.Email == email);
            if (userForLogin != null)
            {
                if(userForLogin.Password == password)
                {
                    return new Tuple<bool, User?>(true, userForLogin);
                }
                return new Tuple<bool, User?>(false, userForLogin);
            }
            return new Tuple<bool, User?>(false, null);
        }
    }
}
