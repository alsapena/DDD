using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DDDTools;

namespace DDD
{
    public class Box
    {
        private MyPoint[] _myPoints = new MyPoint[9];


        private Color[] ColorLados;

        public Box(MyPoint p1, double profundidad, bool Coloreado)
        {
            if (Coloreado)
                ColorLados = new Color[6];
            _myPoints[0] = new MyPoint(p1.X, p1.Y, p1.Z);
            _myPoints[1] = new MyPoint(p1.X + profundidad, p1.Y, p1.Z);
            _myPoints[2] = new MyPoint(p1.X + profundidad, p1.Y + profundidad, p1.Z);
            _myPoints[3] = new MyPoint(p1.X, p1.Y + profundidad, p1.Z);
            _myPoints[4] = new MyPoint(p1.X, p1.Y, p1.Z + profundidad);
            _myPoints[5] = new MyPoint(p1.X + profundidad, p1.Y, p1.Z + profundidad);
            _myPoints[6] = new MyPoint(p1.X + profundidad, p1.Y + profundidad, p1.Z + profundidad);
            _myPoints[7] = new MyPoint(p1.X, p1.Y + profundidad, p1.Z + profundidad);
            _myPoints[8] = new MyPoint(p1.X + (double)profundidad / 2, p1.Y + (double)profundidad / 2,
                p1.Z + (double)profundidad / 2);
        }

        public MyPoint this[int punto]
        {
            get { return _myPoints[punto]; }
        }

        public MyPoint Centro
        {
            get { return _myPoints[8]; }
        }

        public void RotarX(int grados)
        {
            Tools.RotarX(_myPoints,Centro, grados);
        }
        public void RotarZ(int grados)
        {
            Tools.RotarZ(_myPoints,Centro, grados);
        }

        public void RotarY(int grados)
        {
            Tools.RotarY(_myPoints,Centro, grados);
        }

        public void Escalar(double Proporcion)
        {
            Tools.Escalar(_myPoints,Centro, Proporcion);
        }

        public int CantPuntos
        {
            get { return 8; }
        }

        public void DibEstructura(Graphics e, Color a, float tamanhoLinea)
        {
            Pen pluma = new Pen(a, tamanhoLinea);
            for (int i = 0; i < 2; i++)
            {
                PointF[] p = new PointF[4];
                for (int j = i * 4; j < 4 * (i + 1); j++)
                    p[j - i * 4] = new PointF((float)_myPoints[j].X, (float)_myPoints[j].Y);
                e.DrawLine(pluma, p[3], p[0]);
                for (int k = 0; k < 3; k++)
                    e.DrawLine(pluma, p[k], p[k + 1]);
            }

            for (int i = 0; i < 4; i++)
            {
                PointF pI = new PointF((float)_myPoints[i].X, (float)_myPoints[i].Y);
                PointF pF = new PointF((float)_myPoints[i + 4].X, (float)_myPoints[i + 4].Y);
                e.DrawLine(pluma, pI, pF);
            }
        }

        public int[] Cara(int a)
        {
            switch (a)
            {
                case 0:
                    return new int[] { 0, 1, 2, 3 };
                case 1:
                    return new int[] { 4, 5, 6, 7 };
                case 2:
                    return new int[] { 1, 2, 6, 5 };
                case 3:
                    return new int[] { 2, 3, 7, 6 };
                case 4:
                    return new int[] { 3, 0, 4, 7 };
                case 5:
                    return new int[] { 0, 1, 5, 4 };
            }
            return new int[] { 0 };
        }


        public Color ColorDeLado(int lado)
        {
            if (ColorLados == null) 
                return Color.Black;
            return ColorLados[lado];
        }

        public void ColorDeLado(int lado, Color x)
        {
            if (lado >= 6) throw new Exception("No exite el lado " + lado);
            ColorLados[lado] = x;
        }

