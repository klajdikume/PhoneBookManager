using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookDTOs.DTO
{
    //modeli ne kerkesat e kthimit te phonebook qe ka emer, mbiemer te userit dhe numrat e tij
    public class PhoneBookForReturnDTO
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public List<NumberToReturnDTO> NumberInfo { get; set; }
    }
}
