using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Application.UserVerif
{
    public class CheckLoginInputDto
    {
        [Required]
        public string token { get; set; }
        [Required]
        public string okl_token { get; set; }
        [Required]
        public string cookies { get; set; }
    }
}
