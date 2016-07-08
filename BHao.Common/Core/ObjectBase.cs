using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Common.Core
{
    public class ObjectBase : IExtensibleDataObject
    {
        public ExtensionDataObject ExtensionData {get; set;}
        
    }
}
