namespace Government.Contracts
{
    public class NewImageValidation : AbstractValidator<NewImage>
    {
        private readonly string[] _allowedExtensions = [".jpg", ".jpeg", ".png"];
        private const long _maxFileSizeInBytes = 5 * 1024 * 1024; // 5MB

        public NewImageValidation()
        {
        
            RuleFor(x => x.newImage)
             .NotNull().WithMessage("Image is required.")
             .DependentRules(() =>
             {
                 RuleFor(x => x.newImage)
                     .Must(file => file.Length > 0)
                     .WithMessage("Uploaded image is empty.")
                     .Must(file => _allowedExtensions.Contains(
                         System.IO.Path.GetExtension(file.FileName).ToLower()))
                     .WithMessage("Only image files (.jpg, .jpeg, .png, .gif) are allowed.")
                     .Must(file => file.Length <= _maxFileSizeInBytes)
                     .WithMessage("Image size must not exceed 5 MB.");
     });
        }
    }
}
