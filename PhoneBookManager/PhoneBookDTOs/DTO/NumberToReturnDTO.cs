using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookDTOs.DTO
{
    //modeli qe do te perdoret per te kthyer numrat e nje useri
    public class NumberToReturnDTO
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Type { get; set; }
    }
}
