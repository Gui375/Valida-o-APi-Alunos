﻿using Microsoft.AspNetCore.Components.Web;

namespace ExemploAPI.Models.Request
{
	public class AlunoViewModel
	{
		public int Codigo { get; set; }
		public string Nome { get; set; }
		public string RA { get; set; }
		public string Email { get; set; }
		public string CPF { get; set; }
		public bool Ativo { get; set; }
	}
}
