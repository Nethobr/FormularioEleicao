using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormularioEleicao.Dominio.Conceito;

namespace FormularioEleicao.Dominio.Formularios
{
    public class Candidato : Pessoa
    {
        public string Partido { get; private set; } = string.Empty;
        private Candidato(string nome, string documento, string partido) 
        {
            Nome = nome;
            Documento = documento;
            Partido = partido;
        }

        public static Candidato Criar(string nome, string documento, string email, string partido)
        {
            ValidaCamposObrigatorios(nome, documento);

            if (partido == null)
                throw new DominioException("O partido do cadastro do candidato não pode ser nulo.");

            var candidato = new Candidato(nome, documento, partido);

            candidato.AdicionarEmail(email);

            return candidato;
        }
    }
}
