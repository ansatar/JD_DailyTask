using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JD.Core
{
    public class ModifierEntity : CreatorEntity
    {
        //
        // 摘要:
        //     Last modification date of this entity.
        public virtual DateTime? LastModificationTime { get; set; }

        //
        // 摘要:
        //     Last modifier user of this entity.
        public virtual long? LastModifierUserId { get; set; }
    }
}
