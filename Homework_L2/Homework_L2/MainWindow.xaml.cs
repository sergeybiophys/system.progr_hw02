using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Homework_L2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        Thread t1;
        Thread fuel1;
        TranslateTransform transform1 = new TranslateTransform();
        RotateTransform rotateTransform1 = new RotateTransform(90);
        RotateTransform rotateTransform2 = new RotateTransform(180);
        RotateTransform rotateTransform3 = new RotateTransform(270);
        RotateTransform rotateTransform4 = new RotateTransform(0);
        Random rnd = new Random();

        static bool StopTheCar = false;

        static bool PitStop = false;

        static AutoResetEvent waitHandler = new AutoResetEvent(true);
        //static AutoResetEvent waitHandler2 = new AutoResetEvent(true);
        static int x = 0;

        static int Place = 1;
        static int Lap = 0;

        const int LIMIT_X_ONE = 740;
        const int LIMIT_Y_ONE = 380;

        const int LIMIT_X_TWO = 780;
        const int LIMIT_Y_TWO = 380;

        const int LIMIT_X_THREE = 820;
        const int LIMIT_Y_THREE = 380;


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            //pbCar1.Value = 100;
        }

        private void Move(Button btn, Image img,  TranslateTransform transl, RotateTransform rotateTransform1 )
        {
           

            while (true)
            {
                if(StopTheCar||PitStop)
                {
                    waitHandler.WaitOne();
                }
                else
                {
                    //waitHandler.Set();
                    MoveByXtoRight(btn, transl, rotateTransform1);
                    //Dispatcher.Invoke(() =>
                    //{
                    //    imgCar1.RenderTransform = rotateTransform1;

                    //});
                    RotateImage(img, rotateTransform1);
                    MoveByYtoDown(btn, transl, rotateTransform1);
                    RotateImage(img, rotateTransform2);
                    MoveByXtoLeft(btn, transl, rotateTransform1);
                    RotateImage(img, rotateTransform3);
                    MoveByYtoUp(btn, transl, rotateTransform1);
                    RotateImage(img, rotateTransform4);
                    Lap++;
                    if (Lap == 1)
                    {
                       

                        StopTheCar = true;
                        //this.Title = "WINNER!";
                        waitHandler.WaitOne();
                        UpdateTextBox(tbPlace3);
                        Place++;
                    }
                }              
            }
          
     
            //RotateButton(btn, transl, rotateTransform1);
        }


        private void UpdateTextBox(TextBlock tb)
        {

            Action action = () => { UpdateText(tb); };
            tb.Dispatcher.BeginInvoke(action);
           
        }

        private void UpdateText(TextBlock tb)
        {
            tb.Text = $"{Place}+ Place";
        }

        private void MoveByXtoRight(Button btn, TranslateTransform transl, RotateTransform rotateTransform1)
        {
            int distance = 0;
     
            while (distance <= LIMIT_X_ONE)
            {
                if (StopTheCar)
                {
                    waitHandler.WaitOne();
                }
                else
                {
                    //distance += rnd.Next(10, 30);
                    distance += 10;
                    Thread.Sleep(100);
                    UpdatePositionButton(btn, transl, distance);
                }

            }
            
            //if(distance==640)
            //{
            //RotateButton(btn, transl, rotateTransform1);
            //}

        }
        private void MoveByXtoLeft(Button btn, TranslateTransform transl, RotateTransform rotateTransform1)
        {
            int distance = LIMIT_X_ONE;

            while (distance >=10)
            {
                if (StopTheCar)
                {
                    waitHandler.WaitOne();
                }
                else 
                {
                    //distance += rnd.Next(10, 30);
                    distance -= 10;
                    Thread.Sleep(100);
                    UpdatePositionButton(btn, transl, distance);
                }

            }
            //if(distance==640)
            //{
            //RotateButton(btn, transl, rotateTransform1);
            //}

        }
        private void UpdatePositionButton(Button btn,  TranslateTransform transl, int distance)
        {
            Action action = () => { SetDistance(btn,  transl, distance); };
            btn.Dispatcher.BeginInvoke(action);
        }
        private void SetDistance(Button btn,  TranslateTransform transl, int dist)
        {
            //transform1.X += rnd.Next(5, 10);

            transl.X = dist;
            btn.RenderTransform = transl;
            //if (dist == 600)
            //{
             
            //    //tb.Text = Places.ToString();
            //    //Places++;


            //}
        }

        private void MoveByYtoDown(Button btn, TranslateTransform transl, RotateTransform rotateTransform1)
        {
 

            int distance = 0;

            while (distance <= LIMIT_Y_ONE)
            {
                if (StopTheCar)
                {
                    waitHandler.WaitOne();
                }
                else
                {
                    //distance += rnd.Next(10, 30);
                    distance += 10;
                    Thread.Sleep(100);
                    UpdatePositionButtonY(btn, transl, distance);
                }
 
            }

        }

        private void MoveByYtoUp(Button btn, TranslateTransform transl, RotateTransform rotateTransform1)
        {


            int distance = LIMIT_Y_ONE;

            while (distance >= 10)
            {
                if (StopTheCar)
                {
                    waitHandler.WaitOne();
                }
                else
                {
                    //distance += rnd.Next(10, 30);
                    distance -= 10;
                    Thread.Sleep(100);
                    UpdatePositionButtonY(btn, transl, distance);
                }

            }

        }

        private void UpdatePositionButtonY(Button btn, TranslateTransform transl, int distance)
        {
            Action action = () => { SetDistanceY(btn, transl, distance); };
            btn.Dispatcher.BeginInvoke(action);
        }
        private void SetDistanceY(Button btn, TranslateTransform transl, int dist)
        {
            //transform1.X += rnd.Next(5, 10);

            transl.Y = dist;
            btn.RenderTransform = transl;
            //if (dist == 600)
            //{

            //    //tb.Text = Places.ToString();
            //    //Places++;


            //}
        }

        private void RotateImage(Image img, RotateTransform transf)
        {
            UpdateImgAngle(img, transf);
        }

        private void UpdateImgAngle(Image img,  RotateTransform transf)
        {

            Action action = () => { RotateCW(img, transf); };
            img.Dispatcher.BeginInvoke(action);

        }
        private void RotateCW(Image img, RotateTransform transf)
        {
            //transl.X = 650;
            //var buttonLocation = btn.PointToScreen().
            //transf.Angle = 90;
            //transf.CenterX = transl.Y;
            //transf.CenterY = transl.X;
            img.RenderTransform = transf;
            //btn.RenderTransform = transl;
          

        }

        private void FuelConsumption()
        {
            //for(int i = 0; i<=100; i++)
            //{
            //    Thread.Sleep(200);
            //    UpdateProgressBaar(i);
            //}
            int count = 100;

            while (count >= 0)
            {
               
                count -= rnd.Next(1, 5);
                Thread.Sleep(1000);
                UpdateProgressBaar(count);
                if (count <= 20)
                {
                    waitHandler.WaitOne();
                    
                    PitStop = true;

                    

                    int key = rnd.Next(0, 1);
                    switch (key)
                    {
                        case 0:
                            count = 100;
                            Thread.Sleep(3000);
                            waitHandler.Set();
                            PitStop = false;
                            break;
                        case 1:
                            waitHandler.Set();
                            PitStop = false;
                            break;
                    }
                }
                else
                {
                    waitHandler.Set();
                    PitStop = false;
                }
                if (count<=0)
                {
                    waitHandler.WaitOne();
                    StopTheCar = true;


                    //TODO
                    //btnRacer1.Opacity = 0.5;

                }

            }
            
        }
        private void UpdateProgressBaar(int i)
        {
            Action action = () => { SetProgress(i); };
            pbCar1.Dispatcher.BeginInvoke(action);
        }
        private void SetProgress(int i)
        {
            pbCar1.Value = i;
        }


        void StartClick(object sender, RoutedEventArgs e)
        {

            t1 = new Thread(() => Move(btnRacer1, imgCar1 ,transform1, rotateTransform1));
            fuel1 = new Thread(FuelConsumption);
            //t2.IsBackground = true;

            t1.Start();
            fuel1.Start();
   
        }
    }
}
