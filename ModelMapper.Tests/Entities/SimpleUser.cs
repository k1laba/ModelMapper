using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper.Tests.Models
{
    class SimpleUser
    {
        public DateTime Date { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Cities { get; set; }
    }
}
