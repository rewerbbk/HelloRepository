using System.ComponentModel.DataAnnotations;

namespace EmptyUILayer.Models
{
    public class CustomModel
    {
        [Display(Name = "Street")]
        public string addressLine1 { get; set; }

        [Display(Name = "number")]
        public int numberField { get; set; }
    }
}