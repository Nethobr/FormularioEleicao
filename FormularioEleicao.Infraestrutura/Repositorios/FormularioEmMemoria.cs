using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormularioEleicao.Dominio.Conceito;
using FormularioEleicao.Dominio.Formularios;
using FormularioEleicao.Dominio.Repositorios;

namespace FormularioEleicao.Infraestrutura.Repositorios
{
    public class FormularioEmMemoria : IFormularioRepositorio
    {
        private readonly FormularioEleicaoDbContext _context;

        public FormularioEmMemoria(FormularioEleicaoDbContext context)
        {
            _context = context;
        }

        public Formulario Adicionar(Formulario formulario)
        {
            _context.Formularios.Add(formulario);
            _context.SaveChanges();
            return formulario;
        }

        public Formulario AlterarResposta(Resposta resposta)
        {
            var formulario = _context.Formularios
                .FirstOrDefault(f => f.Respostas.Any(r => r.NumeroPergunta == resposta.NumeroPergunta));

            if(formulario != null)
            {
                formulario.AlteraResposta(resposta);
                _context.Formularios.Update(formulario);
                _context.SaveChanges();
            }

            return formulario;
        }

        public void Deletar(Guid id)
        {
            var formulario = _context.Formularios.Find(id);
            if(formulario != null)
            {
                _context.Formularios.Remove(formulario);
                _context.SaveChanges();
            }
        }

        public Formulario ObterPorId(Guid id)
        {
            return _context.Formularios.Find(id);
        }

        public List<Formulario> ObterTudo()
        {
            return _context.Formularios.ToList();
        }
    }
}
