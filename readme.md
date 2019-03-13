## MS BUILD
MS Build usually installed with the visual studio

 - Visual Studio MSBuild Location:   `C:\program files (x86)\ micrisoft visual studio\[date of build]\ [Version]\msbuild\[msbuild version]\bin`
 - Or: `c:\windows\microsoft.net\framework\[version]\msbuild.exe`

## Usual Flow
```mermaid
graph LR
A[Application Development] -- Built Each iteration --> B(Push To Repo like Git )
B --> D((CI/CD i.e. Jenkins))
D --> E{Automatic Build}
E  -- Build --> F[Successful Build]
F  --> H
T[Test Development] -- Built Each iteration --> B(Push To Repo like Git )
T -- If Build Successful, Test Build --> H(Test Execution)

## Framework

### Configuration

 Reading user defined configuration from app.config in ConfigurationReader.cs 
