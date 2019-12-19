using System;
using System.Collections.Generic;
using webapplication.Models;
using System.Linq;

namespace webapplication.Services
{
    public class UserServiceFake : IUserService
    {
        public readonly List<User> _users;

        public UserServiceFake()
        {
            _users = new List<User>()
            {
                new User()
                {
                    Id="5dee34ce743cd90e14ab5f81",
                    FirstName="Martynas",
                    LastName="Petruska",
                    UserLogIn="marpet",
                    UserPassword="slaptas"
                },
                new User()
                {
                    Id="5dee34f8743cd90e14ab5f82",
                    FirstName="Brigita",
                    LastName="Macaite",
                    UserLogIn="brimac",
                    UserPassword="slaptas"
                }
            };
        }

        public List<User> Get()
        {
            List<User> a = new List<User>();
            foreach(var user in _users)
            {
                a.Add(user);
            }
            return a;
        }

        public User Get(string id) =>
            _users.Find(user => user.Id == id);

        public User Create(User user)
        {
            _users.Add(user);
            return user;
        }

        public void Update(string id, User userIn)
        {
            var found = _users.FirstOrDefault(user => user.Id == id);
            _users.Remove(found);
            _users.Add(userIn);
        }

        public void Remove(User userIn)
        {
            var found = _users.FirstOrDefault(user => user.Id == userIn.Id);
            _users.Remove(found);
        }

        public void Remove(string id)
        {
            var found = _users.FirstOrDefault(user => user.Id == id);
            _users.Remove(found);
        }
    }
}
