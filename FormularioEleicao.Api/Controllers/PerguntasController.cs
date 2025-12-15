using FormularioEleicao.Api.Dtos;
using FormularioEleicao.Dominio.Conceito;
using FormularioEleicao.Dominio.Formularios;
using FormularioEleicao.Dominio.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace FormularioEleicao.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerguntasController : ControllerBase
    {
        private readonly IPerguntaRepositorio _perguntaRepositorio;

        public PerguntasController(IPerguntaRepositorio perguntaRepositorio)
        {
            _perguntaRepositorio = perguntaRepositorio;
        }

        private PerguntaResposta MapearParaPerguntaResposta(Pergunta pergunta)
        {
            return new PerguntaResposta
            {
                Id = pergunta.Id,
                Texto = pergunta.Texto,
                NumeroPergunta = pergunta.NumeroPergunta
            };
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CadastrarPergunta([FromBody] CriarPerguntaRequisicao requisicao)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pergunta = Pergunta.Criar(requisicao.Texto, requisicao.NumeroPergunta);

                _perguntaRepositorio.Adicionar(pergunta);

                return Created("pergunta", MapearParaPerguntaResposta(pergunta));
            }
            catch(DominioException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = e.ToString() });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterPerguntaId(Guid id)
        {
            try
            {
                var pergunta = _perguntaRepositorio.ObterPorId(id);

                if (pergunta == null)
                {
                    return NoContent();
                }

                return Ok(MapearParaPerguntaResposta(pergunta));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }

        [HttpGet("{numeroPergunta:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterPerguntaPorNumero(int numeroPergunta)
        {
            try
            {
                var pergunta = _perguntaRepositorio.ObterPorNumeroPergunta(numeroPergunta);

                if (pergunta == null)
                {
                    return NoContent();
                }

                return Ok(MapearParaPerguntaResposta(pergunta));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }

        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AlteraTextoPergunta(Guid id, [FromBody] AlteraTextoPerguntaRequisicao requisicao)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pergunta = _perguntaRepositorio.ObterPorId(id);

                if (pergunta == null)
                {
                    return NoContent();
                }

                var novoTexto = pergunta.AlteraTextoPergunta(requisicao.Texto);

                _perguntaRepositorio.AtualizarTexto(id, novoTexto);

                return Ok(MapearParaPerguntaResposta(pergunta));
            }
            catch (DominioException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = e.ToString() });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AlteraPergunta(Guid id, [FromBody] CriarPerguntaRequisicao requisicao)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pergunta = _perguntaRepositorio.ObterPorId(id);

                if (pergunta == null)
                {
                    return NoContent();
                }

                pergunta.AlteraPergunta(Pergunta.Criar(requisicao.Texto, requisicao.NumeroPergunta));

                _perguntaRepositorio.Atualizar(pergunta);

                return Ok(MapearParaPerguntaResposta(pergunta));
            }
            catch (DominioException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = e.ToString() });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterTodasPerguntas()
        {
            try
            {
                var perguntas = _perguntaRepositorio.ObterTudo();

                if (!perguntas.Any())
                {
                    return NoContent();
                }

                return Ok(perguntas.Select(MapearParaPerguntaResposta));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }


        [HttpDelete(("{id:guid}"))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarPergunta(Guid id)
        {
            try
            {
                _perguntaRepositorio.Deletar(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }
    }
}
