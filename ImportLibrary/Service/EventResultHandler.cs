using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Northernrunners.ImportLibrary.Dto;
using Northernrunners.ImportLibrary.Poco;
using Northernrunners.ImportLibrary.Utils;

namespace Northernrunners.ImportLibrary.Service
{
    public class EventResultHandler
    {
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        private readonly IFilterService _filterService;

        public EventResultHandler(IUserService userService, IEventService eventService, IFilterService filterService)
        {
            
            _userService = userService;
            _eventService = eventService;
            _filterService = filterService;
        }

        public void InsertResultInEvent(ICollection<UserEventInfo> deltakere, Event ev)
        {
            if (string.IsNullOrEmpty(ev.Name))
            {
                ev = _eventService.GetEvent(ev.Id);
            }
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
                var time = deltaker.Time;
                try
                {
                    time = deltaker.Time.Substring(0, deltaker.Time.IndexOf(".", StringComparison.Ordinal));
                }
                catch (ArgumentOutOfRangeException) { }
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
            var invalidUsers = _userService.GetAllUsersWithInvalidDate();
            var itemsToDelete = (from tempResult in tempResults let userId = tempResult.UserId let resultHasInvalidUser = invalidUsers.FirstOrDefault(t => t.Id.Equals(userId)) where resultHasInvalidUser == null select tempResult).ToList();
            foreach (var itemToDelete in itemsToDelete)
            {
                Console.WriteLine("------------------------");
                var result = Tools.Deserializate<Result>(itemToDelete.Data);
                var eventId = itemToDelete.EventId;
                var userId = itemToDelete.UserId;
                var user = _userService.FindUser(userId);

                if (user == null)
                {
                    Console.WriteLine("Invalid user, deleting: " + userId);
                    _eventService.DeleteTempResult(itemToDelete);
                    continue;
                };
                result.User = user;
                var ev = _eventService.GetEvent(eventId);
                // Event has been deleted
                if (ev == null)
                {
                    Console.WriteLine("Invalid event: " + eventId + "; User: " + user.Name);
                    _eventService.DeleteTempResult(itemToDelete);
                    continue;
                }
                var eventResult = new EventResult
                {
                    Event = ev,
                        
                    Results = new List<Result>
                    {
                        result
                    }
                };


                _eventService.AddEventResults(eventResult);
                _eventService.DeleteTempResult(itemToDelete);
                Console.WriteLine("Added entry for user: " + user.Name);
                Console.WriteLine("Event: " + ev.DisplayName);
                

            }
        }

        public ICollection<Filter> GetFilters()
        {
            return _filterService.GetFilters();
        }

        public void SaveFilters(ICollection<Filter> filters)
        {
            _filterService.SaveFilters(filters);
        }

        public ICollection<Event> GetEvents(DateTime from, DateTime to)
        {
            return _eventService.GetEvents(from, to);
        }

        public ICollection<UserEventInfo> FilterDeltakere(ICollection<UserEventInfo> deltakere)
        {
            var filters = _filterService.GetFilters();
            var newList = new List<UserEventInfo>();
            
            foreach (var filter in filters)
            {
                
                if (filter.Type.Equals(FilterType.Contains))
                {
                    newList.AddRange(deltakere.Where(t => t.Club.Contains(filter.Value)).ToList());                    
                }
                if (filter.Type.Equals(FilterType.Equals))
                {
                    newList.AddRange(deltakere.Where(t => t.Club.Equals(filter.Value)).ToList());
                }
            }
            return newList.Distinct().ToList();
        }

    }
}
