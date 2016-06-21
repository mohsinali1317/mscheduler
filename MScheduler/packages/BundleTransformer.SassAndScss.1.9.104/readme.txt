

   ----------------------------------------------------------------------
         README file for Bundle Transformer: Sass and SCSS v1.9.104

   ----------------------------------------------------------------------

      Copyright (c) 2012-2016 Andrey Taritsyn - http://www.taritsyn.ru


   ===========
   DESCRIPTION
   ===========
   BundleTransformer.SassAndScss contains translator-adapter
   `SassAndScssTranslator`. This adapter makes translation of Sass- and
   SCSS-code to CSS-code by using the LibSass Host for .NET
   (http://github.com/Taritsyn/LibSassHost) version 0.5.0 (supports
   libSass version 3.3.6). Also contains debugging HTTP-handler
   `SassAndScssAssetHandler`, which is responsible for text output of
   translated Sass- or SCSS-asset.

   For correct working of the LibSass Host require assemblies
   `msvcp120.dll` and `msvcr120.dll` from the Visual C++ Redistributable
   Packages for Visual Studio 2013.

   =============
   RELEASE NOTES
   =============
   1. Now supported paths beginning with a tilde in `@import` directives
   and `url()` functions;
   2. Added support of libSass version 3.3.6.

   ====================
   POST-INSTALL ACTIONS
   ====================
   If in your system does not assemblies `msvcp120.dll` and
   `msvcr120.dll`, then download and install the Visual C++
   Redistributable Packages for Visual Studio 2013
   (http://www.microsoft.com/en-us/download/details.aspx?id=40784).

   To use a debugging HTTP-handlers in the IIS Classic mode, you need add
   to the `/configuration/system.web/httpHandlers` element of the
   Web.config file a following code:

   <add
	path="*.sass" verb="GET"
	type="BundleTransformer.SassAndScss.HttpHandlers.SassAndScssAssetHandler, BundleTransformer.SassAndScss" />
   <add
	path="*.scss" verb="GET"
	type="BundleTransformer.SassAndScss.HttpHandlers.SassAndScssAssetHandler, BundleTransformer.SassAndScss" />

   =============
   DOCUMENTATION
   =============
   See documentation on CodePlex -
   http://bundletransformer.codeplex.com/documentation