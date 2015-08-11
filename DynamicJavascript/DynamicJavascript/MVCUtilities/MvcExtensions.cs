using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DynamicJavascript.Exceptions;

namespace DynamicJavascript.MVCUtilities
{
    public static class MvcExtensions
    {
        public static ActionResult JavascriptFromView(this Controller controller, string javascriptViewName, object model)
        {
            string script = string.Empty;

            try
            {
                script = ParseViewToContent(controller, javascriptViewName, model);
            }
            catch (DynamicJavascriptException parseViewException)
            {
                throw new HttpException(500, "internal error");
            }

            return new JavaScriptResult
            {
                Script = script
            };
        }

        private static string ParseViewToContent(Controller controller, string viewName, object model)
        {
            if (controller == null || string.IsNullOrEmpty(viewName) == true)
            {
                throw new DynamicJavascriptException(ErrorCode.MISSING_PARAMETER);
            }

            if (model != null)
            {
                controller.ViewData.Model = model;
            }

            // Find the File
            // ControllerContext: Encapsulates information about an HTTP request that matches specified RouteBase and ControllerBase instances.
            // We use FindPartialView instead of FindView because it doesn't need to specify a view master name
            var viewEngineResult = controller.ViewEngineCollection.FindPartialView(controller.ControllerContext, viewName);
            if (viewEngineResult.View == null)
            {
                throw new DynamicJavascriptException(ErrorCode.VIEW_NOT_FOUND);
            }

            using (var viewContentWriter = new StringWriter())
            {
                // The context of the view:
                // controllerContext : Encapsulates information about the HTTP request.
                // view : The view to render.
                // viewData : The dictionary that contains the data that is required in order to render the view.
                // tempData : The dictionary that contains temporary data for the view.
                // writer : The text writer object that is used to write HTML output.
                var viewContext = new ViewContext(controller.ControllerContext,
                    viewEngineResult.View,
                    controller.ViewData,
                    controller.TempData,
                    viewContentWriter
                );

                // Make the rendering
                viewEngineResult.View.Render(viewContext, viewContentWriter);
                var viewAsStringResult = viewContentWriter.ToString().Trim('\r', '\n', ' ');

                // Isolate the script part in the file
                var regex = "<script[^>]*>(.*?)</script>";
                var result = Regex.Match(viewAsStringResult,
                    regex, RegexOptions.IgnoreCase
                    | RegexOptions.IgnorePatternWhitespace
                    | RegexOptions.Multiline
                    | RegexOptions.Singleline
                );

                if (result.Success == false && result.Groups.Count < 2)
                {
                    throw new DynamicJavascriptException(ErrorCode.SCRIPT_TAG_NOT_FOUND);
                }

                return result.Groups[1].Value;
            }
        }
    }
}