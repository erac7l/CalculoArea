using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
	public class AtomoRepository
	{
		System.IO.StreamReader xmlStream;
		string CodigoProteina;

		public bool CodigoProteinaExisteNaBase(string codigo)
		{
			try
			{
				CodigoProteina = codigo;
				string url = string.Format("http://www.rcsb.org/pdb/download/downloadFile.do?fileFormat=xml&compression=NO&structureId={0}", Convert.ToString(codigo));
				string result = new WebClient().DownloadString(url);
				xmlStream = new System.IO.StreamReader(GenerateStreamFromStringXml(result));
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}

		public Proteina RetornarAtomos()
		{
			Proteina proteina = new Proteina() { Codigo = CodigoProteina };
			DateTime inicio = DateTime.Now;

			//System.IO.StreamReader xmlStream = new System.IO.StreamReader(@"C:\Users\eduardo.cesar.SWDOMAIN\Documents\Visual Studio 2013\Projects\CalculoArea\XmlProteinas\1TEC.xml");

			DataSet dataSet = new DataSet();
			dataSet.ReadXml(xmlStream);
			xmlStream.Close();

			long iteracoes = 0;

			IEnumerable<DataRow> cadeias = dataSet.Tables["atom_site"].Copy().AsEnumerable();
			dataSet.Clear();

			int qtdeDeCadeias = cadeias.GroupBy(a => a.Field<string>("auth_asym_id")).Select(a => a).Count();
			
			if (qtdeDeCadeias > 1)
				foreach (var atomoPrincipal in cadeias)
				{
					var cadeiaAuxiliar = cadeias.Where(a => a.Field<string>("auth_asym_id") != atomoPrincipal.Field<string>("auth_asym_id"));
					var idAtmPrinc = atomoPrincipal.Field<string>("Id");

					foreach (var atomoAuxiliar in cadeiaAuxiliar)
					{
						var idAtmAux = atomoAuxiliar.Field<string>("Id");
						if (!proteina.AreasDeContato.ContainsKey(string.Format("{0}-{1}", idAtmPrinc, idAtmAux)) && !proteina.AreasDeContato.ContainsKey(string.Format("{0}-{1}", idAtmAux, idAtmPrinc)))
						{
							var area = new AreaDeContato();
							area.Atomo1 = CarregarAtomo(atomoPrincipal);
							area.Atomo2 = CarregarAtomo(atomoAuxiliar);
							if (area.DistanciaEntreOsAtomos < 7)
								proteina.AreasDeContato.Add(string.Format("{0}-{1}", idAtmPrinc, idAtmAux), area);
						}
						iteracoes++;
					}
				}

			DateTime termino = DateTime.Now;

			TimeSpan tempo = termino - inicio;
			
			return proteina;
		}

		private Atomo CarregarAtomo(DataRow row)
		{
			Atomo atomo = new Atomo();
			atomo.Cordenada_X = Convert.ToDouble(row.Field<string>("Cartn_x").Replace(".", ","));
			atomo.Cordenada_Y = Convert.ToDouble(row.Field<string>("Cartn_y").Replace(".", ","));
			atomo.Cordenada_Z = Convert.ToDouble(row.Field<string>("Cartn_z").Replace(".", ","));

			atomo.Composto = row.Field<string>("label_atom_id");
			atomo.Cadeia = row.Field<string>("auth_asym_id");

			return atomo;
		}

		private Stream GenerateStreamFromStringXml(string xml)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(xml);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
		

	}
}
