using System;
using System.Collections.Generic;

namespace Krk.Common.Animations
{
    [Serializable]
    public class SequenceData
    {
        public SequenceConfig config;
        public List<SequenceStepData> steps;
    }
}