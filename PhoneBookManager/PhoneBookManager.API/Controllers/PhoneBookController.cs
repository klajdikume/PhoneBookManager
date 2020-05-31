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

        //[HttpGet]
        //public List<PhoneBookForReturnDTO> Get()
        //{
        //    bool orderByFirstName = true;
        //    bool asc = true;

        //    var result =  _phoneBookService.GetAllOrderedBy(orderByFirstName, asc).ToList();

        //    //return Ok();
        //    return result;
        //}

        /// <summary>
        /// Retrieve a list of phonebooks
        /// </summary>
        /// <param>No parameters</param>
        /// <returns>Return a list of ordered phonebooks</returns>
        [HttpGet]
        public ActionResult<IEnumerable<PhoneBookForReturnDTO>> Get()
        {
            bool orderByFirstName = true;
            bool asc = true;

            var result = _phoneBookService.GetAllOrderedBy(orderByFirstName, asc).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Add a new phonebook
        /// </summary>
        /// <param>Http request body contains input data for a phonebook</param>
        /// <returns>Return a list with phonebooks</returns>
        [HttpPost]
        public PhoneBookForReturnDTO Post([FromBody] PhoneBookToCreateDTO phoneBook)
        {
            var result = _phoneBookService.Post(phoneBook);

            return result;
        }

        /// <summary>
        /// Delete a PhoneBook
        /// </summary>
        /// <param name="id">The ID of the desired user</param>
        /// <returns>Return true if deleted property was set to true succesfully</returns>
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var result = _phoneBookService.Delete(id);

            return result;
        }

        /// <summary>
        /// Modify PhoneBook
        /// </summary>
        /// <param name="id">The ID of the desired user</param>
        /// <returns>Result of modified user</returns>
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

        /// <summary>
        /// Retrieve the phoneBook by their ID.
        /// </summary>
        /// <param name="id">The ID of the desired user</param>
        /// <returns>an user entity with a list of numbers</returns>
        [HttpGet("{id}")]
        public ActionResult<PhoneBookForReturnDTO> Get(int id)
        {
            var result = _phoneBookService.Get(id);

            return result;
        }

    }
}
