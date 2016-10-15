using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelMapper.Tests.ViewModels
{
    class OrderVM
    {
        public ICollection<int> OrderItemsIds { get; set; }
    }
}
