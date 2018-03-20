using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Informatyka
{
    class XFM
    {
        private double dlugosc;
        private double[] wspolrzednaF;
        private double[] wartoscSily;
        private double[] wartoscMomentu;
        private double[] wspolrzednaM;

        //setter dlugosci belki
        public void setDlugosc(double dl) {dlugosc = dl;}

        //gettery wartosci tablic
        public double getFX(int i)
        {
            try { return wspolrzednaF[i]; }
            catch(IndexOutOfRangeException) { return -1.0; }
        }
        public double getF(int i)
        {
            try { return wartoscSily[i]; }
            catch (IndexOutOfRangeException) { return 0.0; }
        }
        public double getMX(int i)
        {
            try { return wspolrzednaM[i]; }
            catch (IndexOutOfRangeException) { return -1.0; }
        }
        public double getM(int i)
        {
            try { return wartoscMomentu[i]; }
            catch (IndexOutOfRangeException) { return 0.0; }
        }

        //gettery tablic
        public double[] getFX() { return wspolrzednaF; }
        public double[] getF() { return wartoscSily; }
        public double[] getMX() { return wspolrzednaM; }
        public double[] getM() { return wartoscMomentu; }

        //pomcnicze funkcje
        private double abs(double w)
        {
            return (w > 0) ? w : -w;
        }
        public void clear()
        {
            czyscSily();
            czyscMomenty();
        }
        public void czyscSily()
        {
            double[] tmp;

            tmp = new double[1];
            tmp[0] = -1.0;
            wspolrzednaF = tmp;

            tmp = new double[1];
            tmp[0] = 0.0;
            wartoscSily = tmp;
        }
        public void czyscMomenty()
        {
            double[] tmp;

            tmp = new double[1];
            tmp[0] = -1.0;
            wspolrzednaM = tmp;

            tmp = new double[1];
            tmp[0] = 0.0;
            wartoscMomentu = tmp;
        } 
        public double getMaxPrzekrojowy(bool pionowe = false, bool momenty = false)
        {
            double w = 1.0;

            if(!momenty)
            {
                double[,] tmp = getSilyPrzekrojowe(pionowe);

                for(int i = 0; i < tmp.Length / 3; i++)
                {
                    if (abs(tmp[1,i]) > w)
                        w = abs(tmp[1,i]);
                }

                for (int i = 0; i < tmp.Length / 3; i++)
                {
                    if (abs(tmp[2, i]) > w)
                        w = abs(tmp[1, i]);
                }
            }
            else
            {
                double[,] tmp = getMomentyPrzekrojowe();

                for (int i = 0; i < tmp.Length / 3; i++)
                {
                    if (abs(tmp[1, i]) > w)
                        w = abs(tmp[1, i]);
                }

                for (int i = 0; i < tmp.Length / 3; i++)
                {
                    if (abs(tmp[2, i]) > w)
                        w = abs(tmp[1, i]);
                }

                if (w == 0.0)
                    w = 1.0;
            }

            return w;
        }
        private double[] sortuj(double[] x, double[] y, bool drugaWartosc = false)
        {
            int k = 0;

            //sortowanie obu tablic od najmniejszego x
            for (int j = 0; j < x.Length - 1; j++)
            {
                double min = x[j];
                k = j;

                for (int i = j + 1; i < x.Length; i++)
                {
                    if (min > x[i])
                    {
                        k = i;
                        min = x[i];
                    }
                }

                //podmiana wartosci w tabeli x
                x[k] = x[j];
                x[j] = min;
                //podmiana wartosci w tabeli f
                min = y[k];
                y[k] = y[j];
                y[j] = min;
            }

            List<double> wx = new List<double>();
            List<double> wf = new List<double>();

            for (int i = 0; i < x.Count(); i++)
            {
                if (x[i] >= 0)
                {
                    wx.Add(x[i]);
                    wf.Add(y[i]);
                }
            }

            if (!drugaWartosc)
                return wx.ToArray();
            else
                return wf.ToArray();
        }
        private void setWartosci(string t1,string t2,bool momenty = false)
        {
            //przecinki zamieniamy na kropki aby uniknąć błędów zapisu
            t1 = t1.Replace(',', '.');
            t2 = t2.Replace(',', '.');
            t1 = t1.Replace(' ', '\n');
            t2 = t2.Replace(' ', '\n');

            //pomocnicze listy wartości (dynamiczne tablice)
            List<double> x = new List<double>();
            List<double> f = new List<double>();


            //Odczyt danych z textboxów
            if (t1.Length > 0)
            {

                var currentCulture = CultureInfo.InstalledUICulture;
                var numberFormat = (NumberFormatInfo)currentCulture.NumberFormat.Clone();
                numberFormat.NumberDecimalSeparator = ".";

                string tmp = "";

                //wprowadzanie odleglosci
                for (int k = 0; k < t1.Length; k++)
                {
                    if ((k>0 && t1[k] == ' '&& t1[k - 1]!=' ' && t1[k-1]!='\n') || (k>0 && t1[k] == '\n' && t1[k - 1] != ' ' && t1[k - 1] != '\n') || (k == t1.Length - 1))  //((t1[k] == ' ' && t1[k - 1] == ' ' && k > 0) || (t1[k] == '\n') || (k == t1.Length - 1))
                    {
                        if (k == t1.Length - 1)
                            tmp += t1[k];

                        double w = 0.0;
                        
                        if (Double.TryParse(tmp, NumberStyles.Any, numberFormat, out w))
                        {
                            if ((w <= dlugosc) && (w >= 0))
                                x.Add(w);
                            else
                                x.Add(-1.0);
                        }
                        else
                            x.Add(-1.0);
                        
                        tmp = "";
                    }
                    else
                        tmp += t1[k];
                }

                tmp = "";

                //wprowadzanie wartosci
                for (int k = 0; k < t2.Length; k++)
                {
                    if((k>0 && t2[k] == ' ' && t2[k - 1] != ' ' && t2[k - 1] != '\n') || (k>0 && t2[k] == '\n' && t2[k - 1] != ' ' && t2[k - 1] != '\n') || (k == t2.Length - 1))  //(((t1[k] == ' ') || (t1[k] == '\n') || (k == t1.Length - 1)) && t2[k - 1] != ' ' && t1[k - 1] != '\n' && k > 0)  //((t2[k] == ' '&& t2[k-1] == ' '&& k>0) || (t2[k] == '\n') || (k == t2.Length - 1)) // 
                    {
                        if (k == t2.Length - 1)
                            tmp += t2[k];

                        double w = 0.0;

                        if (Double.TryParse(tmp, NumberStyles.Any, numberFormat, out w))
                            f.Add(w);
                        else
                            f.Add(0.0);

                        tmp = "";
                    }
                    else
                        tmp += t2[k];
                }
            }


            //Tablice muszą mieć jednakową długość a nasze wartosci potrzebują dwóch zmiennych
            int I;
            I = (x.Count < f.Count) ? x.Count() :  f.Count();
            
            double[] wspolrzedna = new double[I];
            double[] wartosc = new double[I];
            

            //uzupelniamy tablice z siłami i odległościami           
             for (short i = 0; i < I; i++)
             {
                 if (f[i] != 0)
                    wspolrzedna[i] = x[i];
                 else
                     wspolrzedna[i] = -1.0;

                 wartosc[i] = f[i];
            }
           
            //sortowanie od najmniejszej odległości
            if(!momenty)
            {
                wspolrzednaF = sortuj(wspolrzedna, wartosc);
                wartoscSily = sortuj(wspolrzedna, wartosc, true);
            }
            else
            {
                wspolrzednaM = sortuj(wspolrzedna, wartosc);
                wartoscMomentu = sortuj(wspolrzedna, wartosc, true);
            }

        }

        //Funkcje liczące reakcje
        public double liczReakcje(bool utwierdzona,bool pionoweSily,bool drugaReakcja = false)
        {
            double wartoscReakcji = 0.0;

            if (!pionoweSily)//poziome siły
            {
                //niezależnie czy belka jest podparta czy utwierdzona mamy jedną reakcję poziomą w lewej podporze
                if (!drugaReakcja)
                {
                    for (int i = 0; i < wartoscSily.Length; i++)
                        wartoscReakcji -= wartoscSily[i];
                }
            }
            else //pionowe siły
            {
                if (!drugaReakcja)
                {
                    if(!utwierdzona)
                    {
                        //podparta lewa podpora (siła)
                        for (int i = 0; i < wartoscSily.Length; i++)
                            wartoscReakcji += wartoscSily[i];

                        wartoscReakcji =  -liczReakcje(false, true, true) - wartoscReakcji;
                    }
                    else
                    {
                        //utwierdzona lewa podpora (siła)
                        for (int i = 0; i < wartoscSily.Length; i++)
                            wartoscReakcji -= wartoscSily[i];
                    }
                }
                else
                {
                    if(!utwierdzona)
                    {
                        //podparta prawa podpora (siła)
                        for (int i = 0; i < wartoscSily.Length; i++)
                            wartoscReakcji -= wartoscSily[i] * ( wspolrzednaF[i]);

                        for (int i = 0; i < wartoscMomentu.Length; i++)
                            wartoscReakcji += wartoscMomentu[i];

                        wartoscReakcji = wartoscReakcji / dlugosc;
                    }
                    else
                    {
                        //utwierdzona lewa podpora (moment)
                        for (int i = 0; i < wartoscMomentu.Length; i++)
                            wartoscReakcji -= wartoscMomentu[i];

                        for (int i = 0; i < wartoscSily.Length; i++)
                            wartoscReakcji -= wartoscSily[i] * (wspolrzednaF[i]);                 
                    }
                }
            }

            return wartoscReakcji;
        }
        public void dodajReakcje(bool utwierdzona,bool pionowe)
        {
            double[] tmp;
            
            if(!utwierdzona) //dla podpartej
            {
                if(!pionowe)//pozioma reakcja
                { 
                    //dodajemy wartosc reakcji
                    tmp = new double[wartoscSily.Length + 1];
                    tmp[0] = liczReakcje(false, false);

                    for (int i = 1; i < tmp.Length; i++)
                        tmp[i] = wartoscSily[i-1];      

                    wartoscSily = tmp;


                    //dodajemy przylozenie
                    tmp = new double[wspolrzednaF.Length + 1];
                    tmp[0] = 0.0;

                    for (int i = 1; i < tmp.Length; i++)
                        tmp[i] = wspolrzednaF[i-1];

                    wspolrzednaF = tmp;
                }
                else//pionowa reakcja
                {
                    if(wartoscSily[0] != -1.0)
                    {
                        //dodajemy wartosc reakcji,
                        tmp = new double[wartoscSily.Length + 2];
                        tmp[0] = liczReakcje(false,true);

                        for (int i = 1; i < tmp.Length-1; i++)
                            tmp[i] = wartoscSily[i-1];

                        tmp[tmp.Length-1] = liczReakcje(false,true,true);

                        wartoscSily = tmp;
                    

                        //dodajemy przylozenie
                        tmp = new double[wspolrzednaF.Length + 2];
                        tmp[0] = 0.0;

                        for (int i = 1; i < tmp.Length-1; i++)
                            tmp[i] = wspolrzednaF[i-1];

                        tmp[tmp.Length - 1] = dlugosc;

                        wspolrzednaF = tmp; 
                    }
                    else
                    {
                        tmp = new double[2];
                        tmp[0] = liczReakcje(false, true);
                        tmp[1] = liczReakcje(false, true, true);
                        wartoscSily = tmp;

                        tmp = new double[2];
                        tmp[0] = 0.0;
                        tmp[1] = dlugosc;
                        wspolrzednaF = tmp;
                    }                     
                }
                
            }
            else //utwierdzona
            {
                if (!pionowe)//pozioma reakcja
                {
                    //dodajemy wartosc reakcji
                    tmp = new double[wartoscSily.Length + 1];
                    tmp[0] = liczReakcje(true,false);

                    for (int i = 1; i < tmp.Length; i++)
                        tmp[i] = wartoscSily[i-1];

                    wartoscSily = tmp;


                    //dodajemy przylozenie
                    tmp = new double[wspolrzednaF.Length + 1];
                    tmp[0] = 0.0;

                    for (int i = 1; i < tmp.Length; i++)
                        tmp[i] = wspolrzednaF[i-1];

                    wspolrzednaF = tmp;
                }
                else//pionowa reakcja
                {
                    if(wartoscMomentu[0] != -1.0)
                    {
                        //dodajemy wartosc reakcji
                        tmp = new double[wartoscSily.Length + 1];
                        tmp[0] = liczReakcje(true, true);

                        for (int i = 1; i < tmp.Length; i++)
                            tmp[i] = wartoscSily[i - 1];

                        wartoscSily = tmp;


                        //dodajemy przylozenie
                        tmp = new double[wspolrzednaF.Length + 1];
                        tmp[0] = 0.0;

                        for (int i = 1; i < tmp.Length; i++)
                            tmp[i] = wspolrzednaF[i - 1];

                        wspolrzednaF = tmp;


                        //dodajemy wartosc momentu reakcji
                        tmp = new double[wartoscMomentu.Length + 1];
                        tmp[0] = liczReakcje(true, true, true);

                        for (int i = 1; i < tmp.Length; i++)
                            tmp[i] = wartoscMomentu[i - 1];

                        wartoscMomentu = tmp;


                        //dodajemy przylozenie momentu
                        tmp = new double[wspolrzednaM.Length + 1];
                        tmp[0] = 0.0;

                        for (int i = 1; i < tmp.Length; i++)
                            tmp[i] = wspolrzednaM[i - 1];

                        wspolrzednaM = tmp;
                    }
                    else
                    {
                        tmp = new double[1];
                        tmp[0] = liczReakcje(true, true);
                        wartoscSily = tmp;

                        tmp = new double[1];
                        tmp[0] = 0.0;
                        wspolrzednaF = tmp;

                        tmp = new double[1];
                        tmp[0] = liczReakcje(true, true, true);
                        wartoscMomentu = tmp;

                        tmp = new double[1];
                        tmp[0] = 0.0;
                        wspolrzednaM = tmp;
                    }
                }
            }
           
        }

        //Funkcje zwracające wartości przekrojowe wpostaci tablic (wspolrzedna,wartość prawy,wartość lewy)
        public double[,] poszerz()
        {
          
            double[,] f = redukuj();
            double[,] m = redukuj(wspolrzednaM,wartoscMomentu);

            int k = 0;

            List<double> X = new List<double>();
            List<double> F = new List<double>();
            List<double> M = new List<double>();

            //Przepisujemy tablice do list
            for (int i = 0;i < (m.Length / 2); i++)
            {
                X.Add(m[0, i]);
                M.Add(m[1, i]);
                F.Add(0.0);
            }

            for (int i = 0; i < (f.Length / 2); i++)
            {
                X.Add(f[0, i]);
                F.Add(f[1, i]);
                M.Add(0.0);
            }

            //sortowanie obu tablic od najmniejszego x
            for (int j = 0; j < X.Count() - 1; j++)
            {
                double min = X[j];
                k = j;

                for (int i = j + 1; i < X.Count(); i++)
                {
                    if (min > X[i])
                    {
                        k = i;
                        min = X[i];
                    }
                }

                //podmiana wartosci w tabeli X
                X[k] = X[j];
                X[j] = min;
                //podmiana wartosci w tabelach F 
                min = F[k];
                F[k] = F[j];
                F[j] = min;
                //podmiana wartosci w tabelach M
                min = M[k];
                M[k] = M[j];
                M[j] = min;
            }

            //redukcja (jeśli odległości są takie same to suma wartości dwóch sąsiednich jest zapisana jako poprzedni element)
            for (int j = X.Count() - 1; j > 0; j--)
            {
                if (X[j - 1] == X[j])
                {
                    X[j] = -9.0;

                    F[j - 1] = F[j] + F[j - 1];
                    F[j] = 0.0;

                    M[j - 1] = M[j] + M[j - 1];
                    M[j] = 0.0;
                }
                else
                    k++;
            }

            double[,] tmp = new double[3, k];
            k = 0;

            //umieszczamy to do wynikowej tablicy
            for (int i = 0; i < X.Count(); i++)
            {
                if (X[i] >= 0.0)
                {
                    tmp[0, k] = X[i];
                    tmp[1, k] = F[i];
                    tmp[2, k] = M[i];
                    k++;
                }
            }


            //program dodaje jakieś zera niewiadomo skąd na końcu ten mechanizm je eliminuje
            k = 0;

            for(int i =  tmp.Length / 3 - 1; i>=0;i-- )
            {
                if (tmp[0, i] == 0.0 && tmp[1, i] == 0.0 && tmp[2, i] == 0.0)
                    k++;
                else
                    break;
            }
            
            if(k>0)
            {
                double[,] skracanie = new double[3, tmp.Length / 3 - k];

                for (int j = 0; j < skracanie.Length/3; j++)
                {
                    skracanie[0, j] = tmp[0, j];
                    skracanie[1, j] = tmp[1, j];
                    skracanie[2, j] = tmp[2, j];
                }

                tmp = skracanie;
            }


            return tmp;
        }
        private double[,] redukuj()
        {
            return redukuj(wspolrzednaF, wartoscSily);
        }
        public double[,] redukuj(double[] x, double[] y)
        {
            int k = 1;

            //redukcja (jeśli odległości są takie same to suma wartości dwóch sąsiednich jest zapisana jako poprzedni element)
            for (int j = x.Length - 1; j > 0; j--)
            {
                if (x[j - 1] == x[j])
                {
                    y[j - 1] = y[j] + y[j - 1];
                    y[j] = 0.0;
                    x[j] = -1.0;
                }
                else
                    k++;
            }

            double[,] tmp = new double[2, k];
            k = 0;

            //umieszczamy to do wynikowej tablicy
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] >= 0.0)
                {
                    tmp[0, k] = x[i];
                    tmp[1, k] = y[i];
                    k++;
                }
            }

            
            //program dodaje jakieś zera niewiadomo skąd na końcu ten mechanizm je eliminuje
            k = 0;

            for (int i = tmp.Length / 2 - 1; i >= 0; i--)
            {
                if (tmp[0, i] == 0.0 && tmp[1, i] == 0.0)
                    k++;
                else
                    break;
            }

            if (k > 0)
            {
                double[,] skracanie = new double[2, tmp.Length / 2 - k];

                for (int j = 0; j < skracanie.Length / 2; j++)
                {
                    skracanie[0, j] = tmp[0, j];
                    skracanie[1, j] = tmp[1, j];
                }

                tmp = skracanie;
            }

            return tmp;
        }
        public double[,] getSilyPrzekrojowe(bool pionowe = false)
        {
            
            double[,] tmp = redukuj();
            double[,] result;

            if (tmp.Length != 0)
            {
                
                double suma;
                int start = 0;

                //ostatni punkt przekroju jest w długości belki i należy go dodać jeżeli nie ma tam przyłożonych żadnych sił ani momentów 
                //to samo tyczy pierwszego punktu przekroju
                if (tmp[0, tmp.Length / 2 - 1] < dlugosc)
                {
                    if (tmp[0, 0] == 0.0)
                        result = new double[3, tmp.Length / 2 + 1];
                    else
                    {
                        result = new double[3, tmp.Length / 2 + 2];
                        result[0, 0] = 0.0;
                        result[1, 0] = 0.0;
                        result[2, 0] = 0.0;
                        start = 1;
                    }

                    result[0, (result.Length / 3) - 1] = dlugosc;
                    result[1, (result.Length / 3) - 1] = 0.0;
                    result[2, (result.Length / 3) - 1] = 0.0;
                }
                else
                {
                    if (tmp[0, 0] == 0.0)
                        result = new double[3, tmp.Length / 2];
                    else
                    {
                        result = new double[3, tmp.Length / 2 + 1];
                        result[0, 0] = 0.0;
                        result[1, 0] = 0.0;
                        result[2, 0] = 0.0;
                        start = 1;
                    }
                }


                //brzeg lewy (X|L|P)
                result[0, start] = tmp[0, 0];
                result[1, start] = 0.0;

                suma = 0.0;
                for (int j = 1; j < tmp.Length / 2; j++)
                    suma += tmp[1, j];
                if (!pionowe)
                    result[2, start] = suma;
                else
                    result[2, start] = -suma;


                //brzeg prawy (X|L|P)
                result[0, tmp.Length / 2 - 1] = tmp[0, tmp.Length / 2 - 1];
                result[2, tmp.Length / 2 - 1] = 0.0;

                suma = 0.0;
                for (int j = 0; j < tmp.Length / 2 - 1; j++)
                    suma -= tmp[1, j];
                if (!pionowe)
                    result[1, tmp.Length / 2 - 1] = suma;
                else
                    result[1, tmp.Length / 2 - 1] = -suma;



                for (int i = start + 1; i < tmp.Length / 2 - 1; i++)
                {
                    //odległości
                    result[0, i] = tmp[0, i];

                    //lewy przekrój myslowy wartośći
                    suma = 0.0;
                    for (int j = 0; j < i; j++)
                        suma -= tmp[1, j];
                    if (!pionowe)
                        result[1, i] = suma;//poziome
                    else
                        result[1, i] = -suma;//pionowe

                    //prawy przekrój myslowy wartosći
                    suma = 0.0;
                    for (int j = i + 1; j < tmp.Length / 2; j++)
                        suma += tmp[1, j];
                    if (!pionowe)
                        result[2, i] = suma;
                    else
                        result[2, i] = -suma;

                }
            }
            else
            {
                result = new double[3, 2];
                result[0, 0] = 0.0;
                result[0, 1] = 0.0;
                result[1, 0] = 0.0;
                result[1, 1] = 0.0;
            }
            return result;
        }
        public double[,] getMomentyPrzekrojowe()
        {

            double[,] tmp = poszerz();
            double[,] result;
            double suma;
            int start = 0;

            //ostatni punkt przekroju jest w długości belki i należy go dodać jeżeli nie ma tam przyłożonych żadnych sił ani momentów 
            //to samo tyczy pierwszego punktu przekroju
            try
            {
                if (tmp[0, tmp.Length / 3 - 1] < dlugosc)
                {
                    if (tmp[0, 0] == 0.0)
                        result = new double[3, tmp.Length / 3 + 1];
                    else
                    {
                        result = new double[3, tmp.Length / 3 + 2];
                        result[0, 0] = 0.0;
                        result[1, 0] = 0.0;
                        result[2, 0] = 0.0;
                        start = 1;
                    }

                    result[0, (result.Length / 3) - 1] = dlugosc;
                    result[1, (result.Length / 3) - 1] = 0.0;
                    result[2, (result.Length / 3) - 1] = 0.0;
                }
                else
                {
                    if (tmp[0, 0] == 0.0)
                        result = new double[3, tmp.Length / 3];
                    else
                    {
                        result = new double[3, tmp.Length / 3 + 1];
                        result[0, 0] = 0.0;
                        result[1, 0] = 0.0;
                        result[2, 0] = 0.0;
                        start = 1;
                    }
                }


                //brzeg lewy (X|L|P)
                result[0, start] = tmp[0, 0];
                result[1, start] = 0.0;

                suma = 0.0;
                for (int j = 1; j < tmp.Length / 3; j++)
                    suma -= tmp[1, j] * (tmp[0, j]);
                for (int j = 1; j < tmp.Length / 3; j++)
                    suma += tmp[2, j];

                result[2, start] = suma;


                //brzeg prawy (X|L|P)
                result[0, result.Length / 3 - 1] = tmp[0, tmp.Length / 3 - 1];
                result[2, result.Length / 3 - 1] = 0.0;

                suma = 0.0;
                for (int j = 0; j < tmp.Length / 3 - 1; j++)
                    suma += tmp[1, j] * (dlugosc - tmp[0, j]);
                for (int j = 1; j < tmp.Length / 3; j++)
                    suma += tmp[2, j];

                result[1, result.Length / 3 - 1] = suma;


                for (int i = 1; i < tmp.Length / 3; i++)
                {
                    //odległości
                    result[0, i] = tmp[0, i];

                    //lewy przekrój myslowy wartośći
                    suma = 0.0;

                    for (int j = 0; j < i; j++)
                        suma += tmp[1, j] * (tmp[0, i] - tmp[0, j]);
                    for (int j = 0; j < i; j++)
                        suma += tmp[2, j];

                    result[1, i] = suma;

                    //prawy przekrój myslowy wartosći
                    suma = 0.0;

                    for (int j = i; j < tmp.Length / 3; j++)
                        suma -= tmp[1, j] * (tmp[0, j] - tmp[0, i]);
                    for (int j = i; j < tmp.Length / 3; j++)
                        suma += tmp[2, j];

                    result[2, i] = suma;
                }
            }
            catch(IndexOutOfRangeException)
            {
                result = new double[3, 1];
                result[0, 0] = 0.0;
                result[1, 0] = 0.0;
                result[2, 0] = 0.0;

            }


            return result;
        }

        //ustawianie wartosci sil/momentow
        public void setF(string t1, string t2)
        {
                setWartosci(t1, t2);
        }
        public void setM(string t1, string t2)
        {
                setWartosci(t1, t2, true);
        }

        //konstruktor klasy
        public XFM()
        {
            clear();
            setDlugosc(100.0);
        }   
    }
}
