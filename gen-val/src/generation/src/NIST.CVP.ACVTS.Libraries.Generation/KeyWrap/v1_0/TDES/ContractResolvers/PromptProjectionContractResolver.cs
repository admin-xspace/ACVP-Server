﻿using System;
using System.Linq;
using Newtonsoft.Json.Serialization;
using NIST.CVP.ACVTS.Libraries.Generation.Core.ContractResolvers;

namespace NIST.CVP.ACVTS.Libraries.Generation.KeyWrap.v1_0.TDES.ContractResolvers
{
    public class PromptProjectionContractResolver : ProjectionContractResolverBase<TestGroup, TestCase>
    {
        protected override Predicate<object> TestGroupSerialization(JsonProperty jsonProperty)
        {
            var includeProperties = new[]
            {
                nameof(TestGroup.TestGroupId),
                nameof(TestGroup.Tests),
                nameof(TestGroup.TestType),
                nameof(TestGroup.KeyLength),
                nameof(TestGroup.KwCipher),
                nameof(TestGroup.PayloadLen),
                nameof(TestGroup.Direction),
                nameof(TestGroup.KeyingOption)
            };

            if (includeProperties.Contains(jsonProperty.UnderlyingName, StringComparer.OrdinalIgnoreCase))
            {
                return jsonProperty.ShouldSerialize =
                    instance => true;
            }

            return jsonProperty.ShouldSerialize = instance => false;
        }


        protected override Predicate<object> TestCaseSerialization(JsonProperty jsonProperty)
        {
            var includeProperties = new[]
            {
                nameof(TestCase.TestCaseId),
                nameof(TestCase.Key1),
                nameof(TestCase.Key2),
                nameof(TestCase.Key3)
            };

            if (includeProperties.Contains(jsonProperty.UnderlyingName, StringComparer.OrdinalIgnoreCase))
            {
                return jsonProperty.ShouldSerialize =
                    instance => true;
            }

            if (jsonProperty.UnderlyingName.Equals("plaintext", StringComparison.OrdinalIgnoreCase))
            {
                return jsonProperty.ShouldSerialize = instance =>
                {
                    GetTestCaseFromTestCaseObject(instance, out var testGroup, out var testCase);

                    if (testGroup.Direction.Equals("encrypt", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }

                    return false;
                };
            }

            if (jsonProperty.UnderlyingName.Equals("ciphertext", StringComparison.OrdinalIgnoreCase))
            {
                return jsonProperty.ShouldSerialize = instance =>
                {
                    GetTestCaseFromTestCaseObject(instance, out var testGroup, out var testCase);

                    if (testGroup.Direction.Equals("decrypt", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }

                    return false;
                };
            }

            return jsonProperty.ShouldSerialize = instance => false;
        }
    }
}
