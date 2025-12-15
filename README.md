# Sistema para Formulários de Eleição

## 1. APRESENTAÇÃO DO PROJETO
Este projeto constitui-se em um sistema robusto para a gestão de formulários de pesquisa única eleitoral.

O desenvolvimento foi realizado com base na estrutura .NET Core 9, empregando rigorosamente os princípios do Domain-Driven Design (DDD). Essa abordagem arquitetural garantiu um design de software focado na complexidade do domínio, resultando em um código modular, escalável e de fácil manutenção.

O produto final é uma API RESTful que oferece as seguintes funcionalidades primárias:
- Gerenciamento Completo de Perguntas: Implementação de um CRUD (Create, Read, Update, Delete) integral para o cadastro e manutenção de perguntas da pesquisa.
- Cadastro de Formulários de Pesquisa: Funcionalidade para o registro e estruturação dos formulários, utilizando as perguntas previamente cadastradas.

Para fins de prototipagem e desenvolvimento inicial, o sistema utiliza um banco de dados em memória para persistência dos dados.

### Objetivo Principal e Escopo
O objetivo primordial e o escopo deste projeto concentram-se na aplicação prática e consolidação dos conhecimentos arquiteturais adquiridos.
Busca-se o desenvolvimento de uma solução arquitetural robusta e inteligente para o sistema de pesquisas eleitorais, com foco nos seguintes pilares:

- 1. Arquitetura e Modelagem de Domínio
    - Domain-Driven Design (DDD): Aplicar integralmente os conceitos do DDD para estabelecer um Domínio Rico. Isso envolve a criação de Aggregates bem definidos, Entities e Value Objects que reflitam com precisão as regras e complexidades do negócio eleitoral.
    - Solução Arquitetural: Implementar um design que promova alta coesão e baixo acoplamento, resultando em um sistema escalável, testável e manutenível.

- 2. Ferramenta Principal
    - Entity Framework Core (EF Core): Utilizar o EF Core como a principal ferramenta de Object-Relational Mapping (ORM). Sua aplicação será fundamental para a persistência eficiente dos dados, mapeando as classes do domínio para o banco de dados e gerenciando transações.

Este escopo está alinhado com a busca por uma solução de software de alta qualidade técnica e aderência rigorosa às melhores práticas de desenvolvimento.

### O que o Projeto Faz
O projeto permite:
- **Cadastrar perguntas** informando o seu número e texto relacionado a ela.
- **Consultar perguntas** tanto de forma induvidual pelo seu `GUID`, `Número` ou uma lista completa delas
- **Atualizar dados da pergunta**, somente seu texto, como também ela toda (desde o número ao texto).
- **Remover perguntas** do sistema.
- **Cadastrar Formulários** informando os dados do entrevistado, candidato e as perguntas que foram respondidas.
- **Consultar Formulários** uma lista completa de todas as perguntas que foram cadastradas no sistema.
> É crucial ressaltar que **não há possibilidade de atualização (*Update*) nos formulários de pesquisa**. Uma vez que o formulário é registrado, ele é considerado uma **resposta única e imutável**. Seu estado persiste permanentemente como um registro histórico e validado da pesquisa.

## 2. ARQUITETURA E DESIGN

### Justificativa das Decisões Arquiteturais

A arquitetura do projeto segue o padrão de **Arquitetura em Camadas (Layered Architecture)**, com forte influência do **Domain-Driven Design (DDD)**. Isso garante uma separação clara de responsabilidades, facilitando a manutenção, testabilidade e escalabilidade da aplicação.

A utilização da plataforma .NET foi um pré-requisito do projeto, definido pela startup devido ao domínio prévio da equipe em C# e .NET, o que reduziu a curva de aprendizado, os riscos de entrega e aumentou a previsibilidade do prazo, fator crítico considerando o calendário eleitoral.

O .NET 9 foi escolhido por ser a versão estável, oferecendo melhorias de desempenho, segurança e suporte a aplicações com alto volume de acessos simultâneos, garantindo escalabilidade e longevidade à solução.

O Domain-Driven Design (DDD) foi adotado para modelar o sistema de forma alinhada ao domínio de pesquisas eleitorais, assegurando clareza nas regras de negócio, organização do código e facilidade de evolução futura.

O Entity Framework Core foi escolhido por ser o ORM oficial do .NET, amplamente conhecido pela equipe, integrado ao ASP.NET Core e adequado ao prazo do projeto, proporcionando produtividade, facilidade de manutenção e suporte a banco de dados em memória.

