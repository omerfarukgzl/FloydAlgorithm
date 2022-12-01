﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloydAlgortihm
{
    public partial class Form1 : Form
    {
        private TextBox[,] textBoxArray;
        private int[,] graph;
        private int[,] graphLine;

        private Label[] labelLineArray;  
        private Label[] labelCoulmnArray;
      

        private const int ENDLESS = 9999;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e) // get node number
        {

            try
            {
                string count = NodeNumber.Text.ToString();
                int number = Convert.ToInt32(count);

                NodeNumber.Enabled = false;
                button1.Enabled = false;
                label1.Enabled = false;
                button3.Enabled = true;


                textBoxArray = new TextBox[number, number];
                labelLineArray = new Label[number];
                labelCoulmnArray = new Label[number];
                graph = new int[number, number];
                graphLine = new int[number, number];

                AddComponnet(number, textBoxArray, labelLineArray, labelCoulmnArray);

            }
            catch
            {
                MessageBox.Show("Lütfen Düğüm Sayısını Tam Sayı Değerinde Giriniz !", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




             

        }
        //static void FloydWarshall(int[,] weights, int number)
        //{
        //    double[,] dist = new double[number, number];
        //    for (int i = 0; i < number; i++)
        //    {
        //        for (int j = 0; j < number; j++)
        //        {
        //            dist[i, j] = double.PositiveInfinity;
        //        }
        //    }

        //    for (int i = 0; i < weights.GetLength(0); i++)
        //    {
        //        dist[weights[i, 0] - 1, weights[i, 1] - 1] = weights[i, 2];
        //    }

        //    int[,] next = new int[number, number];
        //    for (int i = 0; i < number; i++)
        //    {
        //        for (int j = 0; j < number; j++)
        //        {
        //            if (i != j)
        //            {
        //                next[i, j] = j + 1;
        //            }
        //        }
        //    }

        //    for (int k = 0; k < number; k++)
        //    {
        //        for (int i = 0; i < number; i++)
        //        {
        //            for (int j = 0; j < number; j++)
        //            {
        //                if (dist[i, k] + dist[k, j] < dist[i, j])
        //                {
        //                    dist[i, j] = dist[i, k] + dist[k, j];
        //                    next[i, j] = next[i, k];
        //                }
        //            }
        //        }
        //    }

        //    PrintResult(dist, next);
        //}

        //static void PrintResult(double[,] dist, int[,] next)
        //{
        //    Console.WriteLine("pair     dist    path");
        //    for (int i = 0; i < next.GetLength(0); i++)
        //    {
        //        for (int j = 0; j < next.GetLength(1); j++)
        //        {
        //            if (i != j)
        //            {
        //                int u = i + 1;
        //                int v = j + 1;
        //                string path = string.Format("{0} -> {1}    {2,2:G}     {3}", u, v, dist[i, j], u);
        //                do
        //                {
        //                    u = next[u - 1, v - 1];
        //                    path += " -> " + u;
        //                } while (u != v);
        //                Console.WriteLine(path);
        //            }
        //        }
        //    }
        //}

        private void Print(int[,] distance, int verticesCount)
        {
            Console.WriteLine("Shortest distances between every pair of vertices:");

            for (int i = 0; i < verticesCount; ++i)
            {
                for (int j = 0; j < verticesCount; ++j)
                {
                    if (distance[i, j] == ENDLESS)
                    {
                        Debug.Write("INF".PadLeft(7));
                    }

                    else
                    {
                      
                        Debug.Write(distance[i, j].ToString().PadLeft(7));
                    }

                }

                Debug.WriteLine("");
            }
        }
        private void PrintNodebyNodeShortestLineNumber(int[,] graph, int number)
        {
            for (int i = 0; i < number; i++)
            {
                for (int j = number-1; j >= 0; j--)
                {
                    if(i!=j)
                    Debug.WriteLine(i+1+"---->" + (j+1)+ ": " + graph[i, j]);
                }

            }
                

        }
        private void PrintNodebyNodeShortestLine(int [,] graph, int number)
        {
            try
            {
                int j = number - 1;
                List<int> lineList = new List<int>();

                for (int i = 0; i < number; i++)
                {                  
                    for (int k=number-1;k>=0;k--)
                    {                        
                        j = k;
                        Debug.Write((i +1) + " ve " + (j+1)+ "arası en kısa mesafe :  ");
                        while (j >= 0 && i!=j) // foru değiştirdim
                        {                                                    
                            if (j == (graph[i, j] - 1) || j == 0)
                            {
                               lineList.Add((j+1));
                               break;
                            }
                            lineList.Add((j+1));
                            j = graph[i, j] - 1;
                         
                        }
                        lineList.Add((i + 1));
                        lineList.Reverse();
                        foreach (int val in lineList)
                        {
                            if(val==lineList.Last())
                                Debug.Write(val);
                            else
                                Debug.Write(val + " ===> ");

                        }
                          

                        lineList.Clear();
                        Debug.WriteLine("");
                    }                  
                }
            } 
            catch
            {
                Debug.WriteLine("error"); 
            }
           


        }
        private void FloydWarshall(int[,] graph, int[,] graphLine, int number)
        {
            int[,] distance = new int[number, number];

            for (int i = 0; i < number; ++i)
                for (int j = 0; j < number; ++j)
                    distance[i, j] = graph[i, j];

            for (int k = 0; k < number; ++k)
            {
                for (int i = 0; i < number; ++i)
                {
                    for (int j = 0; j < number; ++j)
                    {
                        if (distance[i, k] + distance[k, j] < distance[i, j] && i!=j)
                        {
                            distance[i, j] = distance[i, k] + distance[k, j];
                            graphLine[i, j] = k+1;
                        }
                           
                    }
                }
            }

            Print(distance, number);
            PrintNodebyNodeShortestLineNumber(distance, number);
            PrintNodebyNodeShortestLine(graphLine, number);
        }
        private void CalculateGraphLine(int[,]graphLine,int number)
        {


        }


        private void GraphSoulution(TextBox [,]textBoxArray, int[,] graph, int number) // graph soulution 
        {

            for (int i = 0; i < number; i++)
            {
                for (int j = 0; j < number; j++)
                {
                    if (textBoxArray[i,j].Text.Length>0)
                    {
                        try
                        {
                            int value = Convert.ToInt32(textBoxArray[i, j].Text);
                            graph[i, j] = value;
                            if (i != j)
                                graphLine[j, i] = i+1;
                            else
                                graphLine[i, j] = 0;
                        }
                        catch 
                        {
                            MessageBox.Show("Lütfen Düğümler Arası Bağlantı Bilgilerini eksiksiz ve tam sayı değerleri giriniz", "Eksik Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Lütfen Düğümler Arası Bağlantı Bilgilerini eksiksiz ve tam sayı değerleri giriniz", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                }
            }
            FloydWarshall(graph, graphLine, number);

        }


        private void AddComponnet(int number,TextBox[,] textBoxArray,Label[] labelLineArray, Label[] labelCoulmnArray) //  form screen add dynamicly component 
        {
            int leftStart = 350;
            int lineDistance = 150;
            int topStart = 100;
            int topDistance = 60;






            try
            {

                Label labelInf = new Label
                {
                    Location = new System.Drawing.Point(leftStart, topStart-50),
                    Name = "labelNodeInfo",
                    //Size = new Size(600, 20),
                    Text = "Lütfen komşu bağlantısı olmayan düğümler arasına 10000 ve eş düğümler arası mesafeye 0 yazınız(örn: 1 vs 1)"
                };
                labelInf.AutoSize = true;
                labelInf.BringToFront();
                this.Controls.Add(labelInf);

                for (int i = 0; i < number; i++)
                {
                    for (int j = 0; j < number; j++)
                    {
                        if (i == 0)//index sayısı text label (satır)
                        {
                            Label label = new Label
                            {
                                Location = new System.Drawing.Point( leftStart + 40 + (lineDistance * j), topStart + (topDistance * i)),
                                Name = "labelNodeCoulmn" + j + 1,
                                Size = new Size(100, 20),
                                Text = "" + (j + 1)
                            };
                            label.BringToFront();

                            labelLineArray[j] = label;
                            this.Controls.Add(labelLineArray[j]);
                        }
                        // veri girişi text box
                        TextBox textadd = new TextBox
                        {
                            Location = new System.Drawing.Point(leftStart + (lineDistance * j), topStart + 20 + (topDistance * i)),
                            Name = "btnNode" + j,
                            Size = new Size(100, 20)
                        };
                        textadd.BringToFront();

                        textBoxArray[i, j] = textadd;
                        this.Controls.Add(textBoxArray[i, j]);

                    }
                    // index sayısı text label (sutun)
                    Label label2 = new Label
                    {
                        Location = new System.Drawing.Point(leftStart-40, topStart + 20 + (topDistance * i)),
                        Name = "labelNodeLine" + i + 1,
                        Size = new Size(50, 50),
                        Text = "" + (i + 1)
                    };
                    label2.BringToFront();

                    labelCoulmnArray[i] = label2;
                    this.Controls.Add(labelCoulmnArray[i]);
                }

                Label labelCalculateInf = new Label
                {
                    Location = new System.Drawing.Point(leftStart, topStart*number),
                    Name = "labelCalculateNodeInf",
                    //Size = new Size(600, 20),
                    Text = "Lütfen arasındaki mesafeyi hesaplamak istediğiniz iki düğümü giriniz !"
                };
                labelCalculateInf.AutoSize = true;
                labelCalculateInf.BringToFront();
                this.Controls.Add(labelCalculateInf);

                Button calculateButton = new Button
                {
                    Location = new System.Drawing.Point(leftStart, topStart * number+40),
                    Name = "buttonCalculate",
                    //Size = new Size(600, 20),
                    Text = "Hesapla"
                };
                calculateButton.Click += (s, e) => {
                    string count = NodeNumber.Text.ToString();
                    int number = Convert.ToInt32(count);

                    GraphSoulution(textBoxArray, graph, number);
                };
                calculateButton.AutoSize = true;
                calculateButton.BringToFront();
                this.Controls.Add(calculateButton);

            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

        }

        private void button2_Click(object sender, EventArgs e) // Reset Floyd Graph
        {
            string count = NodeNumber.Text.ToString();
            int number = Convert.ToInt32(count);
            for (int i = 0; i < number; i++)
            {
                for (int j = 0; j < number; j++)
                {
                    this.Controls.Remove(textBoxArray[i, j]);
                    textBoxArray[i, j] = null;                   
                }
                this.Controls.Remove(labelCoulmnArray[i]);
                this.Controls.Remove(labelLineArray[i]);
                labelCoulmnArray[i] = null;
                labelLineArray[i] = null;
            }

            button1.Enabled = true;
            label1.Enabled = true;
            NodeNumber.Enabled = true;
            button3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e) // Floyd Calculate
        {
            string count = NodeNumber.Text.ToString();
            int number = Convert.ToInt32(count);

            GraphSoulution(textBoxArray,graph, number);

        }
    }
















}
