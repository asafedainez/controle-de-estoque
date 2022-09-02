using System;
using System.Globalization;
using System.IO;
using Xunit;
using FluentAssertions;

namespace controle_de_estoque.Test;

public class ControleDeEstoqueTest
{
    [Theory(DisplayName = "Testando criação de loja")]
    [InlineData("Loja1", 1)]
    [InlineData("Loja2", 10)]
    public void TestandoCriacaoDeLoja(string nome, int estoque)
    {
        var instance = new Loja(nome, estoque);
        instance.Estoque.Length.Should().Be(estoque);
        instance.NomeDaLoja.Should().Be(nome);
    }
    
    [Theory(DisplayName = "Testando função AdicionarProduto e Listar produtos")]
    [InlineData("Test1", 1, 1.5)]
    [InlineData("Test2", 1, 10100.5)]
    public void TestandoAdicionarProduto(string nome, int qnt, double valor)
    {
        var writer = new StringWriter();
        var consoleDefault = Console.Out;
        var instance = new Loja("test", 1);
        instance.AdicionarProduto(nome, qnt, valor);
        Console.SetOut(writer);
        instance.ListarProdutos();
        Console.SetOut(consoleDefault);
        var result = writer.ToString().Trim().Split(Environment.NewLine);
        result[0].Should().Contain(nome);
        result[1].Should().Contain(qnt.ToString());
        result[2].Should().Contain(valor.ToString("N2", new CultureInfo("en-us")));
    }
    
    [Theory(DisplayName = "Testando falha de AdicionarProduto")]
    [InlineData("Test1", 1, 1.5)]
    [InlineData("Test2", 1, 10100.5)]
    public void TestandoFalhaAdicionarProduto(string nome, int qnt, double valor)
    {
        var instance = new Loja("test", 0);
        var act = () => instance.AdicionarProduto(nome, qnt, valor);
        act.Should().Throw<IndexOutOfRangeException>()
            .WithMessage("O estoque não suporta mais produtos");
    }
    
    [Theory(DisplayName = "Testando falha de ListarProdutos")]
    [InlineData()]
    public void TestandoFalhaListarProdutos()
    {
        var instance = new Loja("test", 0);
        var act = () => instance.ListarProdutos();
        act.Should().Throw<NullReferenceException>()
            .WithMessage("Não existem produtos no estoque");
    }
    
    [Theory(DisplayName = "Testando função AdicionarProduto, EditarProduto e ListarProduto")]
    [InlineData("Produto1", 10, 1.5, 7)]
    [InlineData("Produto2", 1, 10100.5, 5)]
    public void TestandoEditarProduto(string nome, int qnt, double valor, int novoQnt)
    {
        var writer = new StringWriter();
        var consoleDefault = Console.Out;
        var instance = new Loja("test", 1);
        Console.SetOut(writer);
        
        instance.AdicionarProduto(nome, qnt, valor);
        instance.EditarProduto(nome, novoQnt, valor);
        instance.ListarProdutos();
        
        Console.SetOut(consoleDefault);
        var result = writer.ToString().Trim().Split(Environment.NewLine);
        result[0].Should().Contain(nome);
        result[1].Should().Contain(novoQnt.ToString());
        result[2].Should().Contain(valor.ToString("N2", new CultureInfo("en-us")));
    }
    
    [Theory(DisplayName = "Testando função AdicionarProduto e RemoverProduto")]
    [InlineData("Produto1", 10, 1.5)]
    [InlineData("Produto2", 1, 10100.5)]
    public void TestandoRemoverProduto(string nome, int qnt, double valor)
    {
        var instance = new Loja("test", 1);
        
        instance.AdicionarProduto(nome, qnt, valor);
        instance.RemoverProduto(nome);
        var act = () => instance.ListarProdutos();

        act.Should().Throw<NullReferenceException>()
            .WithMessage("Não existem produtos no estoque");
    }
    
    [Theory(DisplayName = "Testando falha de RemoverProduto")]
    [InlineData("Produto1")]
    [InlineData("Produto2")]
    public void TestandoFalhaRemoverProduto(string nome)
    {
        var instance = new Loja("test", 1);
        
        var act = () => instance.RemoverProduto(nome);;

        act.Should().Throw<IndexOutOfRangeException>()
            .WithMessage("Não existem produtos no estoque");
    }
}