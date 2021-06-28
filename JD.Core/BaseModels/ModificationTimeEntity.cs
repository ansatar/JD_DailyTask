using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Core
{
    public class ModificationTimeEntity : CreationTimeEntity, IHasModificationTime
    {
        //
        // 摘要:
        //     Last modification date of this entity.
        public virtual DateTime? LastModificationTime { get; set; }
    }
}
