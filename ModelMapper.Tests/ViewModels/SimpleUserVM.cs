using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper.Tests.Models
{
    class SimpleUserVM : IMappable
    {
        public DateTime Date { get; internal set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
