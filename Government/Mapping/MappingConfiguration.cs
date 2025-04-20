using Government.Contracts.Request;
using Government.Contracts.Services;
using Mapster;
using SurvayBasket.Contracts.Authentication;
using SurvayBasket.Contracts.User.cs;

namespace Government.Mapping
{
    public class MappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
      

            config.NewConfig<Service, ServiceResponse>()
             .Map(dest => dest.category, src => src.category);

            config.NewConfig<RegisterRequest, AppUser>()
          .Map(dest => dest.UserName, src => src.Email);


            config.NewConfig<UserRequest, AppUser>()
              .Map(dest => dest.UserName, src => src.Email)
              .Map(dest => dest.EmailConfirmed, src => true);

            config.NewConfig<UserUpdate, AppUser>()
             .Map(dest => dest.UserName, src => src.Email)
             .Map(dest => dest.NormalizedUserName, src => src.Email.ToUpper());


        }
    }
}
