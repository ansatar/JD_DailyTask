using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JD_DailyTask.Models
{
    public class DoActionIntput
    {
        [Required]
        public string pt_pin { get; set; }
        [Required]
        public List<string> ActionList { get; set; }
    }
}
