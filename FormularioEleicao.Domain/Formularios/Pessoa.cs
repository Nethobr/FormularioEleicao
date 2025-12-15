using FormularioEleicao.Dominio.Conceito;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormularioEleicao.Dominio.Formularios
{
    public abstract class Pessoa
    {
        public string Nome { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public DateTime? DataCadastro { get; set; }

        public string Email { get; private set; } = null!;

        public static void ValidaCamposObrigatorios(string nome, string documento)
        {
            if (nome == null)
                throw new DominioException("O nome do candidato não pode ser nulo.");

            if (documento == null)
                throw new DominioException("O documento do candidato não pode ser nulo.");
        }

        public void AdicionarEmail(string enderecoEmail)
        {
            if (enderecoEmail == null)
                throw new DominioException("O email não pode ser nulo.");

            if (!enderecoEmail.Contains("@") || !enderecoEmail.Contains("."))
                throw new DominioException("Digite um valor válido para o email");

            Email = enderecoEmail;
        }
    }
}
