using Government.Contracts.Request.Submiting;

namespace Government.Contracts.FilesAndFileds
{
    public class UserFieldsValidator : AbstractValidator<UserFields>
    {
        public UserFieldsValidator()
        {

            RuleForEach(x => x.ServiceData).SetValidator(new ServiceDataDtoValidator());
        }
    }
}
