using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Core
{
    public interface IHasModificationTime
    {
        //
        // 摘要:
        //     The last modified time for this entity.
        DateTime? LastModificationTime
        {
            get;
            set;
        }
    }
}
