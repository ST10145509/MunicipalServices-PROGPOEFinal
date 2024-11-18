using MunicipalServicesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesLibrary.Data
{
    public class IssueRepository
    {
        private static List<Issue> _issues = new List<Issue>();
        private static int _nextId = 1;

        public List<Issue> GetAllIssues()
        {
            return _issues;
        }

        public void AddIssue(Issue issue)
        {
            issue.Id = _nextId++;
            _issues.Add(issue);
        }

        public Issue GetIssueById(int id)
        {
            return _issues.FirstOrDefault(i => i.Id == id);
        }

        public List<Issue> GetIssuesByCategory(string category)
        {
            return _issues.Where(i => i.Category == category).ToList();
        }

        public List<Issue> GetIssuesByLocation(string location)
        {
            return _issues.Where(i => i.Location.Contains(location)).ToList();
        }
    }
}
