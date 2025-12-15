using System.ComponentModel.DataAnnotations;

namespace FormularioEleicao.Api.Dtos
{
    public class RespostaRequisicao
    {
        [Required(ErrorMessage = "A pergunta é obrigatória para uma resposta")]
        public int NumeroPergunta { get; set; }

        [Required(ErrorMessage = "O valor da resposta é obrigatório")]
        public bool Valor { get; set; }
    }

    public class RespostaResposta
    {
        public PerguntaResposta Pergunta { get; set; }
        public bool Valor { get; set; }
    }
}
