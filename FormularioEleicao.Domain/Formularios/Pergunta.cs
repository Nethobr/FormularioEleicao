using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormularioEleicao.Dominio.Conceito;
using FormularioEleicao.Dominio.Repositorios;

namespace FormularioEleicao.Dominio.Formularios
{
    public class Pergunta : Entidade
    {
        public string Texto { get; private set; } = String.Empty;
        public int NumeroPergunta { get; private set; }
        
        private Pergunta(string texto, int numeroPergunta)
        {
            Texto = texto;
            NumeroPergunta = numeroPergunta;
        }

        public static Pergunta Criar(string texto, int numeroPergunta)
        {
            ValidaTextoPergunta(texto);
            ValidaNumeroPergunta(numeroPergunta);

            var pergunta = new Pergunta(texto, numeroPergunta);

            return pergunta;
        }

        public Pergunta AlteraPergunta(Pergunta pergunta)
        {
            ValidaTextoPergunta(pergunta.Texto);
            ValidaNumeroPergunta(pergunta.NumeroPergunta);

            Texto = pergunta.Texto;
            NumeroPergunta = pergunta.NumeroPergunta;

            return this;
        }

        public string AlteraTextoPergunta(string texto)
        {
            ValidaTextoPergunta(texto);
            Texto = texto;
            return texto;
        }

        public int AlteraNumeroPergunta(int numeroPergunta)
        {
            ValidaNumeroPergunta(numeroPergunta);
            NumeroPergunta = numeroPergunta;
            return numeroPergunta;
        }

        private static void ValidaTextoPergunta(string texto)
        {
            if(string.IsNullOrEmpty(texto)) throw new DominioException("O texto da pergunta é obrigatório");
            if (texto.Length < 5) throw new DominioException("O texto ter no mínimo 5 caracters");
        }

        private static void ValidaNumeroPergunta(int numeroPergunta)
        {
            if(numeroPergunta <= 0) throw new DominioException("O numero da pergunta deve ser maior que 0");
        }
    }
}
