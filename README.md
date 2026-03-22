# 🚀 Desafio Técnico - Campos Dealer

## 📌 Objetivo

Evoluir uma API de gestão de vendas garantindo rastreabilidade, escalabilidade e organização.

---

## 🧠 Arquitetura

* Controller → camada de entrada (REST)
* Service → regras de negócio
* Repository → acesso a dados (LINQ to SQL)

Aplicação dos princípios SOLID:

* SRP: separação de responsabilidades
* DIP: uso de interfaces
* OCP: estrutura extensível

---

## 🧱 Modelagem

* Cliente
* Produto
* Venda
* VendaItem
* ProdutoPrecoHistorico

---

## 🔥 Regras de Negócio

* Venda deve conter ao menos 1 item
* Valor do item = Quantidade * ValorUnitário
* Valor total da venda = soma dos itens
* Valor unitário é fixado no momento da venda
* Histórico de preço é registrado

---

## 🌐 Endpoints

* GET /api/venda
* POST /api/venda
* GET /api/venda/{id}
* GET /api/venda/cliente/{id}
* GET /api/venda/top/{top}
* DELETE /api/venda/{id}

## 🔐 Segurança

* Header obrigatório: `X-API-KEY`
* Chave configurável em `Web.config` (`appSettings: ApiKey`)

## 🧭 Swagger

Após restaurar os pacotes NuGet, o Swagger estará disponível em:

* `https://localhost:44302/swagger/ui/index`

Ele complementa a coleção Postman, oferecendo documentação interativa da API.

Para autenticar as chamadas:

1. Clique em **Authorize**
2. Preencha `X-API-KEY` com o valor do `Web.config`
3. Execute os endpoints normalmente

---

## 🖥️ Frontend

Projeto ASP.NET MVC com:

* Criação de vendas
* Listagem
* Visualização

Rotas simples:

* `/ClienteMvc` (CRUD de clientes)
* `/ProdutoMvc` (CRUD de produtos)
* `/VendaMvc` (CRUD de vendas)

Rotas curtas (alternativas):

* `/cliente`
* `/produto`
* `/venda`

---

## 🐳 Banco de Dados

Executar via Docker:

1. Build da imagem
2. Executar container
3. Rodar script restore.sql

### Docker Compose (SQL Server)

1. Abra o terminal na pasta `API/docker`
2. Suba os serviços:
   `docker compose up -d`
3. Aguarde a restauração (o serviço `db-restore` aplica `restore.sql` e `pos_restore.sql`)
4. String de conexão usada no projeto:
   `Data Source=localhost,1433;Initial Catalog=TesteCamposDealer;User ID=sa;Password=YourStrong@Pass123;TrustServerCertificate=True;Encrypt=False`

---

## 🧪 Testes

Coleção Postman disponível em /docs
Arquivo: `docs/CamposDealer.postman_collection.json`

Projeto de testes:

* `Tests/TesteCamposDealer.Tests.csproj` (xUnit)

---

## 📌 Histórias de Usuário

* Como usuário, quero registrar uma venda com múltiplos produtos
* Como usuário, quero consultar vendas por cliente
* Como gestor, quero visualizar ranking de vendas

---

## ⚙️ Tecnologias

* .NET
* LINQ to SQL
* SQL Server
* ASP.NET MVC
* Docker

---

## ✅ O Que Foi Adicionado e Por Quê

* **API Key** via header `X-API-KEY` para segurança simples e auditável.
* **Transações** em criação de venda e atualização de preço para garantir consistência.
* **VendaItem + ProdutoPrecoHistorico** com mapeamento LINQ to SQL para rastreabilidade.
* **DTOs de retorno** para garantir resposta com dados persistidos (incluindo itens).
* **CRUD MVC** para Produtos e Vendas (rotas `/ProdutoMvc` e `/VendaMvc`).
* **UI do painel e CRUDs** com layout simples e amigável para navegação.
* **Datas automáticas**: `dthRegistro` recebe `GETDATE()` no banco e `DateTime.Now` no código.
* **Testes xUnit** separados em projeto de testes para validar regra de total.
* **Postman Collection** para facilitar validação manual dos endpoints.

## 🧩 Boas Práticas e Padrões

* **Repository + Service** para separar acesso a dados e regra de negócio.
* **DIP** com injeção de dependência via `DependencyResolver`.
* **Factory simples** no `DependencyConfig` para criação de serviços.
* **Validações e exceções** com tipos específicos (404/400).

Optamos por não adicionar padrões mais pesados (ex: Singleton/Strategy/Adapter) para manter o projeto simples e alinhado ao escopo do teste.
