﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Libs.DataTransmission.JsonModels.Synchronization
{
    public class PushCapsula
    {
        public IList<PushMetaMessage> metaMessages;
        public IList<long> messageToDeleteIds;
    }
}
