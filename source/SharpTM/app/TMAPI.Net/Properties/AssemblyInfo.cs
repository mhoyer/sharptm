using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Resources;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("TMAPI.NET")]
[assembly: AssemblyDescription(".NET version of TMAPI")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("TMAPI.NET")]
[assembly: AssemblyCopyright("Copyright © Stefan Kesberg, Ralf Eilbracht, Marcel Hoyer 2008")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("42a4da61-d469-472b-bfa9-126e9f5f00ec")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("2.0.0.0")]
[assembly: AssemblyFileVersion("2.0.0.0")]

[assembly: NeutralResourcesLanguageAttribute("en")]
[assembly: CLSCompliant(true)]

// Some global suppressions that does not fit the TMAPI naming conventions.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TMAPI")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TMAPI", Scope = "namespace", Target = "TMAPI.Net.Core")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TMAPI", Scope = "namespace", Target = "TMAPI.Net.Index")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TMAPI", Scope = "type", Target = "TMAPI.Net.Core.TMAPIException")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TMAPI", Scope = "type", Target = "TMAPI.Net.Core.TMAPIRuntimeException")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "TMAPI.Net.Index", Justification = "TMAPI.Net.Index just does not contain more types. See also: TMAPI 2.0.")]

