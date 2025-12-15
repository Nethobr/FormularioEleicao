using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormularioEleicao.Dominio.Formularios;

namespace FormularioEleicao.Dominio.Repositorios
{
    public interface IPerguntaRepositorio
    {
        Pergunta Adicionar(Pergunta pergunta);

        Pergunta Atualizar(Pergunta pergunta);

        Pergunta AtualizarTexto(Guid id, string texto);

        void Deletar(Guid id);

        Pergunta ObterPorId(Guid id);

        Pergunta ObterPorNumeroPergunta(int numeroPergunta);

        List<Pergunta> ObterTudo();
    }
}
