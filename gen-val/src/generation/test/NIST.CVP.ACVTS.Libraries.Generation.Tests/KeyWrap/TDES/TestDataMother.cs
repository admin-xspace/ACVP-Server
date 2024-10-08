﻿using System.Collections.Generic;
using NIST.CVP.ACVTS.Libraries.Generation.KeyWrap.v1_0.TDES;
using NIST.CVP.ACVTS.Libraries.Math;

namespace NIST.CVP.ACVTS.Libraries.Generation.Tests.KeyWrap.TDES
{
    public static class TestDataMother
    {
        public static TestVectorSet GetTestGroups(int groups = 2, string direction = "encrypt", bool isFailureTest = false)
        {
            var vectorSet = new TestVectorSet();

            var list = new List<TestGroup>();
            vectorSet.TestGroups = list;
            for (var groupIdx = 0; groupIdx < groups; groupIdx++)
            {
                var tg = new TestGroup
                {
                    Direction = direction,
                    PayloadLen = 128,
                    KeyLength = 128,
                    KwCipher = "cipher",
                    TestType = "AFT"
                };

                for (var testId = 15 * groupIdx + 1; testId <= (groupIdx + 1) * 15; testId++)
                {
                    var tc = new TestCase
                    {
                        Key = new BitString(128),
                        PlainText = new BitString(128),
                        CipherText = new BitString(128),
                        TestPassed = !isFailureTest,
                        TestCaseId = testId,
                        ParentGroup = tg
                    };

                    tg.Tests.Add(tc);
                }

                list.Add(tg);
            }

            return vectorSet;
        }
    }
}
