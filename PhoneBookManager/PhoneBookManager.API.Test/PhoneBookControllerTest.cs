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

        [Fact]
        public void AddPhoneBookTest()
        {

            Random ran = new Random();

            var listWithNumbers = new List<NumberToCreateDTO>()
            {
                new NumberToCreateDTO{ Id= ran.Next(100,199), Number ="06868685", TypeId="1"},
                new NumberToCreateDTO{ Id= ran.Next(100,199), Number ="06867777", TypeId="2"}
            };
            //arrange
            var phoneBook = new PhoneBookToCreateDTO()
            {

                Id = ran.Next(5, 100),
                Firstname = "TestName",
                Lastname = "TestLastName",
                NumberInfo = listWithNumbers

            };


            var createdResponse = _controller.Post(phoneBook);

            _controller.ModelState.AddModelError("Id", "Id is a required field");

            //assert
            Assert.IsType<PhoneBookForReturnDTO>(createdResponse);

            var item = createdResponse as PhoneBookForReturnDTO;

            Assert.Equal(phoneBook.Id, item.Id);
            Assert.Equal(phoneBook.Firstname, item.Firstname);
            Assert.Equal(phoneBook.Lastname, item.Lastname);
            Assert.Equal(phoneBook.NumberInfo.ToString(), item.NumberInfo.ToString());

            



        }

     

    }
}
