// <copyright file="SharedAssemblyInfo.cs" company="Pixelplastic">
// Copyright (©) Marcel Hoyer 2009. All rights reserved.
// </copyright>
// <author>Marcel Hoyer</author>
// <email>mhoyer AT pixelplastic DOT de</email>

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Pixelplastic")]
[assembly: AssemblyProduct("SharpTM")]
[assembly: AssemblyCopyright("Copyright © Marcel Hoyer 2009")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision

[assembly: AssemblyFileVersion("1.1.0.0")]
[assembly: AssemblyVersion("1.1.0.0")]

[assembly: NeutralResourcesLanguageAttribute("en")]
[assembly: CLSCompliant(true)]
#if LOG4NET
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
#endif
