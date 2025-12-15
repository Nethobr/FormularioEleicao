using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FormularioEleicao.Dominio.Repositorios;
using FormularioEleicao.Infraestrutura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace FormularioEleicao.Infraestrutura
{
    public static class ColecaoExtensaoServicos
    {
        /// <summary>
        /// Adiciona os serviços de infraestrutura (DbContext e Repositórios) à coleção de serviços.
        /// </summary>
        /// <param name="services">A coleção de serviços.</param>
        /// <param name="configuration">A configuração da aplicação.</param>
        /// <returns>A coleção de serviços atualizada.</returns>
        public static IServiceCollection AdicionaInfraestrutura(this IServiceCollection servicos, IConfiguration configuracao)
        {
            servicos.AddDbContext<FormularioEleicaoDbContext>(options => options.UseInMemoryDatabase("FormularioEleicao"));

            // Registra o repositório de clientes para injeção de dependência.
            // Scoped significa que uma nova instância será criada por requisição.
            servicos.AddScoped<IFormularioRepositorio, FormularioEmMemoria>();
            servicos.AddScoped<IPerguntaRepositorio, PerguntaEmMemoria>();

            return servicos;
        }
    }
}
