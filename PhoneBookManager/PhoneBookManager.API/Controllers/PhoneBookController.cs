using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBookBLL.Interfaces;
using PhoneBookDTOs;
using PhoneBookDTOs.DTO;


namespace PhoneBookManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPhoneBookServices _phoneBookService;
        public PhoneBookController(IPhoneBookServices phoneBookService)
        {
            _phoneBookService = phoneBookService;
        }

        [HttpGet]
        public List<PhoneBookForReturnDTO> Get()
        {
            bool orderByFirstName = true;
            bool asc = true;

            var result =  _phoneBookService.GetAllOrderedBy(orderByFirstName, asc).ToList();

            return result;
        }

        [HttpPost]
        public PhoneBookForReturnDTO Post([FromBody] PhoneBookToCreateDTO phoneBook)
        {
            var result = _phoneBookService.Post(phoneBook);

            return result;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var result = _phoneBookService.Delete(id);

            return result;
        }

        [HttpPut("{id}")]
        public ActionResult<PhoneBookForReturnDTO> Put(int id, [FromBody] PhoneBookToCreateDTO phoneBook)
        {
            if (id != phoneBook.Id)
            {
                return new BadRequestObjectResult("Check phone book id");
            }

            var result = _phoneBookService.Put(phoneBook);

            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<PhoneBookForReturnDTO> Get(int id)
        {
            var result = _phoneBookService.Get(id);

            return result;
        }

    }
}
