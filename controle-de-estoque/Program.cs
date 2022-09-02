using System.Globalization;
using controle_de_estoque;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--Controle de Estoque--\nDigite o nome da sua loja:");
        var nomeLoja = Console.ReadLine();
        Console.WriteLine("Digite a quantidade de produtos que o estoque poderá comportar:");
        int.TryParse(Console.ReadLine(), out var qntProdutos);
        
        var loja = new Loja(nomeLoja ?? "Loja", qntProdutos);

        var op = 99;
        while (op != 0)
        {
            Console.WriteLine($"Loja {loja.NomeDaLoja}" +
                              $"\nDigite a opção desejada:\n" +
                              "\t1 - Listar produtos\n" +
                              "\t2 - Adicionar produto\n" +
                              "\t3 - Editar produto\n" +
                              "\t4 - Remover produto\n" +
                              "\t0 - SAIR");
            if (int.TryParse(Console.ReadLine(), out op))
            {
                Options(op, loja);
            }
            else
            {
                op = 99;
                Options(op, loja);
            }
        };
    }

    private static void Options(int option, Loja loja)
    {
        switch (option)
        {
            case 1:
                try
                {
                    Console.WriteLine("Produtos em estoque:");
                    loja.ListarProdutos();
                    Console.WriteLine("-----------");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    // throw;
                }

                break;

            case 2:
                Console.WriteLine("Digite o nome do produto, quantidade e valor separados por ESPAÇO:");
                var entrada1 = Console.ReadLine();
                try
                {
                    loja.AdicionarProduto(entrada1.Split(" ")[0],
                        int.Parse(entrada1.Split(" ")[1]),
                        double.Parse(entrada1.Split(" ")[2], new CultureInfo("en-us")));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    // throw;
                }

                break;
            
            case 3:
                Console.WriteLine("Digite o nome do produto, quantidade e valor que deseja editar separados por ESPAÇO:");
                var entrada2 = Console.ReadLine();
                try
                {
                    loja.EditarProduto(entrada2.Split(" ")[0],
                        int.Parse(entrada2.Split(" ")[1]),
                        double.Parse(entrada2.Split(" ")[2]));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    // throw;
                }
                break;
            
            case 4:
                Console.WriteLine("Digite o nome do produto que deseja remover:");
                var entrada3 = Console.ReadLine();
                try
                {
                    loja.RemoverProduto(entrada3);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    // throw;
                }
                break;
            
            case 0:
                break;
            default:
                Console.WriteLine("Digite uma opção válida!");
                break;
        }
    }
}