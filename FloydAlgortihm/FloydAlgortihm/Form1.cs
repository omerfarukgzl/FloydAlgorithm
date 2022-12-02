using System;
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
            AutoScroll = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            button2.Enabled = false;
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
                groupBox1.Enabled = true;
                button2.Enabled = true;

                // girilen güğüm sayısna göre oluşturulan dynamic array 
                textBoxArray = new TextBox[number, number];
                labelLineArray = new Label[number];
                labelCoulmnArray = new Label[number];
                graph = new int[number, number];
                graphLine = new int[number, number];

                // girilen düğüm sayısına bağlı olarak düğüm verileri alınacak textbox ları ve info için label box ları dynamic olarak oluştur
                AddComponnet(number, textBoxArray, labelLineArray, labelCoulmnArray);
            }
            catch
            {
                MessageBox.Show("Lütfen Düğüm Sayısını Tam Sayı Değerinde Giriniz !", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

       
        // En kısa yol mesafe uzunluğu
        private void PrintNodebyNodeShortestLineNumber(int[,] graph, int[,] graphLine)
        {

            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                try
                {
                    label2.Text = "En Kısa Mesafe: ";
                    int firstNode = Convert.ToInt32(textBox1.Text);
                    int secondNode = Convert.ToInt32(textBox2.Text);

                    int i = firstNode - 1;
                    int j = secondNode - 1;

                    if (i != j)
                        Debug.WriteLine(i + 1 + "---->" + (j + 1) + ": " + graph[i, j]);
                    label2.Text = label2.Text + "  " + graph[i, j];

                    CalculateGraphLine(graphLine);
                }
                catch
                {
                    MessageBox.Show("Lütfen Düğüm Numaralarını Eksiksiz ve Doğru Giriniz !", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen Düğüm Numaralarını Eksiksiz ve Doğru Giriniz !", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
            //Print(distance, number);            
            PrintNodebyNodeShortestLineNumber(distance,graphLine);
           // PrintNodebyNodeShortestLine(graphLine, number);          
        }
        private void CalculateGraphLine(int[,]graphLine)
        {           
            if (textBox1.Text.Length > 0 && textBox2.Text.Length>0)
            {
                try
                {
                    nodeDistanceLabel.Text = "";
                    int firstNode = Convert.ToInt32(textBox1.Text);
                    int secondNode = Convert.ToInt32(textBox2.Text);

                    int i = firstNode-1;
                    int j = secondNode-1;

                    List<int> lineList = new List<int>();

                    Debug.Write((i + 1) + " ve " + (j + 1) + "arası en kısa mesafe :  ");                   
                    
                    //nodeDistanceLabel.Text = (i + 1) + " ve " + (j + 1) + "arası en kısa mesafe :  ";

                    while (j >= 0 && i != j) // ilgili iki düğüm satırının işlemleri
                    {
                        if (j == (graphLine[i, j] - 1) || j == 0)
                        {
                            lineList.Add((j + 1));
                            break;
                        }
                        lineList.Add((j + 1));
                        j = graphLine[i, j] - 1;
                    }
                    lineList.Add((i + 1));
                    lineList.Reverse();
                    foreach (int val in lineList)
                    {
                        if (nodeDistanceLabel.Text.Length > 40)
                            nodeDistanceLabel.Text = nodeDistanceLabel.Text + Environment.NewLine;
                        if (val == lineList.Last())
                        {
                            Debug.Write(val);
                            nodeDistanceLabel.Text = nodeDistanceLabel.Text + val ;
                        }                        
                        else
                        {
                            Debug.Write(val + " ===> ");
                            nodeDistanceLabel.Text = nodeDistanceLabel.Text + val + " ===> ";
                        }                            
                    }
                    lineList.Clear();
                    Debug.WriteLine("");
                }
                catch
                {
                    MessageBox.Show("Lütfen Düğüm Numaralarını Eksiksiz ve Doğru Giriniz !", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lütfen Düğüm Numaralarını Eksiksiz ve Doğru Giriniz !", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void GraphSoulution(TextBox [,]textBoxArray, int[,] graph, int number) // graph soulution 
        {
            // girilen matris bilgilerinin değerlerini çözümleyip verilerin atanmasının yapılması ( eksik bilgi  varsa uyarı da bulunn)
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
            //dynamic oluşturulacak componentlerin base location bilgileri
            int leftStart = 400;
            int lineDistance = 150;
            int topStart = 100;
            int topDistance = 60;

            try
            {
                // sonsuz ve sıfır yazılacak düğüm bilgileri info
                Label labelInf = new Label
                {
                    Location = new System.Drawing.Point(leftStart, topStart-50),
                    Name = "labelNodeInfo",
                    Text = "Lütfen komşu bağlantısı olmayan düğümler arasına 9999 ve eş düğümler arası mesafeye 0 yazınız(örn: 1 vs 1)"
                };
                labelInf.AutoSize = true;
                labelInf.BringToFront();
                this.Controls.Add(labelInf);

                // girilen düğüm sayısına göre oluşturulacak kare matris
                for (int i = 0; i < number; i++)
                {
                    for (int j = 0; j < number; j++)
                    {
                        if (i == 0)//index sayısı text label (satır)
                        {
                            // satırca yazılan index bilgileri içeren label lar
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
            groupBox1.Enabled = false;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e) // Calculate Button
        {
            string count = NodeNumber.Text.ToString();
            int number = Convert.ToInt32(count);

            GraphSoulution(textBoxArray,graph, number);
        }
    }
    /* private void Print(int[,] distance, int verticesCount)
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

    private void PrintNodebyNodeShortestLine(int [,] graph, int number)
    {
        // düğümler arası en kısa mesage bilgilerindeki node sırasını list tipinde saklayıp sırayla bastırmak
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
                        if (j == (graph[i, j] - 1) || j == 0) // iki düğüm arası dolaşımda son adım
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
    */












}
