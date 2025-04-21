using FluentValidation;
using Government.Contracts.Services;

public class ServiceFieldsValidator : AbstractValidator<ServiceFields>
{
    public ServiceFieldsValidator()
    {
        RuleFor(x => x.FieldName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.HtmlType)
            .NotEmpty()
            .Must(type => new[] { "text", "number", "date", "email", "select", "checkbox" }.Contains(type, StringComparer.OrdinalIgnoreCase))
            .WithMessage("Invalid HTML type.");
    }
}
