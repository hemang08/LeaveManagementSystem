using LeaveManagementSystem.Common.CommonMethod;
using LeaveManagementSystem.Common.Helpers;
using LeaveManagementSystem.Model.ReqResponse;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace LeaveManagementSystem.API.Middleware
{
    public class CustomMiddleware
    {
        #region Fields
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public CustomMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor,
            IWebHostEnvironment hostingEnvironment,IConfiguration config)
        {
            _next = next;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        /// <summary>
        /// Invoke on every request response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="settingService"></param>
        public async Task Invoke(HttpContext context)
        {
            using (MemoryStream requestBodyStream = new MemoryStream())
            {
                using (MemoryStream responseBodyStream = new MemoryStream())
                {
                    Stream originalRequestBody = context.Request.Body;
                    Stream originalResponseBody = context.Response.Body;
                    try
                    {
                        await context.Request.Body.CopyToAsync(requestBodyStream);
                        requestBodyStream.Seek(0, SeekOrigin.Begin);

                        string requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();

                        requestBodyStream.Seek(0, SeekOrigin.Begin);
                        context.Request.Body = requestBodyStream;

                        string responseBody = "";

                        context.Response.Body = responseBodyStream;

                        Stopwatch watch = Stopwatch.StartNew();
                        await _next(context);
                        watch.Stop();

                        responseBodyStream.Seek(0, SeekOrigin.Begin);
                        responseBody = new StreamReader(responseBodyStream).ReadToEnd();

                        AddRequestLogsToLoggerFile(context, requestBodyText, responseBody);

                        responseBodyStream.Seek(0, SeekOrigin.Begin);

                        await responseBodyStream.CopyToAsync(originalResponseBody);
                    }
                    catch (Exception ex)
                    {
                        await context.Request.Body.CopyToAsync(requestBodyStream);
                        requestBodyStream.Seek(0, SeekOrigin.Begin);

                        string requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();

                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        byte[] data = Encoding.UTF8.GetBytes(new BaseApiResponse()
                        {
                            Success = false,
                            Message = ex.Message
                        }.ToString());

                        originalResponseBody.WriteAsync(data, 0, data.Length);
                        var url = context.Request.GetDisplayUrl();
                        AddExceptionLogsToLoggerFile(context, ex, requestBodyText);
                        return;
                    }
                    finally
                    {
                        context.Request.Body = originalRequestBody;
                        context.Response.Body = originalResponseBody;
                    }
                }
            }
        }

        /// <summary>
        /// Store ReqRes in Logger Exception file
        /// </summary>
        private void AddRequestLogsToLoggerFile(HttpContext context, string requestBodyText, string responseBody)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            try
            {
                ParamValue paramValues = CommonMethods.GetKeyValues(_httpContextAccessor.HttpContext);

                StringBuilder sb = new StringBuilder();

                sb.Append(Environment.NewLine +
                          Environment.NewLine + "--------------------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " ---------------------------" +
                          Environment.NewLine + "Requested URL: " + context.Request.Path.Value +
                          Environment.NewLine + "Request Body: " + requestBodyText +
                          Environment.NewLine + "Query String Params: " + paramValues.QueryStringValue +
                          Environment.NewLine + "Response: " + responseBody +
                          Environment.NewLine + "Status Code: " + context.Response.StatusCode +
                          Environment.NewLine);
                logger.Info(sb.ToString());
                DeleteOldReqResLogFiles();
            }
            catch (Exception ex)
            {
                logger.Error("Exception in AddRequestLogsToLoggerFile: ", ex.Message.ToString());
            }
        }

        /// <summary>
        /// Store exception in Logger Exception file
        /// </summary>
        private void AddExceptionLogsToLoggerFile(HttpContext context, Exception ex, string requestBody)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            var paramValues = JsonConvert.DeserializeObject(requestBody);
            if (paramValues == null)
            {
                paramValues = ErrorMessages.NoParametersPassed;
            }

            StringBuilder sb = new StringBuilder();

            sb.Append(Environment.NewLine +
                      Environment.NewLine + "--------------------- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " ---------------------------" +
                      Environment.NewLine + "Requested URL: " + context.Request.Path.Value +
                      Environment.NewLine + "Request Params: " + requestBody +
                      Environment.NewLine + "Query String Params: " + paramValues +
                      Environment.NewLine + "Status Code: " + context.Response.StatusCode +
                      Environment.NewLine + "Exception: " + ex.Message +
                      Environment.NewLine + "Exception: " + ex.InnerException +
                      Environment.NewLine);
            logger.Error(sb.ToString());
            DeleteOldExceptionLogFiles();
        }

        ///// <summary>
        ///// Delete files from ReqRes logs folder which is older than 7 days.
        ///// </summary>
        public void DeleteOldReqResLogFiles()
        {
            var directoryPath = Path.Combine(_hostingEnvironment.ContentRootPath, Constants.LogsFilePathRequest);
            string[] Dfiles = Directory.GetFiles(directoryPath);

            foreach (string file in Dfiles)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.LastAccessTime < DateTime.Now.AddDays(-7))
                {
                    fi.Delete();
                }
            }
        }

        ///// <summary>
        ///// Delete files from error logs folder which is older than 7 days.
        ///// </summary>
        public void DeleteOldExceptionLogFiles()
        {
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, Constants.LogsFilePathException);

            string[] files = Directory.GetFiles(filePath);

            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.LastAccessTime < DateTime.Now.AddDays(-7))
                {
                    fi.Delete();
                }
            }
        }
    }
}