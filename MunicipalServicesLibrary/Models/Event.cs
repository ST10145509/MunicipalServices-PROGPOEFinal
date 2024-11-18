using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesLibrary.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public int Priority { get; set; }
        public HashSet<string> Tags { get; set; } = new HashSet<string>();
        public string Organizer { get; set; }
    }
}
