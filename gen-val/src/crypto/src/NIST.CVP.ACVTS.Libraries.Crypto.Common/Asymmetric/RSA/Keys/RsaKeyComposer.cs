﻿using System;
using System.Numerics;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.RSA.PrimeGenerators;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Math;
using NIST.CVP.ACVTS.Libraries.Math.Helpers;

namespace NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.RSA.Keys
{
    public class RsaKeyComposer : IRsaKeyComposer
    {
        public KeyPair ComposeKey(BigInteger e, PrimePair primes)
        {
            var n = primes.P * primes.Q;

            // Checks to avoid exceptions
            if (primes.P == 0 || primes.Q == 0 || e == 0)
            {
                throw new Exception("Invalid p, q, e provided for standard RSA Key");
                //return new KeyPair
                //{
                //    PrivKey = new CrtPrivateKey {P = primes.P, Q = primes.Q},
                //    PubKey = new PublicKey {E = e, N = n}
                //};
            }

            var d = e.ModularInverse(NumberTheory.LCM(primes.P - 1, primes.Q - 1));

            return new KeyPair
            {
                PrivKey = new PrivateKey
                {
                    D = d,
                    P = primes.P,
                    Q = primes.Q
                },
                PubKey = new PublicKey
                {
                    E = e,
                    N = n
                }
            };
        }
    }
}
