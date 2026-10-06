# APICatalogo

API REST de catálogo de produtos e categorias, feita com ASP.NET Core 5, controllers e Entity Framework Core sobre MySQL.

> Projeto de estudo (março/2022). Foi a primeira de uma série de APIs de catálogo; as versões seguintes são o [CatalogAPI](https://github.com/Gipsy7/CatalogAPI) (.NET 6, async) e o [CatalogAPI2](https://github.com/Gipsy7/CatalogAPI2) (Minimal API + JWT).

## O que tem

- CRUD completo de **categorias** e **produtos** (`GET`, `POST`, `PUT`, `DELETE`)
- Endpoint que traz cada categoria com seus produtos (`/api/categories/GetCategoryWithProducts`)
- Relacionamento 1:N categoria → produtos, com validação por data annotations
- Migrations do EF Core, incluindo uma que popula o banco com dados iniciais
- Tratamento de erros com mensagens em português e `ReferenceHandler.Preserve` para evitar ciclos na serialização
- Swagger no ambiente de desenvolvimento

## Endpoints

| Método | Rota | Descrição |
| --- | --- | --- |
| GET | `/api/categories` | Lista as categorias |
| GET | `/api/categories/GetCategoryWithProducts` | Lista as categorias com os produtos |
| GET | `/api/categories/{id}` | Busca uma categoria |
| POST | `/api/categories` | Cria uma categoria |
| PUT | `/api/categories/{id}` | Atualiza uma categoria |
| DELETE | `/api/categories/{id}` | Remove uma categoria |
| GET | `/api/products` | Lista os produtos |
| GET | `/api/products/{id}` | Busca um produto |
| POST | `/api/products` | Cria um produto |
| PUT | `/api/products/{id}` | Atualiza um produto |
| DELETE | `/api/products/{id}` | Remove um produto |

## Tecnologias

- .NET 5 / ASP.NET Core (controllers + `Startup`)
- Entity Framework Core 5 com Pomelo (MySQL)
- Swashbuckle (Swagger)

## Como executar

Pré-requisitos: SDK do .NET 5 e um MySQL rodando.

1. Ajuste a connection string `DefaultConnection` em `APICatalogo/appsettings.json`.
2. Crie o banco a partir das migrations:
   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef database update --project APICatalogo
   ```
3. Rode a API e abra o Swagger em `/swagger`:
   ```bash
   dotnet run --project APICatalogo
   ```

---

Feito por **Mikael Francisco** · [Portfólio](https://mikaelfrancisco.vercel.app) · [LinkedIn](https://www.linkedin.com/in/mikael-francisco-a4300b180)
