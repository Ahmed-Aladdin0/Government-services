using Government.Contracts.Services;

namespace Government.Contracts.FilesAndFileds
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
