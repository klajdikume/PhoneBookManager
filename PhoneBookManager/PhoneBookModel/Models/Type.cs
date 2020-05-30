using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookModel.Models
{
    //Tipi i numrit 
    public class Type
    {
        public Type()
        {
            Numbers = new List<Number>();
        }
        public int Id { get; set; }
        public string TypeNumber { get; set; }
        public bool Deleted {get; set; }
        public IEnumerable<Number> Numbers { get; set; }
    }
}
