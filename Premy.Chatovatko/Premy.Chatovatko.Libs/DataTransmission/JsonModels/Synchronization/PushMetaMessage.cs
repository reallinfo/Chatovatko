using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.DataTransmission.JsonModels.Synchronization
{
    public class PushMetaMessage
    {
        public long RecepientId { get; set; }
        public long? PublicId { get; set; }
    }
}
