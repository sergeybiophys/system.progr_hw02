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
        Thread t2;
        Thread fuel2;
        Thread t3;
        Thread fuel3;


        TranslateTransform transform1 = new TranslateTransform();
        RotateTransform rotateTransform1 = new RotateTransform(90);
        RotateTransform rotateTransform2 = new RotateTransform(180);
        RotateTransform rotateTransform3 = new RotateTransform(270);
        RotateTransform rotateTransform4 = new RotateTransform(0);

        TranslateTransform transform2 = new TranslateTransform();
        RotateTransform rotateTransform21 = new RotateTransform(90);
        RotateTransform rotateTransform22 = new RotateTransform(180);
        RotateTransform rotateTransform23 = new RotateTransform(270);
        RotateTransform rotateTransform24 = new RotateTransform(0);

        TranslateTransform transform3 = new TranslateTransform();
        RotateTransform rotateTransform31 = new RotateTransform(90);
        RotateTransform rotateTransform32 = new RotateTransform(180);
        RotateTransform rotateTransform33 = new RotateTransform(270);
        RotateTransform rotateTransform34 = new RotateTransform(0);

        Random rnd = new Random();

        static bool StopTheCar1 = false;

        static bool PitStop1 = false;

        static bool StopTheCar2 = false;

        static bool PitStop2 = false;

        static bool StopTheCar3 = false;

        static bool PitStop3 = false;

        static AutoResetEvent waitHandler1 = new AutoResetEvent(true);
        static AutoResetEvent waitHandler2 = new AutoResetEvent(true);
        static AutoResetEvent waitHandler3 = new AutoResetEvent(true);

        static int x = 0;

        static int Place = 0;

        static int Lap1 = 0;
        static int Lap2 = 0;
        static int Lap3 = 0;

        const int LIMIT_X_ONE = 740;
        const int LIMIT_Y_ONE = 380;
        const int PITSTOP_TIME = 2000;

        //const int LIMIT_X_TWO = 780;
        //const int LIMIT_Y_TWO = 380;

        //const int LIMIT_X_THREE = 820;
        //const int LIMIT_Y_THREE = 380;

        const int MAX_NUM_LAP = 2;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            //pbCar1.Value = 100;
        }

        private void Move(Button btn, Image img,  TranslateTransform transl, RotateTransform rotateTransform1,
                                                  RotateTransform rotateTransform2, RotateTransform rotateTransform3, 
                                                  RotateTransform rotateTransform4, TextBlock tb, AutoResetEvent waitHandler,
                                                   ref bool StopTheCar, ref bool PitStop, int Lap
                                                  )
        {
           

            while (true)
            {
                if(StopTheCar||PitStop)
                {
                    waitHandler.WaitOne();
                }
                else
                {
 
                    MoveByXtoRight(btn, transl, rotateTransform1, waitHandler,ref StopTheCar,ref PitStop);
 
                    RotateImage(img, rotateTransform1);
                    MoveByYtoDown(btn, transl, rotateTransform1, waitHandler,ref StopTheCar,ref PitStop);
                    RotateImage(img, rotateTransform2);
                    MoveByXtoLeft(btn, transl, rotateTransform1, waitHandler,ref StopTheCar,ref PitStop);
                    RotateImage(img, rotateTransform3);
                    MoveByYtoUp(btn, transl, rotateTransform1, waitHandler,ref StopTheCar,ref PitStop);
                    RotateImage(img, rotateTransform4);
                    Lap++;

                    if (Lap == MAX_NUM_LAP)
                    {

                        StopTheCar = true;
                        UpdateTextBox(tb);
                        Place++;
                    }
                }              
            }
        }


        private void UpdateTextBox(TextBlock tb)
        {

            Action action = () => { UpdateText(tb); };
            tb.Dispatcher.BeginInvoke(action);
           
        }

        private void UpdateText(TextBlock tb)
        {
            tb.Text = $" [-{Place}-]";
        }

        private void MoveByXtoRight(Button btn, TranslateTransform transl, RotateTransform rotateTransform1, AutoResetEvent waitHandler,ref  bool StopTheCar,ref bool PitStop)
        {
            int distance = 0;
     
            while (distance <= LIMIT_X_ONE)
            {
                if (StopTheCar || PitStop)
                {
                    waitHandler.WaitOne();
                }
                else
                {

                    distance += 10;
                    Thread.Sleep(rnd.Next(100, 300));
                    UpdatePositionButton(btn, transl, distance);
                }

            }
            
        }
        private void MoveByXtoLeft(Button btn, TranslateTransform transl, RotateTransform rotateTransform1, AutoResetEvent waitHandler,ref bool StopTheCar, ref  bool PitStop)
        {
            int distance = LIMIT_X_ONE;

            while (distance >=10)
            {
                if (StopTheCar || PitStop)
                {
                    waitHandler.WaitOne();
                }
                else 
                {
                    distance -= 10;
                    Thread.Sleep(rnd.Next(100, 300));
                    UpdatePositionButton(btn, transl, distance);
                }

            }
        }
        private void UpdatePositionButton(Button btn,  TranslateTransform transl, int distance)
        {
            Action action = () => { SetDistance(btn,  transl, distance); };
            btn.Dispatcher.BeginInvoke(action);
        }
        private void SetDistance(Button btn,  TranslateTransform transl, int dist)
        {

            transl.X = dist;
            btn.RenderTransform = transl;

        }

        private void MoveByYtoDown(Button btn, TranslateTransform transl, RotateTransform rotateTransform1, AutoResetEvent waitHandler,ref bool StopTheCar, ref bool PitStop)
        {
 

            int distance = 0;

            while (distance <= LIMIT_Y_ONE)
            {
                if (StopTheCar || PitStop)
                {
                    waitHandler.WaitOne();
                }
                else
                {
                    distance += 10;
                    Thread.Sleep(rnd.Next(100, 300));
                    UpdatePositionButtonY(btn, transl, distance);
                }
            }
        }

        private void MoveByYtoUp(Button btn, TranslateTransform transl, RotateTransform rotateTransform1, AutoResetEvent waitHandler,ref bool StopTheCar,ref bool PitStop)
        {


            int distance = LIMIT_Y_ONE;

            while (distance >= 10)
            {
                if (StopTheCar || PitStop)
                {
                    waitHandler.WaitOne();
                }
                else
                {
                    //distance += rnd.Next(10, 30);
                    distance -= 10;
                    Thread.Sleep(rnd.Next(100,500));
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
            img.RenderTransform = transf;
        }

        private void FuelConsumption(AutoResetEvent waitHandler, ProgressBar pb, ref bool StopTheCar, ref bool PitStop)
        {

            int count = 100;

            while (count >= 0)
            {
               
                count -= rnd.Next(1, 4);
                Thread.Sleep(1000);

                UpdateProgressBaar(pb, count);

                if (count <= 20 && count >=15)
                {

                    
                    PitStop = true;


                    int key = rnd.Next(0, 2);
                    switch (key)
                    {
                        case 0:
                           
                            Thread.Sleep(PITSTOP_TIME);
                            PitStop = false;
                            count = 100;
                            waitHandler.Set();
                           
                            break;
                        case 1:

                            waitHandler.Set();
                            PitStop = false;
                            break;
                    }
                }
                else
                {

                    PitStop = false;
                }
                if (count<=0)
                {

                    StopTheCar = true;


                    //TODO
                    //btnRacer.Opacity = 0.5;

                }

            }
            
        }
        private void UpdateProgressBaar(ProgressBar pb, int i)
        {
            Action action = () => { SetProgress(pb,i); };
            pb.Dispatcher.BeginInvoke(action);
        }
        private void SetProgress(ProgressBar pb, int i)
        {
            pb.Value = i;
        }

        void ClearFields()
        {
            Place = 0;

            Lap1 = 0;
            Lap2 = 0;
            Lap3 = 0;

            tbPos1.Text = "";
            tbPos2.Text = "";
            tbPos3.Text = "";

        }

        void StartClick(object sender, RoutedEventArgs e)
        {

            ClearFields();

            fuel1 = new Thread(() => FuelConsumption(waitHandler1, pbCar1, ref StopTheCar1,ref  PitStop1));

            t1 = new Thread(() => Move(btnRacer1, imgCar1 ,transform1, rotateTransform1, rotateTransform2, rotateTransform3, 
                                                                rotateTransform4, tbPos1,  waitHandler1,ref StopTheCar1,ref PitStop1, Lap1));

            fuel2 = new Thread(() => FuelConsumption(waitHandler2, pbCar2, ref StopTheCar2,ref PitStop2));
            t2 = new Thread(() => Move(btnRacer2, imgCar2, transform2, rotateTransform21, rotateTransform22, rotateTransform23,
                                                                rotateTransform24, tbPos2, waitHandler2,ref StopTheCar2,ref PitStop2, Lap2));

            fuel3 = new Thread(() => FuelConsumption(waitHandler3, pbCar3,ref StopTheCar3,ref PitStop3));
            t3 = new Thread(() => Move(btnRacer3, imgCar3, transform3, rotateTransform31, rotateTransform32, rotateTransform33,
                                                                rotateTransform34, tbPos3, waitHandler3,ref StopTheCar3,ref PitStop3, Lap3));



            //t2.IsBackground = true;
            fuel1.Start();
            t1.Start();

            fuel2.Start();
            t2.Start();

            fuel3.Start();
            t3.Start();
           

        }
    }
}
