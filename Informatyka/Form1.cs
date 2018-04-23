using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Informatyka
{
    public partial class Form1 : Form
    {
        private double length = 100;
        private XFM O = new XFM();
        private PictureBox pictureBox1 = new PictureBox();
        private PictureBox pictureBox2 = new PictureBox();
        private PictureBox plotBox = new PictureBox();

        //buttons
        private void button1_Click(object sender, EventArgs e)
        {
            //Values input
            valueInput();

            //Reactions output
            if (textBox1.Text.Length > 0 || textBox5.Text.Length > 0)
                showReaction(wsuw.Checked, vertical.Checked);

            //drawing forces on pictureBox
            turnPaintingOn();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox5.Clear();
            textBox4.Clear();

            O.clear();

            reactions_list.Text = "Reaction list:";

            turnPaintingOn();
            drawEmptyPlots();
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

            reactions_list.Text = "Reaction list";

            if (((textBox1.Text.Length > 0 && textBox2.Text.Length > 0) || (textBox4.Text.Length > 0 && textBox5.Text.Length > 0)) &&( O.getFX(0)!=-1.0|| O.getMX(0) != -1.0))
            {
                if (!vertical.Checked)
                {
                    /*
                    label4.Text += "Poredukowane\n";
                    for (int i = 0; i < O.reduce(O.getFX(), O.getF()).Length / 2; i++)
                        label4.Text += O.reduce(O.getFX(), O.getF())[0, i].ToString() + "|" + O.reduce(O.getFX(), O.getF())[1, i].ToString() + "\n";
                     */

                    reactions_list.Text += "\nPrzekrojowe siły poziome X|L|P\n";
                    for (int i = 0; i < O.getIntersectionForces().Length / 3; i++)
                        reactions_list.Text += "Distance:" + O.getIntersectionForces()[0, i].ToString() + " [m] |  " + O.getIntersectionForces()[1, i].ToString() + "[N] Left side|  " + O.getIntersectionForces()[2, i].ToString() + "[N] Right side\n";
                }
                else
                {
                    /*
                    label4.Text += "Poredukowane MOMENTY\n";
                    for (int i = 0; i < O.reduce(O.getMX(),O.getM()).Length / 2; i++)
                        label4.Text += O.reduce(O.getMX(), O.getM())[0, i].ToString() + "|" + O.reduce(O.getMX(), O.getM())[1, i].ToString() + "\n";

                    label4.Text += "Poszerzone\n";
                    for (int i = 0; i < O.poszerz().Length / 3; i++)
                        label4.Text += O.poszerz()[0, i].ToString() + "|" + O.poszerz()[1, i].ToString() + "|" + O.poszerz()[2, i].ToString() + "\n";
                    */

                    reactions_list.Text += "Maximun intersection force: " + O.getMaxIntersectionMoment(true, false) + "\n";
                    reactions_list.Text += "Maximum intersection moment: " + O.getMaxIntersectionMoment(true, true) + "\n";
                    reactions_list.Text += "Vertical intersection forces: X|L|P\n";

                    for (int i = 0; i < O.getIntersectionForces(true).Length / 3; i++)
                        reactions_list.Text += "Distance:" + O.getIntersectionForces()[0, i].ToString() + " [m] |  " + O.getIntersectionForces()[1, i].ToString() + "[N] Left side|  " + O.getIntersectionForces()[2, i].ToString() + "[N] Right side\n";

                    reactions_list.Text += "Vertical intersection moments: X|L|P\n";
                    for (int i = 0; i < O.getIntersectionMoments().Length / 3; i++)
                        reactions_list.Text += O.getIntersectionMoments()[0, i].ToString() + " [m]  |" + O.getIntersectionMoments()[1, i].ToString() + "[Nm] Left side|  " + O.getIntersectionMoments()[2, i].ToString() + "[Nm] Right side\n";
                }

                turnPlotsOn();
            }       
        }

        //subsidiary function
        private void showReaction(bool utwierdzona,bool vertical)
        {
        //reactions presentation
        if (!utwierdzona)//radioButton2ta
        {
                if (!vertical) //horizontal
                    reactions_list.Text = "Reaction list:\nHorizontal reaction on the left support: " + O.getF(0) + "[N]\nNo reaction on the right support";
            else //vertical
                reactions_list.Text = "Reaction list:\nVertical reaction on the left support: " + O.getF(0) + "[N]\nVertical reaction on the right support: " + O.getF(O.getF().Length - 1) + "[N]";
        }
        else //utwierdzona
        {
            if (!vertical) //horizontal
                reactions_list.Text = "Reaction list:\nHorizontal reaction on the left support: " + O.getF(0) + "[N]\nNo reaction on the right support";
            else //vertical
                reactions_list.Text = "Reaction list:\nVertical reaction on the left support: " + O.getF(0) + "[N]\nMoment umocnienia w lewej podporze ma wartość: " + O.getM(0) + "[Nm]";
        }
    }
        private void valueInput()
        {
            //clearing the 'old' values
            O.clear();

            //force input
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
                O.setF(textBox1.Text, textBox2.Text);
            else
                O.forceClear();

            //clearing textboxes so that we can fill them again with the right values
            textBox1.Clear();
            textBox2.Clear();

            //filling with the right values
            for (int i = 0; i < O.getFX().Count(); i++)
            {
                if(O.getF(i)!=0)
                {
                    textBox1.AppendText(O.getFX(i).ToString() + "\n");
                    textBox2.AppendText(O.getF(i).ToString() + "\n");
                }
            }

            //we put in moments (if they exist)
            if (vertical.Checked && textBox4.Text.Length > 0 && textBox5.Text.Length > 0)
                O.setM(textBox5.Text, textBox4.Text);
            else
                O.momentClear();


            //clearing textboxes so that we can fill them again with the right values
            if (vertical.Checked)
            {
                textBox5.Clear();
                textBox4.Clear();

                //filling with the right values
                for (int i = 0; i < O.getMX().Count(); i++)
                {
                    if(O.getM(i)!=0)
                    {
                        textBox5.AppendText(O.getMX(i).ToString() + "\n");
                        textBox4.AppendText(O.getM(i).ToString() + "\n");
                    }
                }
            }

            //adding reactions to value arrays
            if (textBox1.Text.Length > 0 || textBox5.Text.Length > 0)
                O.reactionAdd(wsuw.Checked, vertical.Checked);
        }

        //drawing
        private void drawEmptyPlots()
        {
            plotBox.Location = new Point(520, 260);
            plotBox.Size = new Size(500, 300);
            plotBox.BackColor = Color.LightBlue;
            plotBox.Visible = true;
            Controls.Add(plotBox);

            plotBox.Paint += new System.Windows.Forms.PaintEventHandler(drawAxes);
        }
        private void turnPaintingOn()
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
                pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(drawForces);
                if (vertical.Checked)
                    pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(drawMoments);
            }
            if (wsuw.Checked)
            {
                pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(RysujBelkeUtwierdzona);
                pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(drawForces);
                if (vertical.Checked)
                    pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(drawMoments);
            }
        }
        private void drawForces(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //rysowanie sily w punkcie o odleglej od poczatku belki od fx
            //drawing a force in a point

            if (textBox1.Text.Length > 0 && O.getF().Length == O.getFX().Length)
            {
                Color kolor = new Color();
                string znak;

                for (int a = 0; a < O.getFX().Length; a++)
                {
                    int fx = Convert.ToInt32(O.getFX(a) * 340.0 / length) + 30;

                    if ((horizontal.Checked && a == 0) || (vertical.Checked && (a == 0 || (a == O.getFX().Length - 1 && !wsuw.Checked))))//Wyróżnianie poziomych reakcji  &&  wyróżnienie pionowych reakcji
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
                        if (O.getF(a) > 0)//positive Forces
                        {
                            if (vertical.Checked)//vertical positive force
                            {
                                g.FillRectangle(new SolidBrush(kolor), fx - 4, 115, 8, 60);

                                PointF[] points = new PointF[] { new PointF { X = fx + 10, Y = 115 }, new PointF { X = fx - 10, Y = 115 }, new PointF { X = fx, Y = 100 } };
                                g.FillPolygon(new SolidBrush(kolor), points);

                                g.DrawString(znak + a, new Font("ArialBold", 8), new SolidBrush(Color.Black), fx - 5, 175 + 10 * (a % 5));
                            }

                            if (horizontal.Checked)//horizontal positive force
                            {
                                g.FillRectangle(new SolidBrush(kolor), fx - 65, 96, 60, 8);

                                PointF[] points = new PointF[] { new PointF { X = fx - 15, Y = 110 }, new PointF { X = fx - 15, Y = 90 }, new PointF { X = fx, Y = 100 } };
                                g.FillPolygon(new SolidBrush(kolor), points);

                                g.DrawString(znak + a, new Font("ArialBold", 8), new SolidBrush(Color.Black), fx - 5, 125 + 10 * (a % 5));
                            }
                        }

                        if (O.getF(a) < 0)
                        {
                            if (vertical.Checked)//vertical negative force
                            {
                                g.FillRectangle(new SolidBrush(kolor), fx - 4, 30, 8, 60);

                                PointF[] points = new PointF[] { new PointF { X = fx + 10, Y = 85 }, new PointF { X = fx - 10, Y = 85 }, new PointF { X = fx, Y = 100 } };
                                g.FillPolygon(new SolidBrush(kolor), points);

                                g.DrawString(znak + (a), new Font("ArialBold", 8), new SolidBrush(Color.Black), fx - 5, 15 - 10 * (a % 5));
                            }

                            if (horizontal.Checked)//horizontal negative force
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
        private void drawMoments(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //rysowanie sily w punkcie o odleglej od poczatku belki od fx
            //drawing force at point
            if (textBox5.Text.Length > 0 && O.getM().Length == O.getMX().Length)
            {
                Color color = new Color();
                string znak;

                for (int a = 0; a < O.getMX().Length; a++)
                {
                    int fx = Convert.ToInt32(O.getMX(a) * 340.0 / length) + 30;

                    if (a == 0 && wsuw.Checked)//Wyróżnianie momentu utwardzenia 
                    {
                        color = Color.DarkGray;
                        znak = "U";
                    }
                    else
                    {
                        color = Color.Cyan;
                        znak = "M";
                    }


                    if (fx >= 30)
                    {
                        if (O.getM(a) > 0)//positive moment
                        {
                            g.DrawLine(new Pen(color, 1), fx, 95, fx, 105);
                            g.DrawArc(new Pen(color, 2), fx - 15, 100 - 15, 30, 30, 180, 220);

                            PointF[] points = new PointF[] { new PointF { X = fx - 22, Y = 100 }, new PointF { X = fx - 8, Y = 100 }, new PointF { X = fx - 15, Y = 110 } };
                            g.FillPolygon(new SolidBrush(color), points);

                            g.DrawString(znak + (a), new Font("ArialBold", 8), new SolidBrush(Color.Black), fx - 5, 130 + 10 * (a % 5));

                        }

                        if (O.getM(a) < 0)//negative moment
                        {
                            g.DrawLine(new Pen(color, 1), fx, 95, fx, 105);
                            g.DrawArc(new Pen(color, 2), fx - 15, 100 - 15, 30, 30, 0, 120);

                            PointF[] points = new PointF[] { new PointF { X = fx + 22, Y = 100 }, new PointF { X = fx + 8, Y = 100 }, new PointF { X = fx + 15, Y = 90 } };
                            g.FillPolygon(new SolidBrush(color), points);

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

            //clear drawing area
            g.Clear(Color.SeaShell);

            //left support
            g.DrawEllipse(System.Drawing.Pens.Black, 30 - 4, 100 - 4, 8, 8);
            g.DrawLine(System.Drawing.Pens.Black, 30, 100, 30 + i, 100 + i);
            g.DrawLine(System.Drawing.Pens.Black, 30 - i, 100 + i, 30, 100);
            g.DrawLine(System.Drawing.Pens.Black, 30 - i, 100 + i, 30 + i, 100 + i);

            //right support
            g.DrawEllipse(System.Drawing.Pens.Black, 370 - 4, 100 - 4, 8, 8);
            g.DrawLine(System.Drawing.Pens.Black, 370, 100, 370 + i, 100 + i);
            g.DrawLine(System.Drawing.Pens.Black, 370 - i, 100 + i, 370, 100);
            g.DrawLine(System.Drawing.Pens.Black, 370 - i, 100 + i, 370 + i, 100 + i);
            g.DrawLine(System.Drawing.Pens.Black, 370 - i, 100 + i + 5, 370 + i, 100 + i + 5);

            //drawing the beam
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
            //clear drawing area
            g.Clear(Color.SeaShell);

            //drawing the beam
            Pen p = new Pen(Color.Black, 3);
            g.DrawLine(p, 30, 100, 370, 100);

            //umocnienie
            g.DrawLine(System.Drawing.Pens.Black, 30, 60, 30, 140);
            //kreskowanie
            for (int j = 0; j < 7; j++)
                g.DrawLine(System.Drawing.Pens.Black, 30, 135 - j * 10, 10, 115 - j * 10);

        }

        //radiobuttons
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
            if (vertical.Checked)
            {
                groupBox3.Visible = true;
                addForces.Text = "Add Forces\n and Moments";
            }

            else
            {
                groupBox3.Visible = false;
                addForces.Text = "Add Forces";
            }

            button3_Click(sender, e);
        }

        //textboxes
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            var currentCulture = System.Globalization.CultureInfo.InstalledUICulture;
            var numberFormat = (System.Globalization.NumberFormatInfo)currentCulture.NumberFormat.Clone();
            numberFormat.NumberDecimalSeparator = ".";

            double w = 0;

            if (double.TryParse(textBox3.Text, NumberStyles.Any, numberFormat, out w) && w > 0)
            {
                length = w;
                O.setLength(length);
            }
            else
                textBox3.Text = length.ToString();
        }

        //labels
        private void label4_MouseEnter(object sender, EventArgs e)
        {
            reactions_list.ForeColor = Color.Red;
            reactions_list.BackColor = Color.LightGray;
        }
        private void label4_MouseHover(object sender, EventArgs e)
        {
            reactions_list.ForeColor = Color.Red;
            reactions_list.BackColor = Color.LightGray;
        }
        private void label4_MouseLeave(object sender, EventArgs e)
        {
            reactions_list.ForeColor = Color.Black;
            reactions_list.BackColor = Color.SeaShell;
        }

        //drawing plots
        private void turnPlotsOn()
        {
            plotBox.Location = new Point(520, 260);
            plotBox.Size = new Size(500, 300);
            plotBox.BackColor = Color.LightBlue;
            plotBox.Visible = true;
            plotBox.Parent = pictureBox2;
            Controls.Add(plotBox);

            plotBox.Paint += new System.Windows.Forms.PaintEventHandler(drawAxes);


                if ((textBox1.Text.Length > 0 && textBox2.Text.Length > 0) || (textBox4.Text.Length > 0 && textBox5.Text.Length > 0))
                {
                    if (!vertical.Checked)
                        plotBox.Paint += new System.Windows.Forms.PaintEventHandler(plotHorizontal);
                    else
                        plotBox.Paint += new System.Windows.Forms.PaintEventHandler(plotVertical);
                }
        }
        private void drawAxes(object sender, PaintEventArgs e)
        {
            //function run after clicking the "calculate" button
            Graphics g = e.Graphics;
            //clear drawing area
            g.Clear(Color.LightGray);

            //drawing arrays
            PointF[] arrayHorizontal = { new PointF(375, 145), new PointF(375, 155), new PointF(390, 150) };
            PointF[] arrayVerticalLeft = { new PointF(25,270 ), new PointF(35, 270), new PointF(30, 285) };
            PointF[] arrayVerticalRight = { new PointF(365, 270), new PointF(375, 270), new PointF(370, 285) };


            g.FillPolygon(new SolidBrush(Color.Black), arrayHorizontal);
            g.FillPolygon(new SolidBrush(Color.Black), arrayVerticalLeft);
            g.FillPolygon(new SolidBrush(Color.Black), arrayVerticalRight);


            //drawing plot axes                                  
            Pen p = new Pen(Color.Black, 2);
            g.DrawLine(p, 30, 30, 30, 270);
            g.DrawLine(p, 30, 150, 375, 150);
            g.DrawLine(p, 370, 30, 370, 270);
        }
        private void plotHorizontal(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            double scaleY;
            double scaleX;

            try
            {
                scaleY = 90.0 / O.getMaxIntersectionMoment();
                scaleX = 340 / length;
            }
            catch(OverflowException)
            {
                scaleY = 0.0;
                scaleX = 0.0;
                reactions_list.Text = " Value input error!";
                button3_Click(sender,e);
            }

            

            double[,] tmp = O.getIntersectionForces();

            PointF[] points = new PointF[(2 * tmp.Length / 3)];

            points[0].X = Convert.ToInt32(tmp[0, 0] * scaleX) + 30;
            points[0].Y = Convert.ToInt32(tmp[1, 0] * scaleY) + 150;
           

            for (int i = 1; i < points.Length; i++)
            {
                points[i].X = Convert.ToInt32(tmp[0, i /2] * scaleX) + 30;

                if (i % 2 == 0)
                    points[i].Y = Convert.ToInt32(tmp[1, i /2] * scaleY) + 150;
                else
                    points[i].Y = Convert.ToInt32(tmp[2, i /2] * scaleY) + 150;
            }

            points[points.Length-1].X = Convert.ToInt32(tmp[0, tmp.Length / 3 - 1] * scaleX) + 30;
            points[points.Length-1].Y = Convert.ToInt32(tmp[2, tmp.Length / 3 - 1] * scaleY) + 150;

            //axes titles
            for (int i = 1; i < points.Length; i++)
            {
                g.DrawLine(new Pen(Color.Green), 365, points[i].Y, 375, points[i].Y);
                string s = tmp[2, (i-1) / 2].ToString();
                Font myfont = new Font("ArialBold", 6);

                g.DrawString(s, myfont, new SolidBrush(Color.Black), 380, points[i].Y - myfont.Size / 2);

            }

            g.DrawLines(new Pen(Color.Red), points);
           

        }
        private void plotVertical(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //intersection forces drawing 
            double scaleY;
            double scaleX;

            try
            {
                scaleY = 90.0 / O.getMaxIntersectionMoment();
                scaleX = 340 / length;
            }
            catch (OverflowException)
            {
                scaleY = 0.0;
                scaleX = 0.0;
                reactions_list.Text = " Data input error!";
                button3_Click(sender, e);
            }

            double[,] tmp = O.getIntersectionForces(true);

            PointF[] points = new PointF[(2 * tmp.Length / 3)];

            points[0].X = Convert.ToInt32(tmp[0, 0] * scaleX) + 30;
            points[0].Y = Convert.ToInt32(tmp[1, 0] * scaleY) + 150;


            for (int i = 1; i < points.Length - 1; i++)
            {
                points[i].X = Convert.ToInt32(tmp[0, i / 2] * scaleX) + 30;

                if (i % 2 == 0)
                    points[i].Y = Convert.ToInt32(tmp[1, i / 2] * scaleY) + 150;
                else
                    points[i].Y = Convert.ToInt32(tmp[2, i / 2] * scaleY) + 150;
            }

            points[points.Length - 1].X = Convert.ToInt32(tmp[0, tmp.Length / 3 - 1] * scaleX) + 30;
            points[points.Length - 1].Y = Convert.ToInt32(tmp[2, tmp.Length / 3 - 1] * scaleY) + 150;

            g.DrawLines(new Pen(Color.Red), points);
            for (int i = 1; i < points.Length; i++)
            {
                g.DrawLine(new Pen(Color.Green), 365, points[i].Y, 375, points[i].Y);
                string s = tmp[2, (i - 1) / 2].ToString();
                Font myfont = new Font("ArialBold", 6);

                g.DrawString(s, myfont, new SolidBrush(Color.Black), 380, points[i].Y - myfont.Size / 2);

            }

            // intersection moments drawing
            scaleY = 90.0 / O.getMaxIntersectionMoment(true, true);
            scaleX = 340 / length;

            tmp = O.getIntersectionMoments();

            points = new PointF[(2 * tmp.Length / 3) ];

            points[0].X = Convert.ToInt32(tmp[0, 0] * scaleX) + 30;
            points[0].Y = Convert.ToInt32(tmp[1, 0] * scaleY) + 150;


            for (int i = 1; i < points.Length; i++)
            {
                points[i].X = Convert.ToInt32(tmp[0, i / 2] * scaleX) + 30;

                if (i % 2 == 0)
                    points[i].Y = Convert.ToInt32(tmp[1, i / 2] * scaleY) + 150;
                else
                    points[i].Y = Convert.ToInt32(tmp[2, i / 2] * scaleY) + 150;
            }

            points[points.Length - 1].X = Convert.ToInt32(tmp[0, tmp.Length / 3 - 1] * scaleX) + 30;
            points[points.Length - 1].Y = Convert.ToInt32(tmp[2, tmp.Length / 3 - 1] * scaleY) + 150;
            //axes title
            for (int i = 1; i < points.Length; i++)
            {
                g.DrawLine(new Pen(Color.Green), 25, points[i].Y, 35, points[i].Y);
                Math.Round(tmp[2, i / 2], 1);
                string s = tmp[2,i/2].ToString();
                Font myfont = new Font("ArialBold", 6);
                g.DrawString(s, myfont, new SolidBrush(Color.Black), 5, points[i].Y-myfont.Size/2);

            }
            g.DrawLines(new Pen(Color.Blue), points);
            

        }

        //form
        public Form1()
        {
            InitializeComponent();

            O.setLength(length);

            podpar.Select();
            horizontal.Select();

            turnPaintingOn();
            drawEmptyPlots();
        }
        //links, AGH logo, finishing touches before project presentation
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

        private void poziome_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
