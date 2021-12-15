using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Tutorias.Models
{
    public class TutorManager : UserManager<Tutor>
    {
        public TutorManager(IUserStore<Tutor> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Tutor> passwordHasher, IEnumerable<IUserValidator<Tutor>> userValidators, IEnumerable<IPasswordValidator<Tutor>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<Tutor>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}
