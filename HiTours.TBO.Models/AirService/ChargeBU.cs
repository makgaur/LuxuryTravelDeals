using System.ComponentModel.DataAnnotations;

namespace HiTours.TBO.Models
{
    public class ChargeBU
    {
        [Display(Name = "Key")]
        public string Key { get; set; }

        [Display(Name = "Value")]
        public long Value { get; set; }
    }
}