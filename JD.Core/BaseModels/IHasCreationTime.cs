using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Core
{
    public interface IHasCreationTime
    {
        //
        // 摘要:
        //     Creation time of this entity.
        DateTime CreationTime
        {
            get;
            set;
        }
    }
}
