using System.ComponentModel.DataAnnotations;

namespace SynetecAssessmentApi.Services.Validations
{
    /// <summary>
    /// Validation attirbute class for checking employee exists in DB.
    /// </summary>
    public class IsValidEmployeeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            var bonusPoolService = (IBonusPoolService)validationContext.GetService(typeof(IBonusPoolService));

            if (bonusPoolService.CheckEmployeeExists((int)value).Result)
                return ValidationResult.Success;

            return new ValidationResult("Employee doesn't exists");
        }
    }
}
