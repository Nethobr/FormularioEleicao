using System.ComponentModel.DataAnnotations;

namespace FormularioEleicao.Api.Dtos
{
    public class CriarPerguntaRequisicao
    {
        [Required(ErrorMessage = "O número da pergunta é obrigatório.")]
        public int NumeroPergunta { get; set; }

        [Required(ErrorMessage = "O texto da pergunta é obrigatório.")]
        public string Texto { get; set; } = string.Empty;
    }

    public class AlteraTextoPerguntaRequisicao
    {
        [Required(ErrorMessage = "O texto da pergunta é obrigatório.")]
        public string Texto { get; set; } = string.Empty;
    }

    public class PerguntaResposta
    {
        public Guid Id { get; set; }
        public int NumeroPergunta { get; set; }
        public string Texto { get; set; } = string.Empty;
    }
}
