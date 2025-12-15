using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormularioEleicao.Dominio.Formularios;

namespace FormularioEleicao.Dominio.Repositorios
{
    public interface IFormularioRepositorio
    {
        Formulario Adicionar(Formulario formulario);

        Formulario AlterarResposta(Resposta resposta);

        void Deletar(Guid id);

        Formulario ObterPorId(Guid id);

        List<Formulario> ObterTudo();
    }
}
