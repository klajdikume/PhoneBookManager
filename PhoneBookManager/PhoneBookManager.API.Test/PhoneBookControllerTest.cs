using System;
using Xunit;
using PhoneBookManager.API.Controllers;
using PhoneBookBLL.Interfaces;
using PhoneBookBLL.Services;
using System.Collections.Generic;
using PhoneBookDTOs.DTO;
using Microsoft.AspNetCore.Mvc;
using PhoneBookDAL.Interfaces;
using PhoneBookDAL.Repositories;


namespace PhoneBookManager.API.Test
{
    public class PhoneBookControllerTest
    {
        PhoneBookController _controller;
        IPhoneBookServices _service;
        IPhoneBookRepository _repo;
        public PhoneBookControllerTest()
        {
            _repo = new PhoneBookRepository();
            _service = new PhoneBookServices(_repo);
            _controller = new PhoneBookController(_service);
        }

        [Fact]
        public void GetAllTest()
        {
            //arrange
            //act
            //get resulst by making requerst
            var result = _controller.Get();
            //assert 
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;
            Assert.IsType<List<PhoneBookForReturnDTO>>(list.Value);

            var listPhoneBooks = list.Value as List<PhoneBookForReturnDTO>;
            Assert.Equal(2, listPhoneBooks.Count);


        }

    }
}
