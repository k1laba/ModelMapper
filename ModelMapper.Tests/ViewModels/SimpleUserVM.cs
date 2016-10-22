using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper.Tests.Models
{
    class SimpleUserVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<int> ProductIds { get; set; }
        public List<string> Citites { get; set; }
        public Status Status { get; set; }
    }
    public enum Status
    {
        Active = 0,
        Disabled = 1
    }
}
