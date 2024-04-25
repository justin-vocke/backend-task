using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTask.Business.DTOs
{
    public record StudentForCreationDto(string FirstName, string LastName, int Age);
}
