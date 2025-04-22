using Government.Contracts.Request.Submiting;

namespace Government.Test.Dtos
{
    public class UserFieldsValidator : AbstractValidator<UserFields>
    {
        public UserFieldsValidator()
        {
       
            RuleForEach(x=>x.ServiceData).SetValidator(new ServiceDataDtoValidator ());
        }
    }
}
