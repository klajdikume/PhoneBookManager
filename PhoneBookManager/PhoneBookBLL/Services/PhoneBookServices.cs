using PhoneBookBLL.Interfaces;
using PhoneBookDAL.Interfaces;
using PhoneBookDTOs.DTO;
using PhoneBookModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookBLL.Services
{
    public class PhoneBookServices : IPhoneBookServices
    {
        private readonly IPhoneBookRepository _phoneBookRepository;

        public PhoneBookServices(IPhoneBookRepository phoneBookRepository)
        {
            _phoneBookRepository = phoneBookRepository;
        }

        public PhoneBookServices()
        {
        }

        public PhoneBookForReturnDTO Post(PhoneBookToCreateDTO phoneBook)
        {
            //kthen objekt user me te dhenat e userit nga phone book dhe IEnumerable per numrat
            var user = PhoneBookToUser(phoneBook);
            //Ben shtimin e phonebook ne repository 
            var createdUser = _phoneBookRepository.Post(user);
            //kthen userin e krijuar me numrat e tij dhe emrin e tipit te numrit
            return UserToPhoneBookConverter(createdUser);
        }

        private static User PhoneBookToUser(PhoneBookToCreateDTO phoneBook)
        {
            return new User
            {
                Id = phoneBook.Id,
                Name = phoneBook.Firstname,
                LastName = phoneBook.Lastname,
                Numbers = phoneBook.NumberInfo.Select(x => new Number
                {
                    Id = x.Id,
                    UserNumber = x.Number,
                    IdType = System.Int32.Parse(x.TypeId)
                }).ToList(),
                Deleted = phoneBook.Deleted
            };
        }

        private static PhoneBookForReturnDTO UserToPhoneBookConverter(User user)
        {
            return new PhoneBookForReturnDTO
            {
                Id = user.Id,
                Firstname = user.Name,
                Lastname = user.LastName,
                NumberInfo = user.Numbers.Select(x => new NumberToReturnDTO
                {
                    Id = x.Id,
                    Number = x?.UserNumber ?? string.Empty,
                    Type = x?.Type?.TypeNumber ?? string.Empty
                }).ToList()
            };
        }

        public IEnumerable<PhoneBookForReturnDTO> GetAllOrderedBy(bool orderByFirstName, bool asc)
        {
            var user = _phoneBookRepository.GetAll();

            IEnumerable<PhoneBookForReturnDTO> result = null;

            var users = (from u in user
                         select new PhoneBookForReturnDTO
                         {
                             Id = u.Id,
                             Firstname = u.Name,
                             Lastname = u.LastName,
                             NumberInfo = u.Numbers.Select(n => new NumberToReturnDTO
                             {
                                 Id = n.Id,
                                 Number = n?.UserNumber ?? string.Empty,
                                 Type = n?.Type?.TypeNumber ?? string.Empty
                             }).ToList()
                         }
                         ).ToList();

            if (!asc)
            {
                if (!orderByFirstName)
                {
                    result = users.OrderByDescending(x => x.Lastname);
                }
                else
                {
                    result = users.OrderByDescending(x => x.Firstname);
                }
            }
            else
            {
                if (!orderByFirstName)
                {
                    result = users.OrderBy(x => x.Lastname);
                }
                else
                {
                    result = users.OrderBy(x => x.Firstname);
                }
            }

            return result;
        }

        public bool Delete(int id)
        {
            bool deleted = _phoneBookRepository.Delete(id);

            return deleted;
        }

        public PhoneBookForReturnDTO Put(PhoneBookToCreateDTO phoneBook)
        {
            var user = PhoneBookToUser(phoneBook);

            var updatedUser = _phoneBookRepository.Put(user);

            return UserToPhoneBookConverter(updatedUser);
        }

        public PhoneBookForReturnDTO Get(int id)
        {
            var user =  _phoneBookRepository.Get(id);

            return UserToPhoneBookConverter(user);
        }

        public PhoneBookForReturnDTO AddNumber(PhoneBookToCreateDTO phoneBook)
        {
            var user = PhoneBookToUser(phoneBook);

            var createdUser = _phoneBookRepository.AddNumber(user);

            return UserToPhoneBookConverter(createdUser);
        }
    }
    
}
