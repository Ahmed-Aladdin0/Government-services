using Government.Contracts.Services;

namespace Government.Test.Dtos
{
    public class FieldsValidator : AbstractValidator<FieldsTest>
    {
        public FieldsValidator()
        {
            RuleForEach(x => x.ServiceFields)
                .SetValidator(new ServiceFieldsValidator());
        }
    }
}
