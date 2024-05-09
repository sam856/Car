using Cars.NewFolder;

namespace Cars.Configuration
{
    public class Accounts
    {
        private readonly    AppDbContext appDbContext;
        public Accounts(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public bool checkEmail (string email)
        {
            bool Found = appDbContext.Users.Any(x => x.Email == email);
            return Found;

        }
    }
}
