using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class UserService
    {
        private readonly FileService _fileService;

        public UserService()
        {
            _fileService = new FileService();
        }

        public IEnumerable<User> GetList()
        {
            try
            {
                var users = new List<User>();
                var reader = _fileService.ReadUsersFromFile();
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLineAsync().Result.Split(",");
                    var user = new User
                    {
                        Name = line[0].ToString(),
                        Email = line[1].ToString(),
                        Phone = line[2].ToString(),
                        Address = line[3].ToString(),
                        UserType = line[4].ToString(),
                        Money = decimal.Parse(line[5].ToString()),
                    };
                    users.Add(user);
                }
                reader.Close();

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User Create(User user)
        {
            try
            {
                user.Email = FormatEmail(user.Email);
                var money = GetConvertedMoney(user).ToString();
                var line = $"{user.Name},{user.Email},{user.Phone},{user.Address},{user.UserType},{money}";
                _fileService.WriteUserFromFile(line);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private string FormatEmail(string email)
        {
            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            email = string.Join("@", new string[] { aux[0], aux[1] });
            return email;
        }
        private decimal GetConvertedMoney(User user)
        {
            if (user.UserType == UserTypes.Normal)
            {
                if (user.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = user.Money * percentage;
                    return user.Money + gif;
                }
                if (user.Money < 100)
                {
                    if (user.Money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = user.Money * percentage;
                        return user.Money + gif;
                    }
                }
            }
            if (user.UserType == UserTypes.SuperUser)
            {
                if (user.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = user.Money * percentage;
                    return user.Money + gif;
                }
            }
            if (user.UserType == UserTypes.Premium)
            {
                if (user.Money > 100)
                {
                    var gif = user.Money * 2;
                    return user.Money + gif;
                }
            }

            return 0;
        }
    }
}
