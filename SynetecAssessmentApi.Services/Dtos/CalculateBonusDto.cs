using SynetecAssessmentApi.Services.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace SynetecAssessmentApi.Services.Dtos
{
    public class CalculateBonusDto
    {
        [Required]
        public int TotalBonusPoolAmount { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid employee id greater than 0")]
        [IsValidEmployee]
        public int SelectedEmployeeId { get; set; }

    }
}
