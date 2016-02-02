using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Northernrunners.ImportLibrary.Poco;
using Northernrunners.ImportLibrary.Utils;
using NR_Resultat_Import;

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

        public void InsertResultInEvent(ICollection<Deltaker> deltakere, Event ev)
        {
            var eventResult = new EventResult {Event = ev};
            var usernames = deltakere.Select(deltaker => new User { Name = deltaker.Name, Gender = deltaker.Gender.ToUpper(), DateOfBirth = DateTime.MinValue }).ToList();
            
            Tools.RandomizeDateOfBirth(usernames);
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
                results.Add(result);

            }
            eventResult.Results = results;
            _eventService.AddEventResults(eventResult);
        }

        public void LoadAndFillDeltakerInfo(ICollection<Deltaker> deltakere)
        {
            var usernames = deltakere.Select(deltaker => new User { Name = deltaker.Name, Gender = deltaker.Gender.ToUpper(), DateOfBirth = DateTime.MinValue }).ToList();
            var users = _userService.CreateAndGetUsers(usernames, new StreamWriter(Console.OpenStandardOutput()));
            foreach (var deltaker in deltakere)
            {
                var userInDb = users.First(t => t.Name.Equals(deltaker.Name));
                deltaker.ValidDate = userInDb.ValidUser();
            }

        }
    }
}
