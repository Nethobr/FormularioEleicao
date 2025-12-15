using FormularioEleicao.Dominio.Conceito;
using FormularioEleicao.Dominio.Formularios;
using FormularioEleicao.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormularioEleicao.Infraestrutura.Repositorios
{
    public class PerguntaEmMemoria : IPerguntaRepositorio
    {
        private readonly FormularioEleicaoDbContext _context;

        public PerguntaEmMemoria(FormularioEleicaoDbContext context)
        {
            _context = context;
        }

        public Pergunta Adicionar(Pergunta pergunta)
        {
            _context.Perguntas.Add(pergunta);
            _context.SaveChanges();
            return pergunta;
        }

        public Pergunta Atualizar(Pergunta pergunta)
        {
            if (pergunta == null)
                throw new ArgumentNullException(nameof(pergunta));

            _context.Perguntas.Update(pergunta);
            _context.SaveChanges();

            return pergunta;
        }

        public Pergunta AtualizarTexto(Guid id, string texto)
        {
            var pergunta = _context.Perguntas.Find(id);

            if (pergunta == null)
                throw new DominioException("Pergunta não encontrada.");
            
            pergunta.AlteraTextoPergunta(texto);
            
            _context.Perguntas.Update(pergunta);
            _context.SaveChanges();
            
            return pergunta;
        }

        public void Deletar(Guid id)
        {
            var perguntaARemover = _context.Perguntas.Find(id);

            if(perguntaARemover != null)
            {
                _context.Perguntas.Remove(perguntaARemover);
                _context.SaveChanges();
            }
        }

        public Pergunta ObterPorId(Guid id)
        {
            return _context.Perguntas.FirstOrDefault(p => p.Id == id);
        }

        public Pergunta ObterPorNumeroPergunta(int numeroPergunta)
        {
            return _context.Perguntas.FirstOrDefault(p => p.NumeroPergunta == numeroPergunta);
        }

        public List<Pergunta> ObterTudo()
        {
            return _context.Perguntas.ToList();
        }
    }
}
