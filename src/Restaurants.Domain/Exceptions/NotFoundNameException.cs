using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Exceptions
{
    public class NotFoundNameException(string resourceType, string resourceName)
        : Exception($"{resourceType} with name: {resourceName} does not exist")
    {
    }
}
