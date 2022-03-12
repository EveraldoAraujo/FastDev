# FastDev

É um conjunto de bibliotecas para acelerar o desenvolvimento de contextos orientados a dados, abstraindo toda a estrutura de processos básicos de crud(podendo omitir rotas indesejadas), alem de filtragem de campos no objeto de retorno;


### Como usar isso?

#### Criando as models

Instale o pacote

```shell
dotnet add package FastDev.Infra.Data
```

Import namespace

```csharp
using FastDev.Infra.Data.Model;
```

Sua model deve herdar de DataModel e especificar o tipo do campo id, que deve ser uma struct como Int32 e Guid por exemplo.

```csharp
public class User : DataModel<Guid>
{
    public string Email { get; set; }
    public string Name { get; set; }
}
```

```
Nota: Se sua aplicação usa Entity Framework não se esqueça de adicionar sua model ao DbContext!
e adicionar a dependencia para essa estrutura
```
```shell
dotnet add package FastDev.Infra.Data.EntityFrameworkCore
```

#### Criando as controllers

Instale o pacote

```shell
dotnet add package FastDev.WebApi
```

Import namespace

```csharp
using FastDev.WebApi.Controllers;
```

Sua controller deve herdar de BaseController e especificar o tipo da model e o tipo do campo id.

``` csharp
public class UsersController : BaseController<User, Guid>
```

### Já posso dar start no debug?

Calma jovem gafanhoto, não está esquecendo de nada?

#### Hora de registrar as dependências

Aqui precisamos de mais uma dependência

```shell
dotnet add package FastDev.Service
```

```csharp

using FastDev.Service;

....

//registra as dependencias genericas de serviços.
builder.Services.AddScoped(typeof(IServiceBase<,>), typeof(ServiceBase<,>));
```

##### Caso sua aplicação use Entity Framework

```csharp
using FastDev.Infra.Data;
using FastDev.Infra.Data.EntityFrameworkCore;

....

/*
FastDev utiliza o componente padrão de injeção de dependencia do asp.net.
veja mais detalhes sobre injeçã de dependencia em 
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
*/

/*
provavelmente você ja configurou a injeção do contexto de dados, 
caso não tenha feito podera encontrar mais detalhes em
https://docs.microsoft.com/pt-br/ef/core/dbcontext-configuration
*/
builder.Services.AddDbContext<YourDbContext>(o => o.UseInMemoryDatabase("FastDevSampleDB"));

//registra o contexto de dados da sua aplicação como implementação para toda solicitação de DbContext(utilizado para controle da unidade de trabalho).
builder.Services.AddScoped<DbContext, YourDbContext>();
// registra a dependencia para controle da unidade de trabalho.
builder.Services.AddScoped<IUoW, UoW>();

//registra as dependencias geníricas de repositórios.
builder.Services.AddTransient(typeof(IRepositoryBase<,>), typeof(FastDev.Infra.Data.EntityFrameworkCore.RepositoryBase<,>));
```

##### E se minha aplicação usar outra estrutura?

Mais estruturas serão adicionadas no futuro. Suporte a mongoDB é uma prioridade e já está na fila de desenvolvimento.

Caso precise urgente, como usamos um modelo de projeto totalmente desacoplado, você pode dar uma olhada no design do pacote ***FastDev.Infra.Data.EntityFrameworkCore*** e criar sua propria estrutura.
Não se esqueça de mandar uma pull request.

Em breve teremos mais detalhes sobre o design para um novo pacote de extensão.

### Pronto! Agora pode dar start.

Você terá uma api com essa estrutura, pronta pra uso. <br/>
<img src="doc\files\imgs\samplecrudapi.png" style="width:380px; border-radius:5px">

Agora é só testar suas rotas.
Vai lá!

### O que mais tem ai?

É possivel personalizar sua controler e para isso passaremos um objeto de configuração no construtor da controler

```csharp
public UsersController() : base(config => ... é aqui que a mágica acontece ...)
{

}
```
```
É possivel encadear todas as chamadas das funções do objeto de configuração.
```

#### Você pode remover uma rota

Você pode usar as funções ommit para isso.

```csharp
public UsersController() : base(config => config.OmmitPatch())
{}
```

```csharp
public UsersController() : base(config => config.OmmitPatch()
                                                .OmmitGetById()
){}
```

```
Nesse caso uma solicitação a esses recursos omitidos retornara - 404 NotFound
```

#### Você pode permitir a especificação dos campos do objeto de retorno.
Você pode usar as funções UsePostReturnFields, UseGetFields e UseGetByIdFields para habilitar a composição nas requisições Post, Get e GetById.

```csharp
public UsersController() 
: base(config => config.UsePostReturnFields()
                       .UseGetFields()
                       .UseGetByIdFields()
){}
```

```
Nesse caso de estarem desabilitados, uma solicitação a esses recursos passando a query string com os campos retornara - 404 NotFound
```

#### Você pode aplicar regras aos processos

Geralmente precisamos fazer algumas verificações para não permitir coisas como: salvar uma entidade com valor duplicado em um campo, por exemplo.

Para esse fim podemos escrever funções que façam essas validações e usar as funções CheckRulesOnPost, CheckRulesOnPut, CheckRulesOnDelete para adiciona-las no processo.

##### Criando a validação

```csharp
using FastDev.Infra.Data;
using FastDev.Notifications;
using FastDev.Notifications.Interfaces;

namespace MyApp;
public static class MyValidation
{
    public static async Task<INotification?> ShouldNotExistWithTheSameEmail(HttpContext context, User user)
    {
        var repository = context.RequestServices.GetService<IRepositoryBase<User, Guid>>();
        if ((await repository!.GetAllAsync()).Any(u => u.Email == user.Email)) 

            return new Notification("Já existe um usuário com esse email");

        return null;
    }
}
```

```
Note que o tipo de retorno da função é um objeto do tipo INotification. 
```
```
A função recebe o objeto de contexto da requisição em tempo de execução dando acesso a todas as features exatamente como se estivesse dentro da controller.
```

##### Adicionando a validação ao processo
```csharp
using static MyApp.MyValidation;

public UsersController() 
: base(config => config.CheckRulesOnPost(
                    ShouldNotExistWithTheSameEmail,
                    .....  another rules  .....
                ))
){}
```
