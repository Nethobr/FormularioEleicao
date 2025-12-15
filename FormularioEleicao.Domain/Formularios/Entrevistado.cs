using FormularioEleicao.Dominio.Conceito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormularioEleicao.Dominio.Formularios
{
    public class Entrevistado : Pessoa
    {
        public string Ip { get; private set; } = string.Empty;
        private Entrevistado(string nome, string documento, string ip) 
        {
            Nome = nome;
            Documento = documento;
            Ip = ip;
        }

        public static Entrevistado Criar(string nome, string documento, string email, string ip)
        {
            ValidaCamposObrigatorios(nome, documento);

            if (ip == null)
                throw new DominioException("O IP do cadastro do candidato não pode ser nulo.");

            var candidato = new Entrevistado(nome, documento, ip);

            candidato.AdicionarEmail(email);

            return candidato;
        }
    }
}