        public void Colorear(Graphics e)
        {
            bool[] ValoresMasLejanos = new bool[8];
            int cantMayor = 0;
            double mayorCoord = 0;
            for (int i = 0; i < 8; i++)
                if (_myPoints[i].Z > mayorCoord)
                {
                    ValoresMasLejanos = new bool[8];
                    cantMayor = 1;
                    ValoresMasLejanos[i] = true;
                    mayorCoord = _myPoints[i].Z;
                }
                else if (_myPoints[i].Z == mayorCoord)
                {
                    ValoresMasLejanos[i] = true;
                    cantMayor++;
                }

            if (cantMayor == 1)
            {
                PointF[] c1 = new PointF[4];
                int val1 = 0;
                for (int i = 0; i < ValoresMasLejanos.Length; i++)
                    if (ValoresMasLejanos[i])
                    {
                        val1 = i;
                        break;
                    }

                for (int i = 0; i < 6; i++)
                {
                    int[] a = Cara(i);
                    Array.Sort(a);
                    if (!BusquedaBinaria(val1, a))
                    {
                        for (int j = 0; j < 4; j++)
                            c1[j] = _myPoints[Cara(i)[j]].ToPointF();
                        e.FillPolygon(new SolidBrush(ColorDeLado(i)), c1);
                    }
                }
            }
            else if (cantMayor == 2)
            {
                PointF[] c1 = new PointF[4];
                int val1 = 0;
                int val2 = 0;
                int k = 0;
                for (int i = 0; i < ValoresMasLejanos.Length; i++)
                    if (ValoresMasLejanos[i])
                    {
                        if (k == 0) val1 = i;
                        else val2 = i;
                        k++;
                        if (k == 2) break;
                    }


                for (int i = 0; i < 6; i++)
                {
                    int[] a = Cara(i);
                    Array.Sort(a);
                    if (!BusquedaBinaria(val1, a) && !BusquedaBinaria(val2, a))
                    {
                        for (int j = 0; j < 4; j++)
                            c1[j] = _myPoints[Cara(i)[j]].ToPointF();
                        e.FillPolygon(new SolidBrush(ColorDeLado(i)), c1);
                    }
                }

            }
            else if (cantMayor == 4)
            {
                PointF[] p = new PointF[4];
                int k = 0;
                if (EstanConsecutivos(ValoresMasLejanos))
                {
                    for (int i = 0; i < ValoresMasLejanos.Length; i++)
                        if (ValoresMasLejanos[i])
                        {
                            p[k] = _myPoints[i].ToPointF();
                            k++;
                        }
                }
                else
                {
                    for (int i = 0; i < ValoresMasLejanos.Length; i++)
                        if (ValoresMasLejanos[i])
                        {
                            p[k] = _myPoints[i].ToPointF();
                            k++;
                            if (k == 2) break;
                        }
                    for (int i = ValoresMasLejanos.Length - 1; i >= 0; i--)
                        if (ValoresMasLejanos[i])
                        {
                            p[k] = _myPoints[i].ToPointF();
                            k++;
                            if (k == 4) break;
                        }
                }

                e.FillPolygon(Brushes.AliceBlue, p);
            }
        }

        private bool EstanConsecutivos(bool[] valoresMasLejanos)
        {
            for (int i = 0; i < valoresMasLejanos.Length; i++)
            {
                if (valoresMasLejanos[i] && i + 4 < valoresMasLejanos.Length)
                {
                    for (int j = i; j < i + 4; j++)
                    {
                        if (!valoresMasLejanos[j])
                            return false;
                    }

                    return true;
                }
            }
            return false;
        }

        public static bool BusquedaBinaria(int x, int[] ordenados)
        {
            return BusquedaBinaria(x, ordenados, 0, ordenados.Length - 1);
        }

        private static bool BusquedaBinaria(int x, int[] ordenados, int com, int fin)
        {
            int medio = com + (fin - com) / 2;
            if (com > fin) return false;
            else if (x < ordenados[medio])
                return BusquedaBinaria(x, ordenados, com, medio - 1);
            else if (x > ordenados[medio])
                return BusquedaBinaria(x, ordenados, medio + 1, fin);
            return true;
        }

    }

}
