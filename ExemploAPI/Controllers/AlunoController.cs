using ExemploAPI.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExemploAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AlunoController : ControllerBase
	{
		private readonly string _AlunoCaminhoArquivo;

		public AlunoController()
		{
			_AlunoCaminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Aluno.json");
		}

		#region Métodos arquivo
		private List<AlunoViewModel> LerAlunosDoArquivo()
		{
			if (!System.IO.File.Exists(_AlunoCaminhoArquivo))
			{
				return new List<AlunoViewModel>();
			}

			string json = System.IO.File.ReadAllText(_AlunoCaminhoArquivo);
			return JsonConvert.DeserializeObject<List<AlunoViewModel>>(json);
		}

		private int ObterProximoCodigoDisponivel()
		{
			List<AlunoViewModel> produtos = LerAlunosDoArquivo();
			
			if (produtos.Any())
			{
				return produtos.Max(p => p.Codigo) + 1;
			}
			else
			{
				return 1;
			}
		}

		private void EscreverProdutosNoArquivo(List<AlunoViewModel> produtos)
		{
			string json = JsonConvert.SerializeObject(produtos);
			System.IO.File.WriteAllText(_AlunoCaminhoArquivo, json);
		}
		#endregion

		#region Operações CRUD

		[HttpGet]
		public IActionResult Get()
		{
			List<AlunoViewModel> produtos = LerAlunosDoArquivo();
			return Ok(produtos);
		}

		[HttpGet("{codigo}")]
		public IActionResult Get(int codigo)
		{
			List<AlunoViewModel> produtos = LerAlunosDoArquivo();
			AlunoViewModel produto = produtos.Find(p => p.Codigo == codigo);
			if (produto == null)
			{
				return NotFound();
			}

			return Ok(produto);
		}

		[HttpPost]
		public IActionResult Post([FromBody] NovoAlunoViewModel aluno)
		{
			if (aluno == null)
			{
				return BadRequest();
			}

			List<AlunoViewModel> produtos = LerAlunosDoArquivo();
			int proximoCodigo = ObterProximoCodigoDisponivel();

			AlunoViewModel novoProduto = new AlunoViewModel()
			{
				Codigo = proximoCodigo,
				Nome = aluno.Nome,
				RA = aluno.RA,
				Email = aluno.Email,
				CPF = aluno.CPF,
				Ativo = aluno.Ativo
			};

			produtos.Add(novoProduto);
			EscreverProdutosNoArquivo(produtos);

			return CreatedAtAction(nameof(Get), new { codigo = novoProduto.Codigo }, novoProduto);
		}

		[HttpPut("{codigo}")]
		public IActionResult Put(int codigo, [FromBody] EditaAlunoViewModel produto)
		{
			if (produto == null )
				return BadRequest();

			List<AlunoViewModel> produtos = LerAlunosDoArquivo();
			int index = produtos.FindIndex(p => p.Codigo == codigo);
			if (index == -1)
				return NotFound();

			AlunoViewModel produtoEditado = new AlunoViewModel()
			{
				Codigo = codigo,
                Nome = produto.Nome,
                RA = produto.RA,
                Email = produto.Email,
                CPF = produto.CPF,
                Ativo = produto.Ativo
            };

			produtos[index] = produtoEditado;
			EscreverProdutosNoArquivo(produtos);

			return NoContent();
		}

		[HttpDelete("{codigo}")]
		public IActionResult Delete(int codigo)
		{
			List<AlunoViewModel> produtos = LerAlunosDoArquivo();
			AlunoViewModel produto = produtos.Find(p => p.Codigo == codigo);
			if (produto == null)
				return NotFound();

			produtos.Remove(produto);
			EscreverProdutosNoArquivo(produtos);

			return NoContent();
		}
		#endregion
	}
}