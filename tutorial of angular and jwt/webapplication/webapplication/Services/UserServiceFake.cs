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
                    UserPassword="jrdyeYzNhmJ8UFjx8uVDk1DKEiCwvJkdqyD+VmtlZUqnuDRM",
                }
            };
        }

        public List<User> Get()
        {
            List<User> a = new List<User>();
            User u = _users.Find(user => true);
            a.Add(u);
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
            found = userIn;
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
