namespace EShop.Middleware.CustomExceptionMiddleware
{
     public static class ExceptionHandlerMiddlewareExtensions  
     {  
        public static void UseExceptionHandlerMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
