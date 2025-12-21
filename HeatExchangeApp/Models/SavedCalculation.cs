using System;
using System.ComponentModel.DataAnnotations;

namespace HeatExchangeApp.Models
{
    public class SavedCalculation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public string ParametersJson { get; set; } = string.Empty;

        [Required]
        public string ResultsJson { get; set; } = string.Empty;
    }
}