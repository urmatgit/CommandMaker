using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Domain.Common.Contracts;
public interface ICheckOwner
{
     Guid CreatedBy { get; set; }
}
