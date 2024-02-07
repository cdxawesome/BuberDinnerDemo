using BuberDinner.Application.Authentication.Command.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Contracts.Authentication;
using Mapster;

namespace BuberDinner.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginQuery>();
            // 因为AuthenticationResult和AuthenticationResponse的属性不一样，所以需要手动映射
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            // AuthenticationResult中的User可以直接映射到AuthenticationResponse中的各项属性。
            .Map(dest => dest, src => src.User);

        }
    }
}