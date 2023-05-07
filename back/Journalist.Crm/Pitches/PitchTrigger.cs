using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journalist.Crm.Domain.Pitches
{
    internal enum  PitchTrigger
    {
        Save,
        Validate,
        Send,
        Accept,
        Refuse,
        Cancel
    }
}
