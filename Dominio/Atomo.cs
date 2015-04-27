using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
	public class RaioDeVanDerWaals
	{
		public const double H = 1.20;
		public const double C = 1.70;
		public const double N = 1.55;
		public const double O = 1.52;
		public const double F = 1.47;
		public const double P = 1.80;
		public const double S = 1.80;
		public const double CI = 1.89;
	}

	public class Atomo
	{

		public double Cordenada_X { get; set; }
		public double Cordenada_Y { get; set; }
		public double Cordenada_Z { get; set; }

		public string Cadeia { get; set; }
		public string Composto { get; set; }

		private double RaioDaProbe = 1.4;

		public double raioDeVanDerWaals
		{
			get
			{
				double raio = 0;

				switch (Composto)
				{
					case "H":
						raio = RaioDeVanDerWaals.H;
						break;
					case "C" + "CA":
						raio = RaioDeVanDerWaals.C;
						break;
					case "N":
						raio = RaioDeVanDerWaals.N;
						break;
					case "O":
						raio = RaioDeVanDerWaals.O;
						break;
					case "F":
						raio = RaioDeVanDerWaals.F;
						break;
					case "P":
						raio = RaioDeVanDerWaals.P;
						break;
					case "S":
						raio = RaioDeVanDerWaals.S;
						break;
					case "CI":
						raio = RaioDeVanDerWaals.CI;
						break;
				}
				return raio + RaioDaProbe; 
			}
		}
	}
}
