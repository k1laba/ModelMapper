using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper.Tests.Entities
{
    class Order
    {
        public ICollection<int> OrderItemsIds { get; set; }
    }
}
