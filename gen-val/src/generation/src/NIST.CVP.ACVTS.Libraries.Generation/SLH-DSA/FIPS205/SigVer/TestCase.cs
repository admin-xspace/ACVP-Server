using Newtonsoft.Json;
using NIST.CVP.ACVTS.Libraries.Generation.Core;
using NIST.CVP.ACVTS.Libraries.Math;
using NIST.CVP.ACVTS.Libraries.Oracle.Abstractions.DispositionTypes;

namespace NIST.CVP.ACVTS.Libraries.Generation.SLH_DSA.FIPS205.SigVer;

public class TestCase : ITestCase<TestGroup, TestCase>
{
    public int TestCaseId { get; set; }
    public TestGroup ParentGroup { get; set; }
    public bool? TestPassed { get; set; }
    public bool Deferred { get; }
    
    [JsonProperty(PropertyName = "sk")]
    public BitString PrivateKey { get; set; }
    [JsonProperty(PropertyName = "pk")]
    public BitString PublicKey { get; set; }
    public BitString AdditionalRandomness { get; set; }
    public int MessageLength { get; set; }
    public BitString Message { get; set; }
    public BitString Signature { get; set; }
    
    public SLHDSASignatureDisposition Reason { get; set; }
}
