using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CalculoArea.Models
{
	public class XmlProteinaModel
	{
		[Required(ErrorMessage = "Preencha o código do átomo")]
		[MaxLength(10, ErrorMessage = "Máximo de {0} caracteres")]
		[Display(Name = "Código da Proteina")]
		public string CodProteina { get; set; }
	}
}