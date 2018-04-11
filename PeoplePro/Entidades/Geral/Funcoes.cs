using System;

namespace PeoplePro.Entidades.Geral
{
    class Funcoes
    {
        //Converte graus em radianos
        public double DegRad(double x)
        {
            return x * ((4 * Math.Atan(1)) / 180);
        }

        //Calcula a distancia em quilometros (1.00000) entre dois pontos atraves de latitude e longitude
        public double CalcDistancia(double Lat1, double Lon1, double Lat2, double Lon2)
        {
            double er = 0;
            double pr = 0;
            double fl = 0;
            double F = 0;
            double G = 0;
            double L = 0;
            double S = 0;
            double C = 0;
            double W = 0;
            double R = 0;
            double D = 0;
            double H1 = 0;
            double H2 = 0;

            er = 6378.1370000;
            pr = 6356.7523142;

            fl = (er - pr) / er;

            F = (DegRad(Lat1) + DegRad(Lat2)) / 2;

            G = (DegRad(Lat1) - DegRad(Lat2)) / 2;
            L = (DegRad(Lon1) - DegRad(Lon2)) / 2;

            S = (Math.Pow(Math.Sin(G), 2)) * Math.Pow(Math.Cos(L), 2) + Math.Pow(Math.Cos(F), 2) * Math.Pow(Math.Sin(L), 2);
            C = (Math.Pow(Math.Cos(G), 2)) * Math.Pow(Math.Cos(L), 2) + Math.Pow(Math.Sin(F), 2) * Math.Pow(Math.Sin(L), 2);

            W = Math.Atan(Math.Sqrt(S / C));

            R = Math.Sqrt(S * C) / W;

            D = 2 * W * er;

            H1 = (3 * R - 1) / (2 * C);
            H2 = (3 * R + 1) / (2 * S);

            return (D * (1 + fl * H1 * Math.Pow(Math.Sin(F), 2) * Math.Pow(Math.Cos(G), 2) - fl * H2 * Math.Pow(Math.Cos(F), 2) * Math.Pow(Math.Sin(G), 2)) / 1.00000);
        }

    }
}
