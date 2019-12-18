using System;
using System.Collections.Generic;
using webapplication.Models;

namespace webapplication.Services
{
    public interface IUserService
    {
        public List<User> Get();

        public User Get(string id);

        public User Create(User user);

        public void Update(string id, User userIn);

        public void Remove(User userIn);

        public void Remove(string id);
    }
}
