using PhoneBookModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookDAL.Interfaces
{
    public interface IPhoneBookRepository
    {
        User Post(User phoneBook);
        List<User> GetAll();
        bool Delete(int id);
        User Put(User user);
        User Get(int id);
        User AddNumber(User user);
    }
}
