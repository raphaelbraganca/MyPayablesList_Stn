# MyPayablesList_Stn

Projeto de API para controle de pagamentos utilizando formatação Json como retorno.

## Instalação

### Clonar

Clonar este reposiótio na máquina local utilizando:
```
$ git clone https://github.com/raphaelbraganca/MyPayablesList_Stn.git
```

### Pré-requisitos

Instalar os seguintes pacotes de referência:

```
$ dotnet add package dotnet tool install --global dotnet-ef --version 3.0.0

$ dotnet add package Microsoft.EntityFrameworkCore.Design

$ dotnet add package Microsoft.EntityFrameworkCore.InMemory

$ dotnet add package Microsoft.EntityFrameworkCore.Tools

$ dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design

$ dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Tools

$ dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

$ dotnet add package Swashbuckle.AspNetCore --version 5.0.0-rc4

```

## Executando testes

Os testes são feitos com o intuito de garantir a obtenção dos retornos e a usabilidade da API.
Após a solução buildar e inicializar, é possível executar os comandos de teste utilizando Swagger ou via Postman.

Exemplos de input:

  Cadastrar Estabelecimentos

<img src="https://i.imgur.com/gDv1kwx.png">

https://localhost:porta/api/organizacao
```
Post
{
  "orgNome": "Nome do Estabelecimento",
  "orgCategoria": "Categoria do Estabelecimento"
}
```

  Consultar Estabelecimentos

<img src="https://i.imgur.com/fW3UxS8.png">

https://localhost:porta/api/organizacao
```
Get
{
}
```

  Cadastrar Lançamentos

<img src="https://i.imgur.com/ZAAKS3J.png">

https://localhost:porta/api/lancamento/
```
Post
{
  "lanOrgOrganizacaoId": "Token UUID do Estabelecimento",
	"lanFormaPagamento": "Debito / Credito",
	"lanValorLancamento": "Valor decimal",
	"lanDataLancamento": "DATA: AAAA-MM-DD"
}
```

  Consultar Lançamentos filtrados por período e agrupados por Categoria

<img src="https://i.imgur.com/UYeUCzZ.png">

https://localhost:porta/api/lancamento/grupo?dataInicio="DATA: AAAA-MM-DD"&dataFim="DATA: AAAA-MM-DD"
```
Get
{
}
```

## Ferramentas utilizadas

* [C#] - Linguagem de desenvolvimento
* [.NetCore 3.0] - Framework
* [Google Cloud SQL] - Hospedagem e manutenção
* [PostegreSQL] - Banco de dados relacional
* [EntityFrameworkCore] - Relacionamento de entidades em memória
* [Postman] - Teste de requisições
* [Swagger] - Documentação da API

## Autor

* **Raphael Tavares** - *Trabalho inicial* - [raphaelbraganca](https://github.com/raphaelbraganca/)

## Licença

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
