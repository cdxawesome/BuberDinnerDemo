using ErrorOr;

namespace BuberDinner.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials => Error.Validation(
                               code: "Auth.InvalidCred",
                                              description: "用户名或密码不正确.");
        }
    }
}
