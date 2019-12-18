using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using webapplication.Controllers;
using webapplication.Models;
using webapplication.Services;
using Xunit;

namespace webapp.Tests
{
    public class EventControllerTest
    {
        EventController _eventController;
        IEventService _eventService;
        IUserService _userService;

        public EventControllerTest()
        {
            _eventService = new EventServiceFake();
            _userService = new UserServiceFake();
            _eventController = new EventController(_eventService, _userService);
        }

        [Theory]
        [InlineData("5df9cb2ebefdf24095aa3256")]
        [InlineData("5df9cb2ebefdf24095aa3257")]
        [InlineData("5df9cb2ebefdf24095aa3258")]
        public void Get_FindsAndChecksForEvent(string x)
        {
            var _event = _eventController.Get(x);

            Assert.True(_event.Value.Id == x);
        }

        [Theory]
        [InlineData("5df9cb2ebefdf24095aa3256", "5df9cb2ebefdf24095aa3257", "5df9cb2ebefdf24095aa3258")]
        public void Remove_RemovesAllElements(string x, string y, string z)
        {
            _eventController.Delete(x);
            _eventController.Delete(y);
            _eventController.Delete(z);

            var a = _eventService.Get("5dee34ce743cd90e14ab5f81",true);
            
            Assert.True(a.Count == 0);
        }

        [Theory]
        [InlineData("5df9cb2ebefdf24095aa3256", "Pakeistas tekstas", "", ""
            , "", "", "", "", "", "", "", "")]
        [InlineData("5df9cb2ebefdf24095aa3257", "Pakeistas tekstas", "", ""
            , "", "", "", "", "", "", "", "")]
        [InlineData("5df9cb2ebefdf24095aa3258", "Pakeistas tekstas", "", ""
            , "", "", "", "", "", "", "", "")]
        public void Update_UpdateEventAllFields(string a, string b, string c,
            string d, string e, string f, string g, string h, string i, string j,
            string k, string l)
        {
            Event _event = new Event()
            {
                Id = a,
                EventName = b,
                EventAddress = c,
                EventDescription = d,
                EventStartDate = e,
                EventStartTime = f,
                EventEndDate = g,
                EventEndTime = h,
                EventTravelTime = i,
                EventRepeat = j,
                EventPriority = k,
                EventCreationDate = l,
                UserId = "5dee34ce743cd90e14ab5f81"
            };

            _eventController.Update(a, _event);
            var lala = _eventController.Get(a);
            

            Assert.Equal(lala.Value.EventName,_event.EventName);
            Assert.Equal(lala.Value.EventAddress, _event.EventAddress);
            Assert.Equal(lala.Value.EventDescription, _event.EventDescription);
            Assert.Equal(lala.Value.EventStartDate, _event.EventStartDate);
            Assert.Equal(lala.Value.EventCreationDate, _event.EventCreationDate);
        }

        [Theory]
        [InlineData("5df9cb2ebefdf24095aa3256", "Pakeistas tekstas")]
        [InlineData("5df9cb2ebefdf24095aa3257", "Pakeistas tekstas")]
        [InlineData("5df9cb2ebefdf24095aa3258", "Pakeistas tekstas")]
        public void Update_UpdateEventSomeFields(string id, string text)
        {
            Event _event = new Event()
            {
                Id = id,
                EventName = text,
                EventAddress = "",
                EventDescription = "",
                EventStartDate = "2019-12-18",
                EventStartTime = "",
                EventEndDate = "2019-12-18",
                EventEndTime = "",
                EventTravelTime = "",
                EventRepeat = "",
                EventPriority = "notImportant",
                EventCreationDate = "12/18/2019 11:53:36",
                UserId = "5dee34ce743cd90e14ab5f81"
            };

            _eventController.Update(id, _event);

            Assert.True(_event.EventName == text);
        }
    }
}
