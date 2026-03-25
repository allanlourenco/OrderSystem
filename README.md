# 🧾 Order System API

API de pedidos com controle de estoque, desenvolvida em .NET 8, com foco em **consistência de dados e concorrência**, simulando cenários reais de sistemas de alta demanda (como e-commerce e fintechs).

---

## 🧠 Principais conceitos aplicados

* Controle de concorrência otimista com RowVersion (EF Core)
* Tratamento de race conditions
* Retry automático em conflitos de concorrência
* Regras de negócio no domínio (DDD)
* Separação de camadas (Domain, Application, Infrastructure, API)
* Injeção de dependência

---

## 🏗️ Arquitetura

O projeto segue uma arquitetura em camadas:

```
OrderSystem
├── Domain
├── Application
├── Infrastructure
└── API
```

* **Domain**: regras de negócio e entidades
* **Application**: casos de uso
* **Infrastructure**: acesso a dados (EF Core / SQL Server)
* **API**: exposição via HTTP

---

## ⚙️ Tecnologias utilizadas

* .NET 8
* Entity Framework Core
* SQL Server (Docker)
* Docker Compose
* REST API

---

## 🐳 Subindo o ambiente

```bash
docker-compose up -d
```

---

## 🗄️ Rodando migrations

```bash
dotnet ef database update \
--project src/OrderSystem.Infrastructure \
--startup-project src/OrderSystem.Api
```

---

## ▶️ Executando a API

```bash
dotnet run --project src/OrderSystem.Api
```

---

## 🧪 Testando concorrência

Exemplo de teste com múltiplas requisições simultâneas:

```bash
seq 1 20 | xargs -I{} -P 10 curl -X POST "http://localhost:5218/api/orders?productId=SEU_ID&quantity=1"
```

### 🔥 Comportamento esperado

* Pedidos criados até o limite do estoque
* Rejeição quando estoque é insuficiente
* Conflitos de concorrência tratados corretamente
* Nenhuma inconsistência de dados

---

## 💡 Aprendizados

Este projeto explora problemas reais de backend, como:

* Consistência em ambientes concorrentes
* Estratégias de retry
* Importância do controle de concorrência
* Separação de responsabilidades

---

## 🚀 Próximos passos

* Implementar idempotência
* Adicionar logging estruturado
* Introduzir mensageria (RabbitMQ)
* Melhorar observabilidade

---

## 👨‍💻 Autor

Desenvolvido por Allan Lourenço