### Explicação Completa da Arquitetura em Camadas

#### 1. **FormularioEleicao.Dominio (Camada de Domínio)**
- **Coração da aplicação.** Contém a lógica de negócios, entidades, Value Objects, agregados e interfaces de repositório.
- **Independente de qualquer tecnologia de infraestrutura ou UI.** Não conhece banco de dados, frameworks web, etc.
- **Foco:** Modelar o problema de negócio de forma rica e expressiva sem depender de nenhum componente externo.

#### 2. **FormularioEleicao.Infraestrutura (Camada de Infraestrutura)**
- **Responsável pela persistência de dados e outras preocupações técnicas.**
- Implementa as interfaces de repositório definidas na camada de Domínio.
- Utiliza Entity Framework Core para interagir com o banco de dados (ImMemory).
- Contém configurações de mapeamento de entidades para o banco de dados, tanto para a entidade Formulário como Pergunta, pode-se checar isso no arquivo .

#### 3. **FormularioEleicao.API (Camada de Apresentação/Aplicação)**
- **Ponto de entrada da aplicação.** Expõe a funcionalidade de negócio através de uma API RESTful.
- Contém controladores (Controllers) que recebem requisições HTTP, **orquestram** as operações de domínio e retornam respostas HTTP.
- Utiliza DTOs (Data Transfer Objects) para desacoplar a API do modelo de domínio.
- Configura a injeção de dependência e o pipeline da aplicação (middleware).


### Padrões de Projeto Utilizados
- Aggregate Root Pattern (DDD): Entidades raiz que garantem a consistência transacional de um cluster de objetos relacionados (Aggregate). Todas as modificações externas ao agregado devem ser gerenciadas pela Root, pode-se observice na classe `FormularioEleicao.Dominio.Formularios`.
- Dependency Injection (DI): Gerencia as dependências entre componentes, promovendo baixo acoplamento. Aplicada para isolar a camada de Aplicação da camada de Infraestrutura, garantindo alta testabilidade.
- Domain-Driven Design (DDD): Estratégia de modelagem de software focada no domínio de negócio. Utiliza Linguagem Ubíqua e implementa conceitos como Aggregate Roots, Value Objects e Repositories para um domínio rico.
- Factory Pattern: Utilizado nos métodos de criação (Criar) de Value Objects e Aggregate Roots. Encapsula a lógica de construção e validação, assegurando que os objetos sejam instanciados sempre em um estado consistente e válido.
- Fluent API (EF Core): Mecanismo de configuração utilizado no Entity Framework Core para o mapeamento objeto-relacional. Essencial para mapear Value Objects complexos para a estrutura do banco de dados.
- Repository Pattern: Abstrai a lógica de persistência de dados. Permite que a camada de Domínio interaja com coleções de objetos de maneira agnóstica, sem conhecimento dos detalhes técnicos de armazenamento (banco de dados).
- RESTful API: Estilo arquitetural aplicado à camada de comunicação. A API utiliza princípios REST (verbos HTTP e URLs semânticas) para fornecer uma interface clara e
padronizada para as interações cliente-servidor.

### Protocolo de Comunicação Utilizado
- A comunicação entre o cliente (front-end ou ferramenta de testes) e o back-end ocorre por meio do protocolo HTTP, seguindo o estilo arquitetural RESTful. As principais características dessa comunicação incluem:
- Uso de verbos HTTP para representar operações:
    - GET para consultas
    - POST para criação de recursos
    - PUT para atualizações
    - DELETE para remoção

Troca de dados no formato JSON, garantindo interoperabilidade entre diferentes plataformas.

Endpoints organizados de forma semântica, por exemplo:
- `/api/perguntas`
- `/api/formularios`

Comunicação stateless, onde cada requisição contém todas as informações necessárias para o seu processamento.

Essa escolha assegura simplicidade, padronização e compatibilidade com diferentes tipos de clientes, além de seguir práticas consolidadas no desenvolvimento de APIs Web modernas.

### Fluxo de Operação da API RESTful

O fluxo operacional do sistema é dividido em duas etapas principais, refletindo a ordem de cadastro e o uso dos dados no domínio.

### 1. Fluxo de Criação de Perguntas

Este fluxo detalha o processamento da requisição desde o cliente até a persistência no banco de dados em memória.

