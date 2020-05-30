using PhoneBookDAL.Interfaces;
using PhoneBookModel.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PhoneBookDAL.Repositories
{
    //aksesi i te dhenave ne filet JSON Data Access Layer
    public class PhoneBookRepository : IPhoneBookRepository
    {
        public readonly string DatabasePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "PhoneBookDAL", "Database");

        public readonly string _typeJson;
        public readonly string _numberJson;
        public readonly string _userJson;

        public PhoneBookRepository()
        {
            _typeJson = Path.Combine(DatabasePath, "type.json");
            _numberJson = Path.Combine(DatabasePath, "number.json");
            _userJson = Path.Combine(DatabasePath, "user.json");
        }

        //Lexo te gjithe userat dhe numrat e secilit qe u perkasin id te njejte
        public List<User> GetAll()
        {
            var userJson = File.ReadAllText(_userJson);
            var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userJson).Where(x => !x.Deleted).ToList();

            var userTypeJson = File.ReadAllText(_numberJson);
            var usertypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Number>>(userTypeJson).Where(x => !x.Deleted).ToList();

            var typeJson = File.ReadAllText(_typeJson);
            var types = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PhoneBookModel.Models.Type>>(typeJson).Where(x => !x.Deleted).ToList();

            usertypes.ForEach(x => x.Type = types.FirstOrDefault(y => y.Id == x.IdType));
            users.ForEach(x => x.Numbers = usertypes.Where(y => y.IdUser == x.Id));

            return users;
        }

        //Shto nje user
        public User Post(User user)
        {
            // merr te gjithe userat 
            var userJson = File.ReadAllText(_userJson);
            var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userJson).ToList();

            var userId = users.Count() + 1;

            users.Add(new User
            {
                Id = userId,
                Name = user.Name,
                LastName = user.LastName,
                Deleted = false
            });

            // Ruaj userat
            string outputUser = Newtonsoft.Json.JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_userJson, outputUser);

            // merr numrat e userave
            var userTypeJson = File.ReadAllText(_numberJson);
            var usertypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Number>>(userTypeJson).ToList();

            int userTypesId = usertypes.Count() + 1;

            var userTypesFromDTO = user.Numbers?.ToList().Select(x => new Number
            {
                Id = userTypesId++,
                UserNumber = x.UserNumber,
                IdType = x.IdType,
                Deleted = false,
                IdUser = userId
            }).ToList();

            
            usertypes.AddRange(userTypesFromDTO);

            // Ruaj numrat
            string outputUserType = Newtonsoft.Json.JsonConvert.SerializeObject(usertypes, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_numberJson, outputUserType);

            // Merr userin e krijuar/modifikuar
            return GetUser(userId);
        }

        private User GetUser(int id)
        {
            // Merr user
            var userJson = File.ReadAllText(_userJson);
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userJson).FirstOrDefault(x => !x.Deleted && x.Id == id);

            // Merr numrat
            var userNumbersJson = File.ReadAllText(_numberJson);
            var usernumbertypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Number>>(userNumbersJson).Where(x => !x.Deleted && x.IdUser == id).ToList();

            // merr tipet per numrat
            var typeJson = File.ReadAllText(_typeJson);
            var types = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PhoneBookModel.Models.Type>>(typeJson).Where(x => !x.Deleted).ToList();

            usernumbertypes.ForEach(x => x.Type = types.FirstOrDefault(y => y.Id == x.IdType));
            user.Numbers = usernumbertypes;

            // Kthe user
            return user;
        }

        //Vjen nga kerkesa delete/id dhe kthen true nese kryhet me sukses modifikimi deleted ne true i Numrit
        public bool Delete(int id)
        {
            //marr userat
            var userJson = File.ReadAllText(_userJson);
            var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userJson).ToList();

            //userin me id perkatese behet delete true
            users.Where(x => x.Id == id).ToList().ForEach(x =>
            {
                x.Deleted = true;
            });

            //shkruaj userat ne file
            string outputUser = Newtonsoft.Json.JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_userJson, outputUser);

            //marr Numrat e userit
            var userTypeJson = File.ReadAllText(_numberJson);
            var usertypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Number>>(userTypeJson).ToList();

            //fshij ate numer me id useri 
            usertypes.Where(x => x.IdUser == id).ToList().ForEach(x =>
            {
                x.Deleted = true;
            });

            string outputUserType = Newtonsoft.Json.JsonConvert.SerializeObject(usertypes, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_numberJson, outputUserType);

            return true;
        }

        public User Put(User user)
        {
            
            var userJson = File.ReadAllText(_userJson);
            var users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(userJson).ToList();

            // Modifiko userin me id perkates
            users.Where(x => x.Id == user.Id).ToList().ForEach(x =>
            {
                x.Name = user.Name;
                x.LastName = user.LastName;
            });

            // Ruaj userat
            string outputUser = Newtonsoft.Json.JsonConvert.SerializeObject(users, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_userJson, outputUser);

            
            var userTypeJson = File.ReadAllText(_numberJson);
            var usertypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Number>>(userTypeJson).ToList();

            List<int> userTypesDTO = user.Numbers.Select(x => x.Id).ToList();

            // Modifiko Userat
            usertypes.Where(x => userTypesDTO.Contains(x.Id)).ToList().ForEach(x =>
            {
                x.UserNumber = user.Numbers.FirstOrDefault(y => y.Id == x.Id)?.UserNumber;
                x.IdType = user.Numbers.FirstOrDefault(y => y.Id == x.Id)?.IdType ?? 1;
            });

            // Ruaj Numrat 
            string outputUserType = Newtonsoft.Json.JsonConvert.SerializeObject(usertypes, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_numberJson, outputUserType);

            // Kthe userin e modifikuar
            return GetUser(user.Id);
        }

        public User Get(int id)
        {
            return GetUser(id);
        }

    }
}
