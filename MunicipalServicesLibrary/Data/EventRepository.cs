using MunicipalServicesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesLibrary.Data
{
    public class EventRepository
    {
        private static SortedDictionary<DateTime, List<Event>> _eventsByDate = new SortedDictionary<DateTime, List<Event>>();
        private static HashSet<string> _eventCategories = new HashSet<string>();
        private static Queue<Event> _upcomingEventQueue = new Queue<Event>();
        private static int _nextId = 1;

        public void AddEvent(Event newEvent)
        {
            newEvent.Id = _nextId++;
            
            if (!_eventsByDate.ContainsKey(newEvent.Date))
            {
                _eventsByDate[newEvent.Date] = new List<Event>();
            }

            _eventsByDate[newEvent.Date].Add(newEvent);
            _eventCategories.Add(newEvent.Category);

            if (newEvent.Date >= DateTime.Now)
            {
                _upcomingEventQueue.Enqueue(newEvent);
            }
        }

        public List<Event> GetAllEvents()
        {
            return _eventsByDate.SelectMany(eventPair => eventPair.Value).ToList();
        }

        public Event GetNextUpcomingEvent()
        {
            return _upcomingEventQueue.Count > 0 ? _upcomingEventQueue.Peek() : null;
        }

        public List<Event> GetEventsByCategory(string category)
        {
            return GetAllEvents().Where(e => e.Category == category).ToList();
        }

        public HashSet<string> GetAllCategories()
        {
            return _eventCategories;
        }

        public List<Event> GetRecommendedEvents(List<string> recentSearchCategories)
        {
            return _eventsByDate
                .Where(eventPair => eventPair.Key >= DateTime.Now)
                .SelectMany(eventPair => eventPair.Value)
                .Where(ev => recentSearchCategories.Contains(ev.Category))
                .OrderBy(ev => ev.Date)
                .ToList();
        }

        public void ClearEvents()
        {
            _eventsByDate.Clear();
            _eventCategories.Clear();
            _upcomingEventQueue.Clear();
            _nextId = 1;
            InitializeCategories();
        }

        public void InitializeCategories()
        {
            _eventCategories.Clear();
            var defaultCategories = new[]
            {
                "Arts",
                "Health",
                "Food",
                "Career",
                "Entertainment",
                "Community",
                "Education",
                "Culture",
                "Business",
                "Environment"
            };
            
            foreach (var category in defaultCategories)
            {
                _eventCategories.Add(category);
            }
        }
    }
}
