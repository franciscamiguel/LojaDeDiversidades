# LojaDeDiversidades

LojaDeDiversidades é uma aplicação completa de e-commerce, composta por um backend em ASP.NET Core (.NET 8) e um frontend em Angular. O sistema permite cadastro de usuários, autenticação, gerenciamento de produtos, vendas e devoluções.

## Sumário

- [Visão Geral](#visão-geral)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Configuração do Ambiente](#configuração-do-ambiente)
- [Como Executar](#como-executar)
- [Testes](#testes)
- [APIs e Endpoints](#apis-e-endpoints)
- [Padrões e Boas Práticas](#padrões-e-boas-práticas)
- [Contribuição](#contribuição)
- [Licença] (#licença)

---

## Visão Geral

O projeto simula uma loja virtual, permitindo:

- Cadastro e autenticação de usuários (JWT)
- Gerenciamento de produtos (CRUD)
- Realização de vendas
- Devolução total ou parcial de vendas
- Controle de perfis (Administrador, Cliente)

## Tecnologias Utilizadas

### Backend

- ASP.NET Core 8 (Web API)
- Entity Framework Core (SQL Server)
- Autenticação JWT
- Versionamento de API

### Frontend

- Angular 18
- Bootstrap (estilização)
- RxJS

## Estrutura do Projeto

```text
LojaDeDiversidades/
├── src/
│   ├── LojaDeDiversidades.Server/      # Backend ASP.NET Core
│   ├── LojaDeDiversidades.Application/ # Camada de aplicação (DTOs, Services)
│   ├── LojaDeDiversidades.Domain/      # Domínio (Entidades, Interfaces)
│   ├── LojaDeDiversidades.Infra/       # Infraestrutura (DbContext, Repositórios)
│   └── lojadediversidades.client/      # Frontend Angular
└── README.md
```

## Configuração do Ambiente

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- SQL Server (local ou Docker)

### Configuração do Banco de Dados

1. Configure a string de conexão em `src/LojaDeDiversidades.Server/appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=LojaDiversidades;User Id=sa;Password=StrongPassword@123;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
   }
   ```

2. Execute as migrations (se aplicável) ou crie o banco manualmente.

## Como Executar

Abra o projeto no Visual Studio e execute. Será executado tanto o front quanto o back

Ou

### Backend (.NET)

```bash
cd src/LojaDeDiversidades.Server
dotnet restore
dotnet build
dotnet run
```

O backend estará disponível em `https://localhost:7110`.

### Frontend (Angular)

```bash
cd src/lojadediversidades.client
npm install
npm start
```

O frontend estará disponível em `https://localhost:62031` (ou conforme configuração do Angular CLI).

## Testes

### Back-end

Inclua testes unitários e de integração (caso existam) usando xUnit, NUnit ou MSTest.

### Front-end

Execute:

```bash
ng test
```

Os testes são executados via [Karma](https://karma-runner.github.io).

## APIs e Endpoints

### Usuários

- `POST /api/v1/usuarios` — Criação de usuário
- `GET /api/v1/usuarios` — Listar usuários (admin)
- `GET /api/v1/usuarios/{id}` — Detalhe do usuário

### Autenticação

- `POST /api/v1/auth/login` — Login (retorna JWT)

### Produtos

- `GET /api/v1/produtos` — Listar produtos
- `POST /api/v1/produtos` — Criar produto (admin)
- `PUT /api/v1/produtos/{id}` — Atualizar produto (admin)
- `DELETE /api/v1/produtos/{id}` — Remover produto (admin)

### Vendas

- `POST /api/v1/vendas` — Realizar venda
- `POST /api/v1/vendas/{id}/devolucao` — Devolução total
- `POST /api/v1/vendas/{id}/devolucao-parcial` — Devolução parcial

## Padrões e Boas Práticas

- Uso de DTOs para comunicação entre camadas
- Autenticação e autorização baseada em roles
- Separação de responsabilidades (Domain, Application, Infra, API)
- Variáveis de ambiente para URLs e segredos
- Validação de dados no frontend e backend
- Testes automatizados

## Contribuição

1. Faça um fork do projeto
2. Crie uma branch: `git checkout -b minha-feature`
3. Commit suas alterações: `git commit -m 'Minha feature'`
4. Push para o fork: `git push origin minha-feature`
5. Abra um Pull Request.
