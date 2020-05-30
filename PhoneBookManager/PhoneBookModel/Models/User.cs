using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookModel.Models
{
    //Entiteti i userit i cili do te kete disa numra 
    public class User
    {
        public User()
        {
            Numbers = new List<Number>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool Deleted { get; set; }
        public IEnumerable<Number> Numbers { get; set; }
    }
}
