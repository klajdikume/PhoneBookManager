using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PhoneBookDTOs.DTO
{
    //e krijim, modifikim ose delete perdoret ky model per user dhe numrat
    public class PhoneBookToCreateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public bool Deleted { get; set; }

        public List<NumberToCreateDTO> NumberInfo { get; set; }
    }
}
