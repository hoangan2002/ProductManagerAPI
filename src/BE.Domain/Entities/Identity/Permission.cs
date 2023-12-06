using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Entities.Identity;
public class Permission
{
    public Guid RoleId { get; set; }
    public string FunctionId { get; set; }
    public string ActionId { get; set; }
}
