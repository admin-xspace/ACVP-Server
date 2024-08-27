dotnet test NIST.CVP.ACVTS.Libraries.Generation.AES_CBC.IntegrationTests.csproj --filter Category=FastIntegrationTest# Security Policy
NIST.CVP.ACVTS.Libraries.Generation.AES_CBC.IntegrationTests.csproj
## Supported Versionssc delete AcvpOrleans # if exists
sc create AcvpOrleans binPath= "C:path/to/executable/NIST.CVP.Orleans.ServerHost.dll"
sc start AcvpOrleans
dotnet test NIST.CVP.ACVTS.Libraries.Generation.AES_CBC.IntegrationTests.csproj
Use this section to tell people about which versions of your project are
currently being supported with security updates.
dotnet run -a [answerFile] -b [iutResponsesFile]
| Version | Supported          |
| ------- | ------------------ |
| 5.1.x   | :white_check_mark: |
| 5.0.x   | :x:                |
| 4.0.x   | :white_check_mark: |
| < 4.0   | :x:                |

## Reporting a Vulnerability
dotnet run -c "C:/registrations/1971-01-01/registration.json"
Use this section to tell people how to report a vulnerability.

Tell them where to go, how often they can expect to get an update on a
reported vulnerability, what to expect if the vulnerability is accepted or
declined, etc.
