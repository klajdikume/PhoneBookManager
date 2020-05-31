-PhoneBookManager.API contains the main controller for Rest Requsets to add, 
  list phonebooks order by username,modify, delete, get by id. Also documented by swagger.
-PhoneBookBLL is Business Layer acesses PhoneBookRepository to give some logic how to return users and their numbers as a phonebook model.
-PhoneBookDAL is library which contains JSON file for stored datas and PhoneBookRepository where are centralized data queries.
  This library goal is to connect with JSON files using Newtonsoft.JSON and retrieve datas.
-PhoneBookDTO contains data models for numbers and phonebook to create and return. Number to create with number and type id,
  Number to return with type name. 
  PhoneBookToCreate with user datas and list of numbers and deleted property in case of modification. 
   This layer transports data between layers
-PhoneBookModel where data relations lives.
  Number is relation entity which can have a User and a Type. User can have some Numbers with a Type.
  Type can be Cellphone, Home or Work. 
