# ASP.NET-MVC-Dynamic-Javascript
This project shows how to inject an MVC Model into a javascript file.
The javascript is no longer a static file.

This project uses:
- a server that creates the script. (DynamicJavascript)
- a client that loads the script and executes a function of the script. (DynamicJSClient)

The most interesting part is the class DynamicJavascript.MVCUtilities.MvcExtensions in the server project.