| Passo | Componente | Ação Executada |
| :--- | :--- | :--- |
| **1.** | **Requisição HTTP (API)** | Um cliente (ex: Postman) envia uma requisição **`POST`** para o *endpoint* `/api/perguntas` da `FormularioEleicao.API`, contendo os DTOs de entrada com o número e o texto da pergunta. |
| **2.** | **Controller (API)** | O *Controller* responsável (`PerguntasController`) recebe a requisição, realiza a validação inicial dos DTOs e orquestra a chamada à camada de Domínio. |
| **3.** | **Serviço de Aplicação/Domínio e Aggregate Root** | O *Controller* solicita a criação da nova entidade `Pergunta`.  A entidade `Pergunta` é criada, utilizando o **Factory Pattern** para garantir que ela esteja em um estado válido, aplicando todas as regras de negócio intrínsecas à entidade. |
| **4.** | **Repositório (Domínio/Infraestrutura)** | O método de adição do `IPerguntaRepository` é chamado. A implementação concreta na camada de Infraestrutura (`PerguntaRepository`) utiliza o **Entity Framework Core**. |
| **5.** | **Persistência (Infraestrutura)** | O EF Core interage com o **banco de dados em memória**, utilizando o `DbContext`, para persistir a nova entidade `Pergunta`. |
| **6.** | **Resposta (API)** | A operação bem-sucedida é confirmada, e o *Controller* retorna uma resposta HTTP (`201 Created` ou similar) ao cliente. |

### 2. Fluxo de Criação e Consulta de Formulários

Após o cadastro das perguntas, o sistema está apto a registrar o formulário da pesquisa e a permitir sua consulta:

| Passo | Componente | Ação Executada |
| :--- | :--- | :--- |
| **1.** | **Requisição HTTP (API)** | O cliente envia uma requisição **`POST`** para o *endpoint* `/api/formularios`, informando os dados do entrevistado, candidato e as respostas às perguntas. |
| **2.** | **Controller (API)** | O *Controller* (`FormulariosController`) recebe os DTOs de entrada e invoca a camada de Domínio para iniciar o processamento. |
| **3.** | **Aggregate Root (Domínio)** | O objeto `Formulario` (**Aggregate Root**) é instanciado. Ele executa a validação das informações do entrevistado e do candidato (usando *Value Objects*) e associa as perguntas/respostas, garantindo a consistência do agregado antes da persistência. |
| **4.** | **Repositório (Domínio/Infraestrutura)** | O `IFormularioRepository` é invocado para persistir o novo agregado `Formulario`. A implementação concreta utiliza o EF Core na camada de Infraestrutura. |
| **5.** | **Persistência (Infraestrutura)** | O EF Core realiza a operação de escrita no **banco de dados em memória**. |
| **6.** | **Consulta e Resposta (API)** | Para a consulta, o cliente envia um **`GET`** ao *endpoint* `/api/formularios`. A API consulta o `IFormularioRepository`, os dados são recuperados do banco e convertidos em DTOs, sendo retornados ao cliente como uma lista completa de Formulários. |

## 3. Testes
### Estratégia de Testes da Aplicação
Para validação do funcionamento do sistema, foram adotados testes manuais da API utilizando a ferramenta Postman. O Postman foi utilizado como um cliente HTTP para:
 - Testar os endpoints REST expostos pela aplicação.
 - Validar os fluxos de criação, consulta e remoção de perguntas.
 - Verificar o processo completo de cadastro e consulta de formulários eleitorais.
 - Confirmar o correto retorno dos códigos HTTP (200, 201, 400, 404, entre outros).

Essa abordagem permite a simulação do comportamento de um front-end consumindo a API, garantindo que os contratos REST estejam sendo respeitados.

Como parte da documentação do projeto, será disponibilizada uma collection do Postman no repositório GitHub, permitindo a reprodução dos testes por terceiros.

### Testes do Componente de Acesso a Dados

O componente de acesso a dados, implementado na camada `FormularioEleicao.Infraestrutura` com o uso do Entity Framework Core, é testado de forma indireta por meio dos testes manuais da API.

Ao realizar operações de criação, consulta e remoção de dados via Postman, é possível validar:
- A persistência correta dos dados no banco em memória
- O funcionamento dos repositórios implementados com EF Core
- A correta recuperação dos dados através do DbContext

Essa abordagem permite validar o comportamento do acesso a dados sem a necessidade de dependência de um banco de dados externo, garantindo rapidez e simplicidade durante o desenvolvimento.
