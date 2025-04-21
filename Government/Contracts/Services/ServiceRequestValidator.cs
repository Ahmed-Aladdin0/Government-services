using FluentValidation;

namespace Government.Contracts.Services
{
    public class ServiceRequestValidator:AbstractValidator<ServiceRequest>
    {

        public ServiceRequestValidator()
        {
            RuleFor(x => x.ServiceName)
             .NotEmpty()
             .MaximumLength(250);


            RuleFor(x => x.ServiceDescription)
                .NotEmpty()
                .MaximumLength(1500);
                 

            RuleFor(x => x.Fee)
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                    .WithMessage("Fee must be a positive number.");

            RuleFor(x => x.ProcessingTime)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(x => x.ContactInfo)
                .NotEmpty()
                .MaximumLength(1500);

            RuleForEach(x => x.ServiceFields)
           .SetValidator(new ServiceFieldsValidator());
        }

    }
}

