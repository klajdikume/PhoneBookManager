using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneBookDTOs.DTO
{
    //Modeli i te dhenave per numrin i cili do te perdoret ne krijimin e objektit numer
    public class NumberToCreateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string TypeId { get; set; }
    }
}
