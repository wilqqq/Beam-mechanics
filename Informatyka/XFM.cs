using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Informatyka
{
    class XFM
    {
        private double length;
        private double[] forceCoordinate;
        private double[] forceValue;
        private double[] momentValue;
        private double[] momentCoordinate;

        //beam length setter
        public void setLength(double dl) {length = dl;}

        //array value getters
        public double getFX(int i)
        {
            try { return forceCoordinate[i]; }
            catch(IndexOutOfRangeException) { return -1.0; }
        }
        public double getF(int i)
        {
            try { return forceValue[i]; }
            catch (IndexOutOfRangeException) { return 0.0; }
        }
        public double getMX(int i)
        {
            try { return momentCoordinate[i]; }
            catch (IndexOutOfRangeException) { return -1.0; }
        }
        public double getM(int i)
        {
            try { return momentValue[i]; }
            catch (IndexOutOfRangeException) { return 0.0; }
        }

        //array getters
        public double[] getFX() { return forceCoordinate; }
        public double[] getF() { return forceValue; }
        public double[] getMX() { return momentCoordinate; }
        public double[] getM() { return momentValue; }

        //subsidiary functions
        private double abs(double w)
        {
            return (w > 0) ? w : -w;
        }
        public void clear()
        {
            forceClear();
            momentClear();
        }
        public void forceClear()
        {
            double[] tmp;

            tmp = new double[1];
            tmp[0] = -1.0;
            forceCoordinate = tmp;

            tmp = new double[1];
            tmp[0] = 0.0;
            forceValue = tmp;
        }
        public void momentClear()
        {
            double[] tmp;

            tmp = new double[1];
            tmp[0] = -1.0;
            momentCoordinate = tmp;

            tmp = new double[1];
            tmp[0] = 0.0;
            momentValue = tmp;
        } 
        public double getMaxIntersectionMoment(bool vertical = false, bool moments = false)
        {
            double w = 1.0;

            if(!moments)
            {
                double[,] tmp = getIntersectionForces(vertical);

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
                double[,] tmp = getIntersectionMoments();

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
        private double[] sort(double[] x, double[] y, bool secondValue = false)
        {
            int k = 0;

            //sort both arrays from the lowest x coordinate
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

                //change values in coordinates array
                x[k] = x[j];
                x[j] = min;
                //change values in forces array
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

            if (!secondValue)
                return wx.ToArray();
            else
                return wf.ToArray();
        }
        private void setValues(string t1,string t2,bool moments = false)
        {
            //change commas to dots to prevent bugs
            t1 = t1.Replace(',', '.');
            t2 = t2.Replace(',', '.');
            t1 = t1.Replace(' ', '\n');
            t2 = t2.Replace(' ', '\n');

            //subsidiary lists (dynamic arrays)
            List<double> x = new List<double>();
            List<double> f = new List<double>();


            //get values from textboxes
            if (t1.Length > 0)
            {

                var currentCulture = CultureInfo.InstalledUICulture;
                var numberFormat = (NumberFormatInfo)currentCulture.NumberFormat.Clone();
                numberFormat.NumberDecimalSeparator = ".";

                string tmp = "";

                //distance input
                for (int k = 0; k < t1.Length; k++)
                {
                    if ((k>0 && t1[k] == ' '&& t1[k - 1]!=' ' && t1[k-1]!='\n') || (k>0 && t1[k] == '\n' && t1[k - 1] != ' ' && t1[k - 1] != '\n') || (k == t1.Length - 1))  //((t1[k] == ' ' && t1[k - 1] == ' ' && k > 0) || (t1[k] == '\n') || (k == t1.Length - 1))
                    {
                        if (k == t1.Length - 1)
                            tmp += t1[k];

                        double w = 0.0;
                        
                        if (Double.TryParse(tmp, NumberStyles.Any, numberFormat, out w))
                        {
                            if ((w <= length) && (w >= 0))
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

                //values input
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


            //both arrays must have the same size
            int I;
            I = (x.Count < f.Count) ? x.Count() :  f.Count();
            
            double[] coordinate = new double[I];
            double[] value = new double[I];
            

            //fills an array with coordinates and values          
             for (short i = 0; i < I; i++)
             {
                 if (f[i] != 0)
                    coordinate[i] = x[i];
                 else
                     coordinate[i] = -1.0;

                 value[i] = f[i];
            }
           
            //sort from the lowest x coordinate
            if(!moments)
            {
                forceCoordinate = sort(coordinate, value);
                forceValue = sort(coordinate, value, true);
            }
            else
            {
                momentCoordinate = sort(coordinate, value);
                momentValue = sort(coordinate, value, true);
            }

        }

        //reaction calculating function
        public double reactionCalc(bool utwierdzona,bool areForcesVertical,bool secondReaction = false)
        {
            double reactionValues = 0.0;

            if (!areForcesVertical)//horizontal forces
            {
                //niezależnie czy belka jest podparta czy utwierdzona mamy jedną reakcję poziomą w lewej podporze
                if (!secondReaction)
                {
                    for (int i = 0; i < forceValue.Length; i++)
                        reactionValues -= forceValue[i];
                }
            }
            else //vertical forces
            {
                if (!secondReaction)
                {
                    if(!utwierdzona)
                    {
                        //podparta lewa podpora (siła)
                        for (int i = 0; i < forceValue.Length; i++)
                            reactionValues += forceValue[i];

                        reactionValues =  -reactionCalc(false, true, true) - reactionValues;
                    }
                    else
                    {
                        //utwierdzona lewa podpora (siła)
                        for (int i = 0; i < forceValue.Length; i++)
                            reactionValues -= forceValue[i];
                    }
                }
                else
                {
                    if(!utwierdzona)
                    {
                        //podparta prawa podpora (siła)
                        for (int i = 0; i < forceValue.Length; i++)
                            reactionValues -= forceValue[i] * ( forceCoordinate[i]);

                        for (int i = 0; i < momentValue.Length; i++)
                            reactionValues += momentValue[i];

                        reactionValues = reactionValues / length;
                    }
                    else
                    {
                        //utwierdzona lewa podpora (moment)
                        for (int i = 0; i < momentValue.Length; i++)
                            reactionValues -= momentValue[i];

                        for (int i = 0; i < forceValue.Length; i++)
                            reactionValues -= forceValue[i] * (forceCoordinate[i]);                 
                    }
                }
            }

            return reactionValues;
        }
        public void reactionAdd(bool utwierdzona,bool vertical)
        {
            double[] tmp;
            
            if(!utwierdzona) //dla podpartej
            {
                if(!vertical)//horizontal reaction
                { 
                    //add reaction value
                    tmp = new double[forceValue.Length + 1];
                    tmp[0] = reactionCalc(false, false);

                    for (int i = 1; i < tmp.Length; i++)
                        tmp[i] = forceValue[i-1];      

                    forceValue = tmp;


                    //dodajemy przylozenie
                    tmp = new double[forceCoordinate.Length + 1];
                    tmp[0] = 0.0;

                    for (int i = 1; i < tmp.Length; i++)
                        tmp[i] = forceCoordinate[i-1];

                    forceCoordinate = tmp;
                }
                else//vertical reaction
                {
                    if(forceValue[0] != -1.0)
                    {
                        //put in reaction value
                        tmp = new double[forceValue.Length + 2];
                        tmp[0] = reactionCalc(false,true);

                        for (int i = 1; i < tmp.Length-1; i++)
                            tmp[i] = forceValue[i-1];

                        tmp[tmp.Length-1] = reactionCalc(false,true,true);

                        forceValue = tmp;
                    

                        //dodajemy przylozenie
                        tmp = new double[forceCoordinate.Length + 2];
                        tmp[0] = 0.0;

                        for (int i = 1; i < tmp.Length-1; i++)
                            tmp[i] = forceCoordinate[i-1];

                        tmp[tmp.Length - 1] = length;

                        forceCoordinate = tmp; 
                    }
                    else
                    {
                        tmp = new double[2];
                        tmp[0] = reactionCalc(false, true);
                        tmp[1] = reactionCalc(false, true, true);
                        forceValue = tmp;

                        tmp = new double[2];
                        tmp[0] = 0.0;
                        tmp[1] = length;
                        forceCoordinate = tmp;
                    }                     
                }
                
            }
            else //utwierdzona
            {
                if (!vertical)//horizontal reaction
                {
                    //put in reaction value
                    tmp = new double[forceValue.Length + 1];
                    tmp[0] = reactionCalc(true,false);

                    for (int i = 1; i < tmp.Length; i++)
                        tmp[i] = forceValue[i-1];

                    forceValue = tmp;


                    //dodajemy przylozenie
                    tmp = new double[forceCoordinate.Length + 1];
                    tmp[0] = 0.0;

                    for (int i = 1; i < tmp.Length; i++)
                        tmp[i] = forceCoordinate[i-1];

                    forceCoordinate = tmp;
                }
                else//vertical reaction
                {
                    if(momentValue[0] != -1.0)
                    {
                        //add reaction value
                        tmp = new double[forceValue.Length + 1];
                        tmp[0] = reactionCalc(true, true);

                        for (int i = 1; i < tmp.Length; i++)
                            tmp[i] = forceValue[i - 1];

                        forceValue = tmp;


                        //dodajemy przylozenie
                        tmp = new double[forceCoordinate.Length + 1];
                        tmp[0] = 0.0;

                        for (int i = 1; i < tmp.Length; i++)
                            tmp[i] = forceCoordinate[i - 1];

                        forceCoordinate = tmp;


                        //add reaction moment value
                        tmp = new double[momentValue.Length + 1];
                        tmp[0] = reactionCalc(true, true, true);

                        for (int i = 1; i < tmp.Length; i++)
                            tmp[i] = momentValue[i - 1];

                        momentValue = tmp;


                        //dodajemy przylozenie momentu
                        tmp = new double[momentCoordinate.Length + 1];
                        tmp[0] = 0.0;

                        for (int i = 1; i < tmp.Length; i++)
                            tmp[i] = momentCoordinate[i - 1];

                        momentCoordinate = tmp;
                    }
                    else
                    {
                        tmp = new double[1];
                        tmp[0] = reactionCalc(true, true);
                        forceValue = tmp;

                        tmp = new double[1];
                        tmp[0] = 0.0;
                        forceCoordinate = tmp;

                        tmp = new double[1];
                        tmp[0] = reactionCalc(true, true, true);
                        momentValue = tmp;

                        tmp = new double[1];
                        tmp[0] = 0.0;
                        momentCoordinate = tmp;
                    }
                }
            }
           
        }

        //functions that return intersection values by arrays - they have 3 columns (coordinate, right side value, left side value)
        public double[,] extend()
        {
          
            double[,] f = reduce();
            double[,] m = reduce(momentCoordinate,momentValue);

            int k = 0;

            List<double> X = new List<double>();
            List<double> F = new List<double>();
            List<double> M = new List<double>();

            //rewrite arrays to lists
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

            //sort both arrays from the lowest x coordinate
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

                //change values of X array
                X[k] = X[j];
                X[j] = min;
                //change values of F array
                min = F[k];
                F[k] = F[j];
                F[j] = min;
                //change values of M array
                min = M[k];
                M[k] = M[j];
                M[j] = min;
            }

            // reduce array size - if two or more values have the same 'x coordinate', then sum them and put as a one force
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

            //putting everything to the final array
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


            //bugfix - prevent from adding zeros at the end of the array
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
                double[,] reducing = new double[3, tmp.Length / 3 - k];

                for (int j = 0; j < reducing.Length/3; j++)
                {
                    reducing[0, j] = tmp[0, j];
                    reducing[1, j] = tmp[1, j];
                    reducing[2, j] = tmp[2, j];
                }

                tmp = reducing;
            }


            return tmp;
        }
        private double[,] reduce()
        {
            return reduce(forceCoordinate, forceValue);
        }
        public double[,] reduce(double[] x, double[] y)
        {
            int k = 1;

            //reduce array size - if two or more values have the same 'x coordinate', then sum them and put as a one force
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

            //putting everything to the final array
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] >= 0.0)
                {
                    tmp[0, k] = x[i];
                    tmp[1, k] = y[i];
                    k++;
                }
            }

            
            //bugfix - prevent from adding zeros at the end of the array
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
                double[,] reducing = new double[2, tmp.Length / 2 - k];

                for (int j = 0; j < reducing.Length / 2; j++)
                {
                    reducing[0, j] = tmp[0, j];
                    reducing[1, j] = tmp[1, j];
                }

                tmp = reducing;
            }

            return tmp;
        }
        public double[,] getIntersectionForces(bool vertical = false)
        {
            
            double[,] tmp = reduce();
            double[,] result;

            if (tmp.Length != 0)
            {
                
                double sum;
                int start = 0;

                //ostatni punkt przekroju jest w długości belki i należy go dodać jeżeli nie ma tam przyłożonych żadnych sił ani momentów 
                //to samo tyczy pierwszego punktu przekroju
                if (tmp[0, tmp.Length / 2 - 1] < length)
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

                    result[0, (result.Length / 3) - 1] = length;
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


                //left side (X|L|R)
                result[0, start] = tmp[0, 0];
                result[1, start] = 0.0;

                sum = 0.0;
                for (int j = 1; j < tmp.Length / 2; j++)
                    sum += tmp[1, j];
                if (!vertical)
                    result[2, start] = sum;
                else
                    result[2, start] = -sum;


                //right side (X|L|R)
                result[0, tmp.Length / 2 - 1] = tmp[0, tmp.Length / 2 - 1];
                result[2, tmp.Length / 2 - 1] = 0.0;

                sum = 0.0;
                for (int j = 0; j < tmp.Length / 2 - 1; j++)
                    sum -= tmp[1, j];
                if (!vertical)
                    result[1, tmp.Length / 2 - 1] = sum;
                else
                    result[1, tmp.Length / 2 - 1] = -sum;



                for (int i = start + 1; i < tmp.Length / 2 - 1; i++)
                {
                    // X coordinates
                    result[0, i] = tmp[0, i];

                    //left side of virtual value intersection
                    sum = 0.0;
                    for (int j = 0; j < i; j++)
                        sum -= tmp[1, j];
                    if (!vertical)
                        result[1, i] = sum;//horizontal
                    else
                        result[1, i] = -sum;//vertical

                    //right side of virtual value intersection
                    sum = 0.0;
                    for (int j = i + 1; j < tmp.Length / 2; j++)
                        sum += tmp[1, j];
                    if (!vertical)
                        result[2, i] = sum;
                    else
                        result[2, i] = -sum;

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
        public double[,] getIntersectionMoments()
        {

            double[,] tmp = extend();
            double[,] result;
            double sum;
            int start = 0;

            //ostatni punkt przekroju jest w długości belki i należy go dodać jeżeli nie ma tam przyłożonych żadnych sił ani momentów 
            //to samo tyczy pierwszego punktu przekroju
            try
            {
                if (tmp[0, tmp.Length / 3 - 1] < length)
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

                    result[0, (result.Length / 3) - 1] = length;
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


                //left side (X|L|R)
                result[0, start] = tmp[0, 0];
                result[1, start] = 0.0;

                sum = 0.0;
                for (int j = 1; j < tmp.Length / 3; j++)
                    sum -= tmp[1, j] * (tmp[0, j]);
                for (int j = 1; j < tmp.Length / 3; j++)
                    sum += tmp[2, j];

                result[2, start] = sum;


                //right side (X|L|R)
                result[0, result.Length / 3 - 1] = tmp[0, tmp.Length / 3 - 1];
                result[2, result.Length / 3 - 1] = 0.0;

                sum = 0.0;
                for (int j = 0; j < tmp.Length / 3 - 1; j++)
                    sum += tmp[1, j] * (length - tmp[0, j]);
                for (int j = 1; j < tmp.Length / 3; j++)
                    sum += tmp[2, j];

                result[1, result.Length / 3 - 1] = sum;


                for (int i = 1; i < tmp.Length / 3; i++)
                {
                    //distance
                    result[0, i] = tmp[0, i];

                    //left side of virtual value intersection
                    sum = 0.0;

                    for (int j = 0; j < i; j++)
                        sum += tmp[1, j] * (tmp[0, i] - tmp[0, j]);
                    for (int j = 0; j < i; j++)
                        sum += tmp[2, j];

                    result[1, i] = sum;

                    //right side of virtual value intersection
                    sum = 0.0;

                    for (int j = i; j < tmp.Length / 3; j++)
                        sum -= tmp[1, j] * (tmp[0, j] - tmp[0, i]);
                    for (int j = i; j < tmp.Length / 3; j++)
                        sum += tmp[2, j];

                    result[2, i] = sum;
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

        //setting forces/moments values
        public void setF(string t1, string t2)
        {
                setValues(t1, t2);
        }
        public void setM(string t1, string t2)
        {
                setValues(t1, t2, true);
        }

        //class constructor
        public XFM()
        {
            clear();
            setLength(100.0);
        }   
    }
}
