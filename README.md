# HOSPITAL CASE

## Descrição

Esta é uma aplicação de backend desenvolvida para gerir gestão de fichas de um hospital, incluindo autenticação e autorização com funções, gestão de pacientes, médicos, registos médicos, e outras entidades relacionadas.

## Estrutura do Projeto

O projeto é organizado em várias camadas para seguir os princípios da Clean Architecture:

- **HospitalCase.Domain**: Contém as entidades de domínio, validadores.
- **HospitalCase.Application**: Inclui os serviços de aplicação, DTOs (por criar), lógica de negócio e interfaces de repositórios e serviços.
- **HospitalCase.Infrastructure**: Responsável pela implementação dos repositórios, acesso a dados, e configuração do Entity Framework Core.
- **HospitalCase.WebAPI**: Camada de apresentação que expõe a API RESTful, incluindo controladores e configuração de autenticação.

## Tecnologias Utilizadas

- **.NET Core**
- **Entity Framework Core**: Para acesso a dados e migrações.
- **SQLServer**: Como sistema de gestão de base de dados.
- **Moq**: Para testes unitários.

## Configuração e Instalação

1. **Clone o Repositório**: \`git clone [https://github.com/seu-usuario/NomeDoProjeto.git](https://github.com/Twodio/HospitalCase)\`
2. **Instale as Dependências**: Utilize o NuGet Package Manager ou execute \`dotnet restore\` na raiz do projeto.
3. **Configure a Base de Dados**: Edite a connection string no arquivo \`appsettings.json\` na camada WebAPI.
4. **Execute as Migrações**: Utilize o comando \`dotnet ef database update\` para aplicar as migrações à base de dados.
5. **Inicie a Aplicação**: Execute \`dotnet run\` na camada WebAPI.

## Utilização

A API expõe vários endpoints para gerir pacientes, médicos, registos médicos, e autenticação. Quanto a documentação,  não se instalou o Swagger ou outro serviço semelhante.

## Testes

O projeto inclui testes unitários para modelos, repositórios, serviços, e controladores. Os testes podem ser executados utilizando o comando \`dotnet test\`.
