using Hangfire.Sample.Repository.EF;
using Hangfire.Sample.Repository.Identity.MessageServices;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Hangfire.Sample.Repository.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser, Guid>
    {
        private readonly Random _randomGenerator = new Random();

        public ApplicationUserManager(ApplicationDbContext dbContext)
            : base(new AppUserStore(dbContext))
        {
            var provider = new MachineKeyProtectionProvider();
            var userTokenProvider = new DataProtectorTokenProvider<ApplicationUser, Guid>(provider.Create("ResetPassword"));

            SetUpManager(this, userTokenProvider);
        }
        private static void SetUpManager(ApplicationUserManager manager, DataProtectorTokenProvider<ApplicationUser, Guid> provider)
        {
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser, Guid>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireDigit = true,
                RequireUppercase = true
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromDays(99999);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser,Guid>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser, Guid>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            //manager.PasswordHasher = new SqlPasswordHasher();

            //Set the expiry on the code that is emailed to users when they reset their passwords.
            var now = DateTime.Now;
            var expiresOn = now.AddHours(24); //The default is 24 hours, you can change it here.
            var ticks = expiresOn.Subtract(now).Ticks;
            provider.TokenLifespan = new TimeSpan(ticks);
            manager.UserTokenProvider = provider;
        }
        public async Task<string> GenerateRandomPasswordAsync()
        {
            var passwordTask = Task<string>.Factory.StartNew(() =>
            {
                var randomLetter = (char)_randomGenerator.Next(65, 90); //A-Z capitilised
                var randomInt = _randomGenerator.Next(0, 9);

                var password = Guid.NewGuid()
                    .ToString()
                    .Replace("-", string.Empty)
                    .ToLower(CultureInfo.InvariantCulture);

                return randomLetter + password.Substring(0, 6) + randomInt;
            }).ConfigureAwait(false);

            return await passwordTask;
        }
    }
}