﻿using NIST.CVP.ACVTS.Libraries.Math;

namespace NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.RSA
{
    public class AlgoArrayResponseKey
    {
        public BitString E { get; set; }
        public BitString P { get; set; }
        public BitString Q { get; set; }
        public bool FailureTest { get; set; }
    }
}
