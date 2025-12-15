using System.ComponentModel.DataAnnotations;

namespace FormularioEleicao.Api.Dtos
{
    public class CriarFormularioRequisicao
    {
        [Required(ErrorMessage = "O nome do candidato é obrigatório.")]
        public CandidatoRequisicao Candidato { get; set; } = new CandidatoRequisicao();

        [Required(ErrorMessage = "O entrevistado é obrigatório.")]  
        public EntrevistadoRequisicao Entrevistado { get; set; } = new EntrevistadoRequisicao();

        [Required(ErrorMessage = "As respostas são obrigatórias.")]
        public List<RespostaRequisicao> Respostas { get; set; } = new List<RespostaRequisicao>();
    }

    public class FormularioResposta
    {
        public Guid Id { get; set; }
        public CandidatoResposta Candidato { get; set; } = new CandidatoResposta();
        public EntrevistadoResposta Entrevistado { get; set; } = new EntrevistadoResposta();
        public List<RespostaResposta> Respostas { get; set; } = new List<RespostaResposta>();
    }
}
