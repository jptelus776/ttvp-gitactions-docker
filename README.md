# entity-info-ms

# Dependencies

## Libraries

| Nuget Package | Version | License |
|:== |:== |
| [CassandraCSharpDriver](https://www.nuget.org/packages/CassandraCSharpDriver/3.19.3)	| 3.19.3 | [Apache 2.0](https://www.nuget.org/packages/CassandraCSharpDriver/3.19.3/License) |
| [MySql.Data](https://www.nuget.org/packages/MySql.Data/6.10.9) | 6.10.9 | [GNU 2.0](https://www.gnu.org/licenses/old-licenses/gpl-2.0.html) |
| [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/13.0.3) | 13.0.3 | [MIT](https://licenses.nuget.org/MIT) |
| [Serilog.AspNetCore](https://www.nuget.org/packages/Serilog.AspNetCore/7.0.0) | 7.0.0 | [Apache 2.0](https://licenses.nuget.org/Apache-2.0) |
| [Serilog.Enrichers.AssemblyName](https://www.nuget.org/packages/Serilog.Enrichers.AssemblyName/1.0.9) | 1.0.9 | [Apache 2.0](https://www.apache.org/licenses/LICENSE-2.0) |
| [Serilog.Enrichers.Context](https://www.nuget.org/packages/Serilog.Enrichers.Context/4.6.5) | 4.6.5 | [Apache 2.0](https://www.nuget.org/packages/Serilog.Enrichers.Context/4.6.5/License) |
| [Serilog.Enrichers.Environment](https://www.nuget.org/packages/Serilog.Enrichers.Environment) | 2.2.0 | [Apache 2.0](https://licenses.nuget.org/Apache-2.0) |
| [Serilog.Enrichers.Process](https://www.nuget.org/packages/Serilog.Enrichers.Process/2.0.2) | 2.0.2 | [Apache 2.0](https://licenses.nuget.org/Apache-2.0) |
| [Serilog.Enrichers.Thread](https://www.nuget.org/packages/Serilog.Enrichers.Thread/3.1.0) | 3.1.0 | [Apache 2.0](https://www.apache.org/licenses/LICENSE-2.0) |
| [Serilog.Expressions](https://www.nuget.org/packages/Serilog.Expressions/4.0.0-dev-00139) | 3.4.1 | [Apache 2.0](https://licenses.nuget.org/Apache-2.0) |
| [Serilog.Sinks.Seq](https://www.nuget.org/packages/Serilog.Sinks.Seq/5.2.2) | 5.2.2 | [Apache 2.0](https://licenses.nuget.org/Apache-2.0) |

## Certificates

If `CA_USE_SSL` environment variable is set to true, 

- Certificate for connecting to Cassandra must be added to a file location `/opt/certs/` folder.
- `CA_CERT_PATH` environment variable must be set to location of the certificate including certification name.
  - The cert should be a .pem file.
