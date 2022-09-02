using System.Globalization;

namespace controle_de_estoque;

public class Loja
{
    private Produto[] estoque;
    private readonly string nomeDaLoja;
    private int QntProdutos;

    public Loja(string nomeDaLoja, int qntProdutos)
    {
        this.nomeDaLoja = nomeDaLoja;
        QntProdutos = 0;
        estoque = new Produto[qntProdutos];
    }

    public Produto[] Estoque
    {
        get => estoque;
    }

    public string NomeDaLoja
    {
        get => nomeDaLoja;
    }

    public void AdicionarProduto(string nome, int quantidade, double valor)
    {
        if (QntProdutos == estoque.Length)
        {
            throw new IndexOutOfRangeException("O estoque não suporta mais produtos");
        }
        var novoProduto = new Produto(nome, quantidade, valor);
        for (var i = 0; i < estoque.Length; i++)
        {
            if (estoque[i] == null)
            {
                estoque[i] = novoProduto;
                QntProdutos++;
                return;
            }
        }
    }

    public void ListarProdutos()
    {
        if (QntProdutos == 0)
        {
            throw new NullReferenceException("Não existem produtos no estoque");
        }

        foreach (var produto in estoque)
        {
            if (produto != null)
            {
                Console.WriteLine($"Nome: {produto.Nome}\n" +
                                  $"Quantidade: {produto.Quantidade}\n" +
                                  $"Valor: {produto.Valor.ToString( "N2",new CultureInfo("en-us"))}");
                Console.WriteLine("");
            }
        }
    }

    public void EditarProduto(string nome, int quantidade, double valor)
    {
        if (QntProdutos == 0)
        {
            throw new NullReferenceException("Não existem produtos no estoque");
        }
        for (var i = 0; i < QntProdutos; i++)
        {
            if (estoque[i] != null && estoque[i]?.Nome == nome)
            {
                estoque[i] = new Produto(nome, quantidade, valor);
                // estoque[i].Nome = nome;
                // estoque[i].Quantidade = quantidade;
                // estoque[i].Valor = valor;
                return;
            }
        }

        throw new NullReferenceException("Produto não encontrado no estoque");
    }
    
    public void RemoverProduto(string nome)
    {
        if (QntProdutos == 0)
        {
            throw new IndexOutOfRangeException("Não existem produtos no estoque");
        }
        for (var i = 0; i < QntProdutos; i++)
        {
            if (estoque[i].Nome == nome)
            {
                estoque[i] = null;
                QntProdutos--;
            }
        }
    }
}