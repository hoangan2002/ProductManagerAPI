using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BE.Domain.Entities.Identity;
public  class AppUser : IdentityUser<Guid>
{
}
