using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
	public class Proteina
	{
		public Proteina()
		{
			AreasDeContato = new Dictionary<string, AreaDeContato>();
		}

		public string Codigo { get; set; }
		public Dictionary<string, AreaDeContato> AreasDeContato { get; set; }
	}
}
