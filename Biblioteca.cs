public class Biblioteca
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- Biblioteca---");

        Console.WriteLine("\n--- CASO OK 1: Empréstimo criado e devolvido no prazo ---");
        try
        {
            var emprestimo1 = new EmprestimoLivro("111", "USER-001");
            Console.WriteLine($"OK: Empréstimo criado para o livro {emprestimo1.LivroId}. Devolver até {emprestimo1.DataPrevistaDevolucao:dd/MM/yyyy}.");
            emprestimo1.DevolverLivro();
            Console.WriteLine($"OK: Livro devolvido. Status: {emprestimo1.Status}. Multa: {emprestimo1.CalcularMulta(2.50m):C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERRO INESPERADO: {ex.Message}");
        }

        Console.WriteLine("\n--- CASO OK 2: Empréstimo devolvido com atraso e cálculo de multa ---");
        try
        {
            var dataAntiga = DateTime.Now.AddDays(-20);
            var emprestimo2 = new EmprestimoLivro("222", "USER-002", dataAntiga, 14);
            Console.WriteLine($"OK: Empréstimo criado em {emprestimo2.DataEmprestimo:dd/MM/yyyy}. Prazo era {emprestimo2.DataPrevistaDevolucao:dd/MM/yyyy}.");
            emprestimo2.DevolverLivro();
            Console.WriteLine($"OK: Livro devolvido hoje. Status: {emprestimo2.Status}.");
            decimal multa = emprestimo2.CalcularMulta(2.50m);
            Console.WriteLine($"OK: Multa calculada: {multa:C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERRO INESPERADO: {ex.Message}");
        }

        Console.WriteLine("\n--- CASO ERRO 1: Tentar criar empréstimo com data futura ---");
        try
        {
            var dataFutura = DateTime.Now.AddDays(5);
            var emprestimoErro1 = new EmprestimoLivro("333", "USER-003", dataFutura);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"ERRO: {ex.Message}");
        }

        Console.WriteLine("\n--- CASO ERRO 2: Tentar criar empréstimo sem ID do livro ---");
        try
        {
            var emprestimoErro2 = new EmprestimoLivro("", "USER-004");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"ERRO: {ex.Message}");
        }
    }
}