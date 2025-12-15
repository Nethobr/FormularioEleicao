using FormularioEleicao.Api.Dtos;
using FormularioEleicao.Dominio.Conceito;
using FormularioEleicao.Dominio.Formularios;
using FormularioEleicao.Dominio.Repositorios;
using Microsoft.AspNetCore.Mvc;
namespace FormularioEleicao.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormulariosController : ControllerBase
    {
        private readonly IFormularioRepositorio _formularioRepositorio;
        private readonly IPerguntaRepositorio _perguntaRepositorio;

        public FormulariosController(IFormularioRepositorio formularioRepositorio, IPerguntaRepositorio perguntaRepositorio)
        {
            _formularioRepositorio = formularioRepositorio;
            _perguntaRepositorio = perguntaRepositorio;
        }

        private FormularioResposta MapearParaFormularioResposta(Formulario formulario)
        {
            var formularioResposta = new FormularioResposta
            {
                Id = formulario.Id,

                Candidato = new CandidatoResposta
                {
                    Nome = formulario.Candidato.Nome,
                    Documento = formulario.Candidato.Documento,
                    Email = formulario.Candidato.Email,
                    Partido = formulario.Candidato.Partido
                },

                Entrevistado = new EntrevistadoResposta
                {
                    Nome = formulario.Entrevistado.Nome,
                    Documento = formulario.Entrevistado.Documento,
                    Email = formulario.Entrevistado.Email,
                    Ip = formulario.Entrevistado.Ip
                },

                Respostas = formulario.Respostas.Select(r =>
                    {
                        var pergunta = _perguntaRepositorio.ObterPorNumeroPergunta(r.NumeroPergunta);

                        return new RespostaResposta
                        {
                            Pergunta = new PerguntaResposta
                            {
                                Id = pergunta.Id,
                                NumeroPergunta = pergunta.NumeroPergunta,
                                Texto = pergunta.Texto
                            },
                            Valor = r.Valor
                        };
                    }
                ).ToList()
            };
            return formularioResposta;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarFormulario([FromBody] CriarFormularioRequisicao requisicao)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var formulario = Formulario.Criar();

                formulario.AdicionarCandidato(
                    requisicao.Candidato.Nome,
                    requisicao.Candidato.Documento,
                    requisicao.Candidato.Email,
                    requisicao.Candidato.Partido
                );

                formulario.AdicionarEntrevistado(
                    requisicao.Entrevistado.Nome,
                    requisicao.Entrevistado.Documento,
                    requisicao.Entrevistado.Email,
                    requisicao.Entrevistado.Ip
                );

                requisicao.Respostas.ForEach(resposta =>
                    {
                        var pergunta = _perguntaRepositorio.ObterPorNumeroPergunta(resposta.NumeroPergunta);

                        if (pergunta == null)
                        {
                            throw new ArgumentException("Pergunta inválida informada!");
                        }

                        var novaResposta = Resposta.Criar(pergunta.NumeroPergunta, resposta.Valor);

                        formulario.AdicionaResposta(novaResposta);
                    }
                );

                _formularioRepositorio.Adicionar(formulario);

                return Created("formulario", MapearParaFormularioResposta(formulario));
            }
            catch (DominioException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = e.ToString() });
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterTodosFormularios()
        {
            try
            {
                var formularios = _formularioRepositorio.ObterTudo();

                if (!formularios.Any())
                {
                    return NoContent();
                }

                return Ok(formularios.Select(MapearParaFormularioResposta));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }
    }
}
