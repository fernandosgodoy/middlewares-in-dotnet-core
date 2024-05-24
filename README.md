# Entendendo Middlewares no ASP.NET Core e suas aplicações

No desenvolvimento de aplicações web com ASP.NET Core, um conceito fundamental é o middleware. Este exemplo que disponibilizo é parte do artigo disponível em meu website e se quiser, o link está aqui => <a href="https://f78m.short.gy/eFUBTg">https://f78m.short.gy/eFUBTg</a>.

## O Que São Middlewares?

Middlewares são componentes que compõem o pipeline de requisição de uma aplicação ASP.NET Core. Eles processam requisições HTTP e podem executar ações antes e depois que outros middlewares no pipeline sejam executados.

### Funcionamento dos Middlewares

Quando uma requisição chega à aplicação, ela passa por uma série de middlewares registrados no pipeline. Cada middleware pode:

- Processar a requisição.
- Passar a requisição para o próximo middleware no pipeline.
- Manipular a resposta.
  
Esta abordagem permite um processamento modular e flexível de requisições.

### Pipeline de Requisição

O pipeline de requisição é configurado no método Configure da classe Startup. Middlewares são adicionados usando métodos de extensão fornecidos pelo ASP.NET Core.

Veja o snippet:

`
public class Startup
{
    public void Configure(IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            // Lógica antes do próximo middleware
            await next.Invoke();
            // Lógica após o próximo middleware
        });

        app.Run(async (context) =>
        {
            await context.Response.WriteAsync("Hello, World!");
        });
    }
}
`
No exemplo acima, um middleware é adicionado ao pipeline usando app.Use. Este middleware executa uma lógica antes e depois do próximo middleware, que é configurado usando app.Run.

### Tipos de Middlewares

O ASP.NET Core vem com vários middlewares integrados para tarefas comuns, como roteamento, autenticação, autorização, logging e compressão de respostas. Exemplos incluem:

- UseRouting()
- UseAuthentication()
- UseAuthorization()
- UseEndpoints()
- UseStaticFiles()

### Middlewares Personalizados

Você pode criar middlewares personalizados para atender a necessidades específicas da sua aplicação. Um middleware personalizado é uma classe que segue um padrão específico: deve ter um método Invoke ou InvokeAsync que processa a requisição.

`
public class CustomMiddleware
{
    private readonly RequestDelegate _next;

    public CustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Lógica antes do próximo middleware
        if (context.Request.Path == "/custom")
        {
            await context.Response.WriteAsync("This is a custom middleware response.");
            return;
        }

        await _next(context); // Chama o próximo middleware
        // Lógica após o próximo middleware
    }
}

public static class CustomMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomMiddleware>();
    }
}
`

Para registrar o middleware no pipeline:

`
public void Configure(IApplicationBuilder app)
{
    app.UseCustomMiddleware();
    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/", async context =>
        {
            await context.Response.WriteAsync("Hello, World!");
        });
    });
}
`

No exemplo que está disponível aqui no Git, poderá verificar as requisições do TimingMiddleware sendo chamadas a cada request, como demonstrado na imagem abaixo:
![image](https://github.com/fernandosgodoy/middlewares-in-dotnet-core/assets/1747058/3efe71d3-1a70-44ff-9d5a-c4e8b4b16533)


