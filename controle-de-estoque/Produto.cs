namespace controle_de_estoque;

public class Produto
{
    private string nome;
    private int quantidade;
    private double valor;

    public Produto (string nome, int quantidade, double valor)
    {
        this.nome = nome;
        this.quantidade = quantidade;
        this.valor = valor;
    }
    
    public string Nome
    {
        get => nome;
        set => nome = value ?? throw new ArgumentNullException("Nome não pode ser nulo");
    }

    public int Quantidade
    {
        get => quantidade;
        set => quantidade = value;
    }

    public double Valor
    {
        get => valor;
        set => valor = value;
    }
    
    
}