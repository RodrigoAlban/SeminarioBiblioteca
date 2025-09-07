public class EmprestimoLivro
{
    // "init" garante imutabilidade da propriedade após a criação
    public string LivroId { get; init; }
    public string UsuarioId { get; init; }

    private DateTime _dataEmprestimo;
    public DateTime DataEmprestimo
    {
        get { return _dataEmprestimo; }
        private set
        {
            if (value.Date > DateTime.Now.Date)
            {
                throw new ArgumentException("A data do empréstimo não pode ser uma data futura.");
            }
            _dataEmprestimo = value;
        }
    }
    public DateTime DataPrevistaDevolucao { get; private set; }
    public DateTime? DataRealDevolucao { get; private set; }
    public StatusEmprestimo Status { get; private set; }

    public EmprestimoLivro(string livroId, string usuarioId, DateTime dataEmprestimo, int diasParaDevolucao)
    {
        if (string.IsNullOrWhiteSpace(livroId))
            throw new ArgumentNullException(nameof(livroId), "O ID do livro é obrigatório.");

        if (string.IsNullOrWhiteSpace(usuarioId))
            throw new ArgumentNullException(nameof(usuarioId), "O ID do usuário é obrigatório.");

        if (diasParaDevolucao <= 0)
            throw new ArgumentException("O prazo de devolução deve ser de pelo menos 1 dia.", nameof(diasParaDevolucao));

        LivroId = livroId;
        UsuarioId = usuarioId;
        DataEmprestimo = dataEmprestimo;
        DataPrevistaDevolucao = dataEmprestimo.AddDays(diasParaDevolucao);
        Status = StatusEmprestimo.Ativo;
        DataRealDevolucao = null;
    }
    public EmprestimoLivro(string livroId, string usuarioId, DateTime dataEmprestimo)
        : this(livroId, usuarioId, dataEmprestimo, 14) { }

    public EmprestimoLivro(string livroId, string usuarioId)
        : this(livroId, usuarioId, DateTime.Now) { }


    public void DevolverLivro()
    {
        if (Status != StatusEmprestimo.Ativo)
        {
            throw new InvalidOperationException("Esse livro já foi devolvido ou o empréstimo está inválido.");
        }

        DataRealDevolucao = DateTime.Now;
        if (DataRealDevolucao.Value.Date > DataPrevistaDevolucao.Date)
        {
            Status = StatusEmprestimo.Atrasado;
        }
        else
        {
            Status = StatusEmprestimo.Devolvido;
        }
    }

    public decimal CalcularMulta(decimal valorMultaPorDia)
    {
        if (Status != StatusEmprestimo.Atrasado || DataRealDevolucao == null)
        {
            return 0; 
        }

        TimeSpan diasAtraso = DataRealDevolucao.Value.Date - DataPrevistaDevolucao.Date;
        if (diasAtraso.Days > 0)
        {
            return diasAtraso.Days * valorMultaPorDia;
        }
        return 0;
    }
}