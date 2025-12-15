using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormularioEleicao.Dominio.Conceito;

namespace FormularioEleicao.Dominio.Formularios
{
    public class Resposta : ObjetoValor
    {
        public int NumeroPergunta { get; private set; }
        public bool Valor { get; private set; } = false;

        private Resposta() { }

        private Resposta(int pergunta, bool resposta) 
        {
            NumeroPergunta = pergunta;
            Valor = resposta;
        }
        public static Resposta Criar(int pergunta, bool resposta)
        {
            if (pergunta == null)
                throw new DominioException("Nunero da Pergunta não deve ser nulo");
            
            var questao = new Resposta(pergunta, resposta);

            return questao;
        }
    }
}
