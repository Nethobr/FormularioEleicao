using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormularioEleicao.Dominio.Conceito;

namespace FormularioEleicao.Dominio.Formularios
{
    public class Formulario : Entidade
    {
        public Candidato Candidato { get; private set; }
        public Entrevistado Entrevistado { get; private set; }
        public  DateTime DataEnvio { get; private set; }

        private readonly List<Resposta> _respostas;
        public IReadOnlyList<Resposta> Respostas => _respostas.AsReadOnly();

        private Formulario(DateTime dataEnvio)
        {
            _respostas = [];
            Candidato = null!;
            Entrevistado = null!;
            DataEnvio = dataEnvio;
        }

        public static Formulario Criar()
        {
            return new(DateTime.UtcNow);
        }

        public Candidato AdicionarCandidato(string nome, string documento, string email, string partido)
        {
            var candidato = Candidato.Criar(nome, documento, email, partido);
            Candidato = candidato;
            return candidato;
        }

        public Entrevistado AdicionarEntrevistado(string nome, string documento, string email, string ip)
        {
            var entrevistado = Entrevistado.Criar(nome, documento, email, ip);
            Entrevistado = entrevistado;
            return entrevistado;
        }

        public Resposta AdicionaResposta(Resposta resposta)
        {
            if (resposta == null)
                throw new DominioException("A resposta não pode ser nula.");

            var questaoExistente = _respostas.FirstOrDefault(r => r.NumeroPergunta == resposta.NumeroPergunta);

            if (questaoExistente != null)
                throw new DominioException("A questão já existe no formulário.");

            _respostas.Add(resposta);
            return resposta;
        }
    }
}
