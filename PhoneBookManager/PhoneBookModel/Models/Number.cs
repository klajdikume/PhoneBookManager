using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBookModel.Models
{
    //Entiteti relacion ku nje user ka disa numra me nje tip te caktuar Home, cellphone ose work
    public class Number
    {
        public Number()
        {
            User = new User();
            Type = new Type();
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }
        public int IdType { get; set; }
        public int IdUser { get; set; }
        public bool Deleted { get; set; }
        public string UserNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public Type Type { get; set; }
    }
}
