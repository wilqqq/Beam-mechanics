using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Informatyka
{
    public partial class Form1 : Form
    {
        private double dlugosc = 100;
        private XFM O = new XFM();
        private PictureBox pictureBox1 = new PictureBox();
        private PictureBox pictureBox2 = new PictureBox();
        private PictureBox WykresyBox = new PictureBox();

        //buttony
        private void button1_Click(object sender, EventArgs e)
        {
            //wprowadzamy wartosci 
            wprowadzWartosci();

            //wypisyawnie reakcji
            if (textBox1.Text.Length > 0 || textBox5.Text.Length > 0)
                PrezentujReakcje(wsuw.Checked, pionowe.Checked);

            //rysowanie sił na wykresie
            WlaczRysowanie();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox5.Clear();
            textBox4.Clear();

            O.clear();

            label4.Text = "Spis reakcji:";

            WlaczRysowanie();
            RysujPusteWykresy();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            /* TO JEST KOD KONTROLNY DO SZUKANIA BŁĘDÓW
            label4.Text = O.getFX().Length.ToString() + " : " + O.getF().Length.ToString() + "\n" + O.getMX().Length.ToString() + " : " + O.getM().Length.ToString() + "\n";

            label4.Text += "SIŁY\n";
            for (int i = 0; i < O.getFX().Length; i++)
                label4.Text += O.getFX(i).ToString() + "|" + O.getF(i).ToString() + "\n";
            label4.Text += "MOMENTY\n";
            for (int i = 0; i < O.getMX().Length; i++)
                label4.Text+= O.getMX(i).ToString() + "|" + O.getM(i).ToString() + "\n";
            */

            label4.Text = "Spis reakcji";

            if (((textBox1.Text.Length > 0 && textBox2.Text.Length > 0) || (textBox4.Text.Length > 0 && textBox5.Text.Length > 0)) &&( O.getFX(0)!=-1.0|| O.getMX(0) != -1.0))
            {
                if (!pionowe.Checked)
                {
                    /*
                    label4.Text += "Poredukowane\n";
                    for (int i = 0; i < O.redukuj(O.getFX(), O.getF()).Length / 2; i++)
                        label4.Text += O.redukuj(O.getFX(), O.getF())[0, i].ToString() + "|" + O.redukuj(O.getFX(), O.getF())[1, i].ToString() + "\n";
                     */

                    label4.Text += "\nPrzekrojowe siły poziome X|L|P\n";
                    for (int i = 0; i < O.getSilyPrzekrojowe().Length / 3; i++)
                        label4.Text += "Odległość:" + O.getSilyPrzekrojowe()[0, i].ToString() + " [m] |  " + O.getSilyPrzekrojowe()[1, i].ToString() + "[N] Lewa strona|  " + O.getSilyPrzekrojowe()[2, i].ToString() + "[N] Prawa strona\n";
                }
                else
                {
                    /*
                    label4.Text += "Poredukowane MOMENTY\n";
                    for (int i = 0; i < O.redukuj(O.getMX(),O.getM()).Length / 2; i++)
                        label4.Text += O.redukuj(O.getMX(), O.getM())[0, i].ToString() + "|" + O.redukuj(O.getMX(), O.getM())[1, i].ToString() + "\n";

                    label4.Text += "Poszerzone\n";
                    for (int i = 0; i < O.poszerz().Length / 3; i++)
                        label4.Text += O.poszerz()[0, i].ToString() + "|" + O.poszerz()[1, i].ToString() + "|" + O.poszerz()[2, i].ToString() + "\n";
                    */

                    label4.Text += "Maksyymalna siła przekrojowa: " + O.getMaxPrzekrojowy(true, false) + "\n";
                    label4.Text += "Maksymalny moment przekrojowy: " + O.getMaxPrzekrojowy(true, true) + "\n";
                    label4.Text += "Przekrojowe pionowe (siły) X|L|P\n";

                    for (int i = 0; i < O.getSilyPrzekrojowe(true).Length / 3; i++)
                        label4.Text += "Odległość:" + O.getSilyPrzekrojowe()[0, i].ToString() + " [m] |  " + O.getSilyPrzekrojowe()[1, i].ToString() + "[N] Lewa strona|  " + O.getSilyPrzekrojowe()[2, i].ToString() + "[N] Prawa strona\n";

                    label4.Text += "Przekrojowe pionowe (momenty) X|L|P\n";
                    for (int i = 0; i < O.getMomentyPrzekrojowe().Length / 3; i++)
                        label4.Text += O.getMomentyPrzekrojowe()[0, i].ToString() + " [m]  |" + O.getMomentyPrzekrojowe()[1, i].ToString() + "[Nm] Lewa strona|  " + O.getMomentyPrzekrojowe()[2, i].ToString() + "[Nm] Prawa strona\n";
                }

                WlaczWykresy();
            }       
        }

        //pomocnicze
        private void PrezentujReakcje(bool utwierdzona,bool pionowe)
        {
        //prezentowanie reakcji
        if (!utwierdzona)//radioButton2ta
        {
                if (!pionowe) //poziome
                    label4.Text = "Spis reakcji:\nReakcja pozioma w lewej podporze ma wartość: " + O.getF(0) + "[N]\nBrak reakcji w prawej podporze";
            else //pionowe
                label4.Text = "Spis reakcji:\nReakcja pionowa w lewej podporze ma wartość: " + O.getF(0) + "[N]\nReakcja pionowa w prawej podporze ma wartość: " + O.getF(O.getF().Length - 1) + "[N]";
        }
        else //utwierdzona
        {
            if (!pionowe) //poziome
                label4.Text = "Spis reakcji:\nReakcja pozioma w lewej podporze ma wartość: " + O.getF(0) + "[N]\nBrak reakcji w prawej podporze";
            else //pionowe
                label4.Text = "Spis reakcji:\nReakcja pionowa w lewej podporze ma wartość: " + O.getF(0) + "[N]\nMoment umocnienia w lewej podporze ma wartość: " + O.getM(0) + "[Nm]";
        }
    }
        private void wprowadzWartosci()
        {
            //Sprzątamy stare wartości
            O.clear();

            //wprowadzamy Siły
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
                O.setF(textBox1.Text, textBox2.Text);
            else
                O.czyscSily();

            //czyścimy textboxy by wypełnić je tylko prawidłowymi wartościami
            textBox1.Clear();
            textBox2.Clear();

            //ponownie wypełniamy textboxy istniejacymi siłamy
            for (int i = 0; i < O.getFX().Count(); i++)
            {
                if(O.getF(i)!=0)
                {
                    textBox1.AppendText(O.getFX(i).ToString() + "\n");
                    textBox2.AppendText(O.getF(i).ToString() + "\n");
                }
            }

            //Wprowadzamy momenty jeśli występują
            if (pionowe.Checked && textBox4.Text.Length > 0 && textBox5.Text.Length > 0)
                O.setM(textBox5.Text, textBox4.Text);
            else
                O.czyscMomenty();

            //czyścimy textboxy by wypełnić je tylko prawidłowymi wartościami
            if (pionowe.Checked)
            {
                textBox5.Clear();
                textBox4.Clear();

                //ponownie wypełniamy textboxy istniejacymi momentami
                for (int i = 0; i < O.getMX().Count(); i++)
                {
                    if(O.getM(i)!=0)
                    {
                        textBox5.AppendText(O.getMX(i).ToString() + "\n");
                        textBox4.AppendText(O.getM(i).ToString() + "\n");
                    }
                }
            }

            //dodawanie reakcji do tablic wartości
            if (textBox1.Text.Length > 0 || textBox5.Text.Length > 0)
                O.dodajReakcje(wsuw.Checked, pionowe.Checked);
        }

        //rysowanie
        private void RysujPusteWykresy()
        {
            WykresyBox.Location = new Point(520, 260);
            WykresyBox.Size = new Size(500, 300);
            WykresyBox.BackColor = Color.LightBlue;
            WykresyBox.Visible = true;
       //     WykresyBox.Parent = pictureBox2;
            Controls.Add(WykresyBox);

            WykresyBox.Paint += new System.Windows.Forms.PaintEventHandler(RysujOsie);
        }
        private void WlaczRysowanie()
        {

            pictureBox1.Location = new Point(520, 35);
            pictureBox1.Size = new Size(400, 200);
            pictureBox1.BackColor = Color.Blue;
            pictureBox1.Visible = true;
            pictureBox1.Parent = pictureBox2;
            pictureBox2.Size = new Size(400, 200);
            Controls.Add(pictureBox1);

            if (podpar.Checked)
            {
                pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(RysujBelkePodparta);
                pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(RysujSily);
                if (pionowe.Checked)
                    pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(RysujMomenty);
            }
            if (wsuw.Checked)
            {
                pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(RysujBelkeUtwierdzona);
                pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(RysujSily);
                if (pionowe.Checked)
                    pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(RysujMomenty);
            }
        }
        private void RysujSily(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //rysowanie sily w punkcie o odleglej od poczatku belki od fx

            if (textBox1.Text.Length > 0 && O.getF().Length == O.getFX().Length)
            {
                Color kolor = new Color();
                string znak;

                for (int a = 0; a < O.getFX().Length; a++)
                {
                    int fx = Convert.ToInt32(O.getFX(a) * 340.0 / dlugosc) + 30;

                    if ((poziome.Checked && a == 0) || (pionowe.Checked && (a == 0 || (a == O.getFX().Length - 1 && !wsuw.Checked))))//Wyróżnianie poziomych reakcji  &&  wyróżnienie pionowych reakcji
                    {
                        kolor = Color.Red;
                        znak = "R";
                    }
                    else
                    {
                        kolor = Color.Blue;
                        znak = "F";
                    }


                    if (fx >= 30)
                    {
                        if (O.getF(a) > 0)//silu dodatnie
                        {
                            if (pionowe.Checked)//sila pionowa dodatnia
                            {
                                g.FillRectangle(new SolidBrush(kolor), fx - 4, 115, 8, 60);

                                PointF[] points = new PointF[] { new PointF { X = fx + 10, Y = 115 }, new PointF { X = fx - 10, Y = 115 }, new PointF { X = fx, Y = 100 } };
                                g.FillPolygon(new SolidBrush(kolor), points);

                                g.DrawString(znak + a, new Font("ArialBold", 8), new SolidBrush(Color.Black), fx - 5, 175 + 10 * (a % 5));
                            }

                            if (poziome.Checked)//sila pozioma dodatnia
                            {
                                g.FillRectangle(new SolidBrush(kolor), fx - 65, 96, 60, 8);

                                PointF[] points = new PointF[] { new PointF { X = fx - 15, Y = 110 }, new PointF { X = fx - 15, Y = 90 }, new PointF { X = fx, Y = 100 } };
                                g.FillPolygon(new SolidBrush(kolor), points);

                                g.DrawString(znak + a, new Font("ArialBold", 8), new SolidBrush(Color.Black), fx - 5, 125 + 10 * (a % 5));
                            }
                        }

                        if (O.getF(a) < 0)
                        {
                            if (pionowe.Checked)//sila pionowa ujemna
                            {
                                g.FillRectangle(new SolidBrush(kolor), fx - 4, 30, 8, 60);

                                PointF[] points = new PointF[] { new PointF { X = fx + 10, Y = 85 }, new PointF { X = fx - 10, Y = 85 }, new PointF { X = fx, Y = 100 } };
                                g.FillPolygon(new SolidBrush(kolor), points);

                                g.DrawString(znak + (a), new Font("ArialBold", 8), new SolidBrush(Color.Black), fx - 5, 15 - 10 * (a % 5));
                            }

                            if (poziome.Checked)//sila pozioma ujemna
                            {
                                g.FillRectangle(new SolidBrush(kolor), fx + 5, 96, 60, 8);

                                PointF[] points = new PointF[] { new PointF { X = fx + 15, Y = 110 }, new PointF { X = fx + 15, Y = 90 }, new PointF { X = fx, Y = 100 } };
                                g.FillPolygon(new SolidBrush(kolor), points);

                                g.DrawString(znak + (a), new Font("ArialBold", 8), new SolidBrush(Color.Black), fx - 5, 125 - 10 * (a % 5));

                            }
                        }
                    }
                }
            }
        }
        private void RysujMomenty(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //rysowanie sily w punkcie o odleglej od poczatku belki od fx
            if (textBox5.Text.Length > 0 && O.getM().Length == O.getMX().Length)
            {
                Color kolor = new Color();
                string znak;

                for (int a = 0; a < O.getMX().Length; a++)
                {
                    int fx = Convert.ToInt32(O.getMX(a) * 340.0 / dlugosc) + 30;

                    if (a == 0 && wsuw.Checked)//Wyróżnianie momentu utwardzenia 
                    {
                        kolor = Color.DarkGray;
                        znak = "U";
                    }
                    else
                    {
                        kolor = Color.Cyan;
                        znak = "M";
                    }


                    if (fx >= 30)
                    {
                        if (O.getM(a) > 0)//Moment dodatni
                        {
                            g.DrawLine(new Pen(kolor, 1), fx, 95, fx, 105);
                            g.DrawArc(new Pen(kolor, 2), fx - 15, 100 - 15, 30, 30, 180, 220);

                            PointF[] points = new PointF[] { new PointF { X = fx - 22, Y = 100 }, new PointF { X = fx - 8, Y = 100 }, new PointF { X = fx - 15, Y = 110 } };
                            g.FillPolygon(new SolidBrush(kolor), points);

                            g.DrawString(znak + (a), new Font("ArialBold", 8), new SolidBrush(Color.Black), fx - 5, 130 + 10 * (a % 5));

                        }

                        if (O.getM(a) < 0)//moment ujemny
                        {
                            g.DrawLine(new Pen(kolor, 1), fx, 95, fx, 105);
                            g.DrawArc(new Pen(kolor, 2), fx - 15, 100 - 15, 30, 30, 0, 120);

                            PointF[] points = new PointF[] { new PointF { X = fx + 22, Y = 100 }, new PointF { X = fx + 8, Y = 100 }, new PointF { X = fx + 15, Y = 90 } };
                            g.FillPolygon(new SolidBrush(kolor), points);

                            g.DrawString(znak + (a), new Font("ArialBold", 8), new SolidBrush(Color.Black), fx - 5, 160 - 10 * (a % 5));

                        }
                    }
                }
            }
        }
        private void RysujBelkePodparta(object sender, PaintEventArgs e)
        {
            const int i = 20;

            Graphics g = e.Graphics;

            //czyszczenie obszaru rysowania
            g.Clear(Color.SeaShell);

            //lewa podpora
            g.DrawEllipse(System.Drawing.Pens.Black, 30 - 4, 100 - 4, 8, 8);
            g.DrawLine(System.Drawing.Pens.Black, 30, 100, 30 + i, 100 + i);
            g.DrawLine(System.Drawing.Pens.Black, 30 - i, 100 + i, 30, 100);
            g.DrawLine(System.Drawing.Pens.Black, 30 - i, 100 + i, 30 + i, 100 + i);

            //prawa podpora
            g.DrawEllipse(System.Drawing.Pens.Black, 370 - 4, 100 - 4, 8, 8);
            g.DrawLine(System.Drawing.Pens.Black, 370, 100, 370 + i, 100 + i);
            g.DrawLine(System.Drawing.Pens.Black, 370 - i, 100 + i, 370, 100);
            g.DrawLine(System.Drawing.Pens.Black, 370 - i, 100 + i, 370 + i, 100 + i);
            g.DrawLine(System.Drawing.Pens.Black, 370 - i, 100 + i + 5, 370 + i, 100 + i + 5);

            //belka
            Pen p = new Pen(Color.Black, 3);
            g.DrawLine(p, 30, 100, 370, 100);


            //kreskowanie
            for (int j = 1; j < 6; j++)
            {
                g.DrawLine(System.Drawing.Pens.Black, 30 - i + (j - 1) * 8 + 3, 100 + i, 30 - i + j * 8 + 3, 100 + i + 8);
                g.DrawLine(System.Drawing.Pens.Black, 370 - i + (j - 1) * 8 + 3, 100 + i + 5, 370 - i + j * 8 + 3, 100 + i + 8 + 5);
            }
        }
        private void RysujBelkeUtwierdzona(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //czyszczenie obszaru rysowania
            g.Clear(Color.SeaShell);

            //belka
            Pen p = new Pen(Color.Black, 3);
            g.DrawLine(p, 30, 100, 370, 100);

            //umocnienie
            g.DrawLine(System.Drawing.Pens.Black, 30, 60, 30, 140);
            //kreskowanie
            for (int j = 0; j < 7; j++)
                g.DrawLine(System.Drawing.Pens.Black, 30, 135 - j * 10, 10, 115 - j * 10);

        }

        //radiobuttony
        private void radioButton1_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }
        private void radioButton2_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (pionowe.Checked)
            {
                groupBox3.Visible = true;
                button1.Text = "Dodaj Sily\ni Momenty";
            }

            else
            {
                groupBox3.Visible = false;
                button1.Text = "Dodaj Sily";
            }

            button3_Click(sender, e);
        }

        //textboxy
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            var currentCulture = System.Globalization.CultureInfo.InstalledUICulture;
            var numberFormat = (System.Globalization.NumberFormatInfo)currentCulture.NumberFormat.Clone();
            numberFormat.NumberDecimalSeparator = ".";

            double w = 0;

            if (double.TryParse(textBox3.Text, NumberStyles.Any, numberFormat, out w) && w > 0)
            {
                dlugosc = w;
                O.setDlugosc(dlugosc);
            }
            else
                textBox3.Text = dlugosc.ToString();
        }

        //labele
        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Red;
            label4.BackColor = Color.LightGray;
        }
        private void label4_MouseHover(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Red;
            label4.BackColor = Color.LightGray;
        }
        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Black;
            label4.BackColor = Color.SeaShell;
        }

        //rysowanie wykresow
        private void WlaczWykresy()
        {
            WykresyBox.Location = new Point(520, 260);
            WykresyBox.Size = new Size(500, 300);
            WykresyBox.BackColor = Color.LightBlue;
            WykresyBox.Visible = true;
            WykresyBox.Parent = pictureBox2;
            Controls.Add(WykresyBox);

            WykresyBox.Paint += new System.Windows.Forms.PaintEventHandler(RysujOsie);


                if ((textBox1.Text.Length > 0 && textBox2.Text.Length > 0) || (textBox4.Text.Length > 0 && textBox5.Text.Length > 0))
                {
                    if (!pionowe.Checked)
                        WykresyBox.Paint += new System.Windows.Forms.PaintEventHandler(RysujWykresPoziome);
                    else
                        WykresyBox.Paint += new System.Windows.Forms.PaintEventHandler(RysujWykresPionowe);
                }
        }
       /* private void RysujOsieWykres(object sender, PaintEventArgs e)
        {
            //funkja uruchamiana po dodaniu sil i wcisnieciu "Oblicz"
            Graphics g = e.Graphics;
            //czyszczenie obszaru rysowania
            g.Clear(Color.LightGray);

            //osie wykresu                                                                    STRZAŁKI!!!!
            Pen p = new Pen(Color.Black, 2);
            g.DrawLine(p, 30, 30, 30, 270);
            g.DrawLine(p, 30, 150, 370, 150);
            
            for(int a=0; a<O.getFX().Length; a++)
            {
                int fx = Convert.ToInt32(O.getFX(a) * 340.0 / dlugosc) + 30;
                //zamiast wartosci 50 ponizej, trzeba wpisac wartosc maksymalna sumy sil dla lewych i prawych stron
                int fy = Convert.ToInt32(150-O.getF(a) * 150.0 / 50 );
                //zaznaczanie na osi x
                g.FillRectangle(new SolidBrush(Color.Red), fx , 150, 6, 6);
                //zaznaczanie na osi x i y
                g.FillRectangle(new SolidBrush(Color.Blue), fx, fy, 6, 6);
                //rysowanie pionowych linii
                g.DrawLine(System.Drawing.Pens.Black,fx,150,fx,fy);
                if(a>0)
                {
                    //poprzednie wartosci, rysowanie lini z poprzednimi wartosciami
                    p.Color = Color.OrangeRed;
                    int fxp = Convert.ToInt32(O.getFX(a-1) * 340.0 / dlugosc) + 30;
                    //tu tez zamienic 50 na maksymalna wartosc
                    int fyp = Convert.ToInt32(150-O.getF(a-1) * 150.0 / 50);
                    
                    g.DrawLine(p, fxp, fyp, fx, fy);

                }

            }


        }*/
        private void RysujOsie(object sender, PaintEventArgs e)
        {
            //funkja uruchamiana po dodaniu sil i wcisnieciu "Oblicz"
            Graphics g = e.Graphics;
            //czyszczenie obszaru rysowania
            g.Clear(Color.LightGray);

            //strzalki
            PointF[] strzalkap = { new PointF(375, 145), new PointF(375, 155), new PointF(390, 150) };
            PointF[] strzalkad = { new PointF(25,270 ), new PointF(35, 270), new PointF(30, 285) };
            PointF[] strzalkadp = { new PointF(365, 270), new PointF(375, 270), new PointF(370, 285) };


            g.FillPolygon(new SolidBrush(Color.Black), strzalkap);
            g.FillPolygon(new SolidBrush(Color.Black), strzalkad);
            g.FillPolygon(new SolidBrush(Color.Black), strzalkadp);


            //osie wykresu                                  
            Pen p = new Pen(Color.Black, 2);
            g.DrawLine(p, 30, 30, 30, 270);
            g.DrawLine(p, 30, 150, 375, 150);
            g.DrawLine(p, 370, 30, 370, 270);
        }
        private void RysujWykresPoziome(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            double skalaY;
            double skalaX;

            try
            {
                skalaY = 90.0 / O.getMaxPrzekrojowy();
                skalaX = 340 / dlugosc;
            }
            catch(OverflowException)
            {
                skalaY = 0.0;
                skalaX = 0.0;
                label4.Text = " Błąd wprowadzania danych !!!";
                button3_Click(sender,e);
            }

            

            double[,] tmp = O.getSilyPrzekrojowe();

            PointF[] points = new PointF[(2 * tmp.Length / 3)];

            points[0].X = Convert.ToInt32(tmp[0, 0] * skalaX) + 30;
            points[0].Y = Convert.ToInt32(tmp[1, 0] * skalaY) + 150;
           

            for (int i = 1; i < points.Length; i++)
            {
                points[i].X = Convert.ToInt32(tmp[0, i /2] * skalaX) + 30;

                if (i % 2 == 0)
                    points[i].Y = Convert.ToInt32(tmp[1, i /2] * skalaY) + 150;
                else
                    points[i].Y = Convert.ToInt32(tmp[2, i /2] * skalaY) + 150;
            }

            points[points.Length-1].X = Convert.ToInt32(tmp[0, tmp.Length / 3 - 1] * skalaX) + 30;
            points[points.Length-1].Y = Convert.ToInt32(tmp[2, tmp.Length / 3 - 1] * skalaY) + 150;

            //podpis osi
            for (int i = 1; i < points.Length; i++)
            {
                g.DrawLine(new Pen(Color.Green), 365, points[i].Y, 375, points[i].Y);
                string s = tmp[2, (i-1) / 2].ToString();
                Font czcionka = new Font("ArialBold", 6);

                g.DrawString(s, czcionka, new SolidBrush(Color.Black), 380, points[i].Y - czcionka.Size / 2);

            }

            g.DrawLines(new Pen(Color.Red), points);
           

        }
        private void RysujWykresPionowe(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //rysunek sił przekrojowych
            double skalaY;
            double skalaX;

            try
            {
                skalaY = 90.0 / O.getMaxPrzekrojowy();
                skalaX = 340 / dlugosc;
            }
            catch (OverflowException)
            {
                skalaY = 0.0;
                skalaX = 0.0;
                label4.Text = " Błąd wprowadzania danych !!!";
                button3_Click(sender, e);
            }

            double[,] tmp = O.getSilyPrzekrojowe(true);

            PointF[] points = new PointF[(2 * tmp.Length / 3)];

            points[0].X = Convert.ToInt32(tmp[0, 0] * skalaX) + 30;
            points[0].Y = Convert.ToInt32(tmp[1, 0] * skalaY) + 150;


            for (int i = 1; i < points.Length - 1; i++)
            {
                points[i].X = Convert.ToInt32(tmp[0, i / 2] * skalaX) + 30;

                if (i % 2 == 0)
                    points[i].Y = Convert.ToInt32(tmp[1, i / 2] * skalaY) + 150;
                else
                    points[i].Y = Convert.ToInt32(tmp[2, i / 2] * skalaY) + 150;
            }

            points[points.Length - 1].X = Convert.ToInt32(tmp[0, tmp.Length / 3 - 1] * skalaX) + 30;
            points[points.Length - 1].Y = Convert.ToInt32(tmp[2, tmp.Length / 3 - 1] * skalaY) + 150;

            g.DrawLines(new Pen(Color.Red), points);
            for (int i = 1; i < points.Length; i++)
            {
                g.DrawLine(new Pen(Color.Green), 365, points[i].Y, 375, points[i].Y);
                string s = tmp[2, (i - 1) / 2].ToString();
                Font czcionka = new Font("ArialBold", 6);

                g.DrawString(s, czcionka, new SolidBrush(Color.Black), 380, points[i].Y - czcionka.Size / 2);

            }

            //rysunek momentów przekrojowych
            skalaY = 90.0 / O.getMaxPrzekrojowy(true, true);
            skalaX = 340 / dlugosc;

            tmp = O.getMomentyPrzekrojowe();

            points = new PointF[(2 * tmp.Length / 3) ];

            points[0].X = Convert.ToInt32(tmp[0, 0] * skalaX) + 30;
            points[0].Y = Convert.ToInt32(tmp[1, 0] * skalaY) + 150;


            for (int i = 1; i < points.Length; i++)
            {
                points[i].X = Convert.ToInt32(tmp[0, i / 2] * skalaX) + 30;

                if (i % 2 == 0)
                    points[i].Y = Convert.ToInt32(tmp[1, i / 2] * skalaY) + 150;
                else
                    points[i].Y = Convert.ToInt32(tmp[2, i / 2] * skalaY) + 150;
            }

            points[points.Length - 1].X = Convert.ToInt32(tmp[0, tmp.Length / 3 - 1] * skalaX) + 30;
            points[points.Length - 1].Y = Convert.ToInt32(tmp[2, tmp.Length / 3 - 1] * skalaY) + 150;
            //podpis osi
            for (int i = 1; i < points.Length; i++)
            {
                g.DrawLine(new Pen(Color.Green), 25, points[i].Y, 35, points[i].Y);
                Math.Round(tmp[2, i / 2], 1);
                string s = tmp[2,i/2].ToString();
                Font czcionka = new Font("ArialBold", 6);
                g.DrawString(s, czcionka, new SolidBrush(Color.Black), 5, points[i].Y-czcionka.Size/2);

            }
            g.DrawLines(new Pen(Color.Blue), points);
            

        }

        //form
        public Form1()
        {
            InitializeComponent();

            O.setDlugosc(dlugosc);

            podpar.Select();
            poziome.Select();

            WlaczRysowanie();
            RysujPusteWykresy();
        }
        //Nowe rzeczy, podpisy,linki,logo AGH
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.agh.edu.pl/");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://imir.agh.edu.pl/");
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
            label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
            label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BorderStyle= System.Windows.Forms.BorderStyle.FixedSingle;
        }
        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

    }
}
