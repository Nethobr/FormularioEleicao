using System.ComponentModel.DataAnnotations;

namespace FormularioEleicao.Api.Dtos
{
    public abstract class PessoaRequisicao
    {
        [Required(ErrorMessage = "O nome do candidato é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O documento do candidato é obrigatório.")]
        public string Documento { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email do candidato é obrigatório.")]
        public string Email { get; set; } = string.Empty;
    }

    public class CandidatoRequisicao : PessoaRequisicao
    {
        [Required(ErrorMessage = "O partido do candidato é obrigatório.")]
        public string Partido { get; set; } = string.Empty;
    }

    public class EntrevistadoRequisicao : PessoaRequisicao
    {
        [Required(ErrorMessage = "O IP do entrevistado é obrigatório.")]
        public string Ip { get; set; } = string.Empty;
    }

    /// <summary>
    /// - Respostas
    /// </summary>

    public abstract class PessoaResposta
    {
        public string Nome { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class CandidatoResposta : PessoaResposta
    {
        public string Partido { get; set; } = string.Empty;
    }

    public class EntrevistadoResposta : PessoaResposta
    {
        public string Ip { get; set; } = string.Empty;
    }
}
