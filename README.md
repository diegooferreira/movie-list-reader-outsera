
# Movie List Reader

#### Informações do projeto:

##### Objetivo
API RESTful para possibilitar a leitura da lista de indicados e vencedores da categoria Pior Filme do Golden Raspberry Awards.

##### Tecnologia(s)

.NET 8

#### Para clonar o projeto, executar o comando:

`git clone https://github.com/diegooferreira/movie-list-reader-outsera.git`

#### Iniciando o projeto

Acessar a pasta `Outsera.MovieListReader.Api` via linha de comando e executar `dotnet run`. Ao processar o comando, acessar o endereço `http://localhost:5000` em algum browser;

#### Executando os testes

Acessar a pasta `Outsera.MovieListReader.Test.Integration` via linha de comando e executar `dotnet test`.

#### Arquivo CSV

Atualmente no projeto está na pasta `Outsera.MovieListReader.Api/Resources` e a configuração do caminho do arquivo CSV que o sistema deve ler está no appsettings.json - atributo `MovieListCsvPath`