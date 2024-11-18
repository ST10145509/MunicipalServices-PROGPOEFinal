using MunicipalServicesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesLibrary.Data
{
    public class EventManager
    {
        private SortedDictionary<DateTime, List<Event>> _eventsByDate;
        private Dictionary<string, HashSet<Event>> _eventsByCategory;
        private Queue<Event> _upcomingEvents;
        private Stack<string> _searchHistory;
        private Dictionary<string, int> _searchFrequency;
        private HashSet<string> _popularCategories;
        
        public EventManager()
        {
            _eventsByDate = new SortedDictionary<DateTime, List<Event>>();
            _eventsByCategory = new Dictionary<string, HashSet<Event>>();
            _upcomingEvents = new Queue<Event>();
            _searchHistory = new Stack<string>();
            _searchFrequency = new Dictionary<string, int>();
            _popularCategories = new HashSet<string>();
        }

        public void AddEvent(Event evt)
        {
            // Add to sorted dictionary by date
            if (!_eventsByDate.ContainsKey(evt.Date))
                _eventsByDate[evt.Date] = new List<Event>();
            _eventsByDate[evt.Date].Add(evt);

            // Add to category dictionary
            if (!_eventsByCategory.ContainsKey(evt.Category))
                _eventsByCategory[evt.Category] = new HashSet<Event>();
            _eventsByCategory[evt.Category].Add(evt);

            // Add to upcoming events queue if event is in the future
            if (evt.Date >= DateTime.Today)
                _upcomingEvents.Enqueue(evt);
        }

        public List<Event> GetRecommendations(string searchTerm)
        {
            UpdateSearchPatterns(searchTerm);
            
            var recommendations = new HashSet<Event>();
            
            // Debug logging
            Console.WriteLine($"Popular Categories: {string.Join(", ", _popularCategories)}");
            Console.WriteLine($"Search History: {string.Join(", ", _searchHistory)}");
            
            // Get matching categories from the search term
            var matchingCategories = _eventsByCategory.Keys
                .Where(category => category.ToLower().Contains(searchTerm.ToLower()))
                .ToList();
            
            Console.WriteLine($"Matching Categories: {string.Join(", ", matchingCategories)}");
            
            // Add events from frequently searched categories
            foreach (var category in _popularCategories)
            {
                if (_eventsByCategory.ContainsKey(category))
                {
                    foreach (var evt in _eventsByCategory[category])
                    {
                        recommendations.Add(evt);
                    }
                }
            }
            
            // Add events from matching categories
            foreach (var category in matchingCategories)
            {
                foreach (var evt in _eventsByCategory[category])
                {
                    recommendations.Add(evt);
                }
            }

            return recommendations.OrderByDescending(e => GetEventPriority(e)).ToList();
        }

        public void UpdateSearchPatterns(string searchTerm)
        {
            _searchHistory.Push(searchTerm);
            
            // Find matching categories and titles
            var matchingCategories = _eventsByCategory.Keys
                .Where(category => category.ToLower().Contains(searchTerm.ToLower()))
                .ToList();
            
            var matchingEvents = _eventsByCategory
                .SelectMany(kvp => kvp.Value)
                .Where(evt => evt.Title.ToLower().Contains(searchTerm.ToLower()))
                .ToList();
            
            // Add categories from matching events
            foreach (var evt in matchingEvents)
            {
                if (!_searchFrequency.ContainsKey(evt.Category))
                    _searchFrequency[evt.Category] = 0;
                _searchFrequency[evt.Category]++;

                // Only add to popular categories after 3 searches
                if (_searchFrequency[evt.Category] > 3)
                {
                    if (!_popularCategories.Contains(evt.Category))
                        _popularCategories.Add(evt.Category);
                }
            }
            
            // Add directly matching categories
            foreach (var category in matchingCategories)
            {
                if (!_searchFrequency.ContainsKey(category))
                    _searchFrequency[category] = 0;
                _searchFrequency[category]++;

                // Only add to popular categories after 3 searches
                if (_searchFrequency[category] > 3)
                {
                    if (!_popularCategories.Contains(category))
                        _popularCategories.Add(category);
                }
            }

            // Debug output
            Console.WriteLine($"Search frequencies:");
            foreach (var kvp in _searchFrequency)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value} searches");
            }
        }

        private int GetEventPriority(Event evt)
        {
            int priority = 0;
            if (_popularCategories.Contains(evt.Category)) priority += 2;
            if (evt.Date <= DateTime.Now.AddDays(7)) priority += 1;
            return priority;
        }
    }
}
