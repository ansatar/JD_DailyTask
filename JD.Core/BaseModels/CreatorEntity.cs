using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Core
{
    public class CreatorEntity : BaseEntity
    {
        //
        // 摘要:
        //     Creation time of this entity.
        public virtual DateTime CreationTime { get; set; }

        //
        // 摘要:
        //     Creator of this entity.
        public virtual long? CreatorUserId { get; set; }
    }
}
