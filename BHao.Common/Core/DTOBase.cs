using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Common.Core
{
    [DataContract]
    public class DTOBase : IExtensibleDataObject
    {
        public ExtensionDataObject ExtensionData { get; set; }
        
    }
}
