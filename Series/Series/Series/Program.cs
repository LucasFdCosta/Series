// See https://aka.ms/new-console-template for more information
using Series.Entidades;
using Series.Enum;

SerieRepositorio repositorio = new SerieRepositorio();

string opcaoUsuario = ObterOpcaoUsuario();
while (opcaoUsuario.ToUpper() != "X")
{
    switch (opcaoUsuario)
    {
        case "1":
            ListarSeries(repositorio);
            break;
        case "2":
            InserirSerie(repositorio);
            break;
        case "3":
            AtualizarSerie(repositorio);
            break;
        case "4":
            ExcluirSerie(repositorio);
            break;
        case "5":
            VisualizarSerie(repositorio);
            break;
        case "C":
            Console.Clear();
            break;
        default:
            throw new ArgumentOutOfRangeException();
    }
    opcaoUsuario = ObterOpcaoUsuario();
}
Console.WriteLine("Obrigado por utilizar nossos serviços.");

static void VisualizarSerie(SerieRepositorio repositorio)
{
    Console.Write("Digite o id da série: ");
    int indiceSerie = int.Parse(Console.ReadLine());

    var serie = repositorio.RetornaPorId(indiceSerie);

    Console.WriteLine(serie);
}

static void ExcluirSerie(SerieRepositorio repositorio)
{
    Console.Write("Digite o id da série: ");
    int indiceSerie = int.Parse(Console.ReadLine());

    repositorio.Exclui(indiceSerie);
}

static void AtualizarSerie(SerieRepositorio repositorio)
{
    Console.Write("Digite o id da série: ");
    int indiceSerie = int.Parse(Console.ReadLine());

    // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
    // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
    foreach (int i in Enum.GetValues(typeof(Genero)))
    {
        Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
    }
    Console.Write("Digite o gênero entre as opções acima: ");
    int entradaGenero = int.Parse(Console.ReadLine());

    Console.Write("Digite o Título da Série: ");
    string entradaTitulo = Console.ReadLine();

    Console.Write("Digite o Ano de Início da Série: ");
    int entradaAno = int.Parse(Console.ReadLine());

    Console.Write("Digite a Descrição da Série: ");
    string entradaDescricao = Console.ReadLine();

    Serie atualizaSerie = new Serie(id: indiceSerie,
                                genero: (Genero)entradaGenero,
                                titulo: entradaTitulo,
                                ano: entradaAno,
                                descricao: entradaDescricao);

    repositorio.Atualiza(indiceSerie, atualizaSerie);
}

static void InserirSerie(SerieRepositorio repositorio)
{
    Console.WriteLine("Inserir nova série");

    // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
    // https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
    foreach (int i in Enum.GetValues(typeof(Genero)))
    {
        Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
    }
    Console.Write("Digite o gênero entre as opções acima: ");
    int entradaGenero = int.Parse(Console.ReadLine());

    Console.Write("Digite o Título da Série: ");
    string entradaTitulo = Console.ReadLine();

    Console.Write("Digite o Ano de Início da Série: ");
    int entradaAno = int.Parse(Console.ReadLine());

    Console.Write("Digite a Descrição da Série: ");
    string entradaDescricao = Console.ReadLine();

    Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                genero: (Genero)entradaGenero,
                                titulo: entradaTitulo,
                                ano: entradaAno,
                                descricao: entradaDescricao);

    repositorio.Insere(novaSerie);
}


static void ListarSeries(SerieRepositorio repositorio)
{
    Console.WriteLine("Listar Séries:");
    var lista = repositorio.Lista();

    if (lista.Count == 0)
    {
        Console.WriteLine("Nenhuma série cadastrada");
        return;
    }

    foreach (var serie in lista)
    {
        var excluido = serie.RetornaExcluido();

        Console.WriteLine("#ID {0}: - {1} {2}", serie.RetornaId(), serie.RetornaTitulo(), (excluido ? "*Excluído*" : ""));
    }
}

static string ObterOpcaoUsuario()
{
    Console.WriteLine("Informe a opção desejada:");
    Console.WriteLine("1 - Listar séries");
    Console.WriteLine("2 - Inserir nova série");
    Console.WriteLine("3 - Atualizar série");
    Console.WriteLine("4 - Excluir série");
    Console.WriteLine("5 - Visualizar série");
    Console.WriteLine("C - Limpar tela");
    Console.WriteLine("X - Sair");

    string opcaoUsuario = Console.ReadLine().ToUpper();
    return opcaoUsuario;
}