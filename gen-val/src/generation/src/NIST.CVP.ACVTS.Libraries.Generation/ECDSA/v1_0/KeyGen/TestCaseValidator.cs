﻿using System.Collections.Generic;
using System.Threading.Tasks;
using NIST.CVP.ACVTS.Libraries.Crypto.Common.Asymmetric.DSA.ECC;
using NIST.CVP.ACVTS.Libraries.Generation.Core;
using NIST.CVP.ACVTS.Libraries.Generation.Core.Async;
using NIST.CVP.ACVTS.Libraries.Generation.Core.Enums;
using NIST.CVP.ACVTS.Libraries.Math;

namespace NIST.CVP.ACVTS.Libraries.Generation.ECDSA.v1_0.KeyGen
{
    public class TestCaseValidator : ITestCaseValidatorAsync<TestGroup, TestCase>
    {
        private readonly TestCase _expectedResult;
        private readonly TestGroup _group;
        private readonly IDeferredTestCaseResolverAsync<TestGroup, TestCase, EccKeyPairGenerateResult> _deferredResolver;

        public int TestCaseId => _expectedResult.TestCaseId;

        public TestCaseValidator(
            TestCase expectedResult,
            TestGroup group,
            IDeferredTestCaseResolverAsync<TestGroup, TestCase, EccKeyPairGenerateResult> deferredResolver
        )
        {
            _expectedResult = expectedResult;
            _group = group;
            _deferredResolver = deferredResolver;
        }

        public async Task<TestCaseValidation> ValidateAsync(TestCase suppliedResult, bool showExpected = false)
        {
            var errors = new List<string>();
            var expected = new Dictionary<string, string>();
            var provided = new Dictionary<string, string>();

            if (suppliedResult.KeyPair.PrivateD == 0 || suppliedResult.KeyPair.PublicQ.X == 0 || suppliedResult.KeyPair.PublicQ.Y == 0)
            {
                errors.Add("Could not find value in key pair");
            }
            else
            {
                var deferredResult = await _deferredResolver.CompleteDeferredCryptoAsync(_group, _expectedResult, suppliedResult);
                if (!deferredResult.Success)
                {
                    errors.Add("Unable to generate public key from private key d value");
                }
                else
                {
                    if (deferredResult.KeyPair.PublicQ.X != suppliedResult.KeyPair.PublicQ.X)
                    {
                        errors.Add("Incorrect Qx generated from private key");
                        expected.Add(nameof(deferredResult.KeyPair.PublicQ.X), new BitString(deferredResult.KeyPair.PublicQ.X).ToHex());
                        provided.Add(nameof(suppliedResult.KeyPair.PublicQ.X), new BitString(suppliedResult.KeyPair.PublicQ.X).ToHex());
                    }

                    if (deferredResult.KeyPair.PublicQ.Y != suppliedResult.KeyPair.PublicQ.Y)
                    {
                        errors.Add("Incorrect Qy generated from private key");
                        expected.Add(nameof(deferredResult.KeyPair.PublicQ.Y), new BitString(deferredResult.KeyPair.PublicQ.Y).ToHex());
                        provided.Add(nameof(suppliedResult.KeyPair.PublicQ.Y), new BitString(suppliedResult.KeyPair.PublicQ.Y).ToHex());
                    }
                }
            }

            if (errors.Count > 0)
            {
                return new TestCaseValidation
                {
                    TestCaseId = suppliedResult.TestCaseId,
                    Result = Disposition.Failed,
                    Reason = string.Join(";", errors),
                    Expected = expected.Count != 0 && showExpected ? expected : null,
                    Provided = provided.Count != 0 && showExpected ? provided : null
                };
            }

            return new TestCaseValidation { TestCaseId = suppliedResult.TestCaseId, Result = Disposition.Passed };
        }
    }
}
