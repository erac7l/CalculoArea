using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
	public class AreaDeContato
	{
		private const double PI = 3.1415926;

		public Atomo Atomo1 { get; set; }
		public Atomo Atomo2 { get; set; }
		
		public double DistanciaEntreOsAtomos
		{
			get 
			{
				return Math.Abs(Math.Sqrt(	Math.Pow(Atomo1.Cordenada_X - Atomo2.Cordenada_X, 2) +
											Math.Pow(Atomo1.Cordenada_Y - Atomo2.Cordenada_Y, 2) +
											Math.Pow(Atomo1.Cordenada_Z - Atomo2.Cordenada_Z, 2)));
			}
		}

		public double AreaAtomo1 { get { return CalcularArea(Atomo1.raioDeVanDerWaals, Atomo2.raioDeVanDerWaals); } }

		public double AreaAtomo2 { get { return CalcularArea(Atomo2.raioDeVanDerWaals, Atomo1.raioDeVanDerWaals); } }

		private double CalcularArea(double raioVDW1, double raioVDW2)
		{
			return 2 * PI * (Math.Pow(raioVDW1, 2) + Math.Pow(raioVDW2, 2)) -
						 PI * (raioVDW1 + raioVDW2) * DistanciaEntreOsAtomos *
						 (1 + Math.Pow(((raioVDW1 - raioVDW2) / DistanciaEntreOsAtomos), 2));
		}
	}
}
