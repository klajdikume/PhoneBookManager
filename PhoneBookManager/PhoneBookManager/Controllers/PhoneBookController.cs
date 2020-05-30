using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBookBLL.Interface;
using PhoneBookDTOs.DTO;

namespace PhoneBookManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPhoneBookServices _phoneBookService;

        public PhoneBookController()
        {

        }

    }
}
