using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Northernrunners.ImportLibrary.Dto;
using Northernrunners.ImportLibrary.Poco;
using Northernrunners.ImportLibrary.Utils;

namespace Northernrunners.ImportLibrary.Service
{
    public class EventResultHandler
    {
        private readonly IUserService _userService;
        private readonly IEventService _eventService;

        public EventResultHandler(IUserService userService, IEventService eventService)
        {
            _userService = userService;
            _eventService = eventService;
        }

        public void InsertResultInEvent(ICollection<UserEventInfo> deltakere, Event ev)
        {
            var eventResult = new EventResult {Event = ev};
            // Hvis bruker ikke fins blir dato satt til DateTime.MinValue, resultat vil legges inn i tempresult
            var usernames = deltakere.Select(deltaker => new User { Name = deltaker.Name, Gender = deltaker.Gender.ToUpper(), DateOfBirth = DateTime.MinValue }).ToList();
            
            var watch = new Stopwatch();
            watch.Start();
            var users = _userService.CreateAndGetUsers(usernames, new StreamWriter(Console.OpenStandardOutput()));
            watch.Stop();
            var results = new List<Result>();
            for (var i = 0; i < deltakere.Count; i++)
            {
                var deltaker = deltakere.ToList()[i];
                var result = new Result
                {
                    User = users.First(t => t.Name.Equals(deltaker.Name)),
                    Position = Convert.ToInt32(deltaker.Place)
                };
                var time = deltaker.Time.Substring(0, deltaker.Time.IndexOf(".", StringComparison.Ordinal));
                if (time.Length < 6)
                {
                    time = "0:" + time;
                }
                result.Time = TimeSpan.Parse(time);
                // For å hindre at bogus tid blir lagt inn. Står 0.00 flere steder i importfil
                if (result.Time.TotalSeconds > 10)
                {
                    
                    results.Add(result);
                }
                

            }
            eventResult.Results = results;
            _eventService.AddEventResults(eventResult);
        }

        public ICollection<User> GetUsersWithInvalidDate()
        {
            return _userService.GetAllUsersWithInvalidDate();
        } 
        public void LoadAndFillDeltakerInfo(ICollection<UserEventInfo> deltakere)
        {
            var usernames = deltakere.Select(deltaker => new User { Name = deltaker.Name, Gender = deltaker.Gender.ToUpper(), DateOfBirth = DateTime.MinValue }).ToList();
            var users = _userService.CreateAndGetUsers(usernames, new StreamWriter(Console.OpenStandardOutput()));
            foreach (var deltaker in deltakere)
            {
                var userInDb = users.First(t => t.Name.Equals(deltaker.Name));
                deltaker.ValidDate = userInDb.ValidUser();
            }

        }

        public void UpdateUser(User user)
        {
            _userService.UpdateUser(user);
        }

        public void CreateOrIgnoreUsers(ICollection<User> users)
        {
            _userService.CreateAndGetUsers(users, new StreamWriter(Console.OpenStandardOutput()));
        }

        public void UpdateTempResults()
        {
            var tempResults = _eventService.GetAllTempResults();
            var itemsToDelete = new List<TempResultDto>();
            var invalidUsers = _userService.GetAllUsersWithInvalidDate();
            foreach (var tempResult in tempResults)
            {
                var userId = tempResult.UserId;
                var resultHasInvalidUser = invalidUsers.FirstOrDefault(t => t.Id.Equals(userId));
                if (resultHasInvalidUser == null) 
                {
                    itemsToDelete.Add(tempResult);
                }
            }
            foreach (var itemToDelete in itemsToDelete)
            {
                var eventResult = Tools.Deserializate<EventResult>(itemToDelete.Data);
                _eventService.AddEventResults(eventResult);
                _eventService.DeleteTempResult(itemToDelete);
            }
        }
    }
}
