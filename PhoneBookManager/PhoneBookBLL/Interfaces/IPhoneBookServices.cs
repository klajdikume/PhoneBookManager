using PhoneBookDTOs.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookBLL.Interfaces
{
    public interface IPhoneBookServices
    {
        PhoneBookForReturnDTO Post(PhoneBookToCreateDTO phoneBook);
        bool Delete(int id);
        IEnumerable<PhoneBookForReturnDTO> GetAllOrderedBy(bool orderByFirstName, bool asc);
        PhoneBookForReturnDTO Get(int id);
        PhoneBookForReturnDTO AddNumber(PhoneBookToCreateDTO phoneBook);
        PhoneBookForReturnDTO Put(PhoneBookToCreateDTO phoneBook);
    }
}
