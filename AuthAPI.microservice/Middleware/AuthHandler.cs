namespace AuthAPI.microservice.Middleware
{
    public class AuthHandler
    {
        private readonly RequestDelegate next;

        public AuthHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            var key = context.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(key) || !string.Equals(key, "KEY FROM DB"))
            {

            }

        }
    }
}
