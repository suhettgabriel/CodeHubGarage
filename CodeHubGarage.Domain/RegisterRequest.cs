using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodeHubGarage.Domain
{
    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [DataType(DataType.Date)] 
        public DateTime DateOfBirth { get; set; }

        [DisplayFormat(DataFormatString = "({0:##})#####-####", ApplyFormatInEditMode = true)]
        [RegularExpression(@"^\(\d{2}\)\d{5}-\d{4}$", ErrorMessage = "Formato de telefone inválido")]
        public string PhoneNumber { get; set; }
        public string CarroPlaca { get; set; }
        public bool IsMensalista { get; set; }


    }
}

