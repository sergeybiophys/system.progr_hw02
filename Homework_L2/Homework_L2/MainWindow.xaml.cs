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
        TranslateTransform transform1 = new TranslateTransform();
        RotateTransform rotateTransform1 = new RotateTransform(90);
        Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

        }

        private void Move(Button btn,  TranslateTransform transl, RotateTransform rotateTransform1 )
        {
            while(true)
            {
                MoveByXtoRight(btn, transl, rotateTransform1);
                MoveByYtoDown(btn, transl, rotateTransform1);
                MoveByXtoLeft(btn, transl, rotateTransform1);
                MoveByYtoUp(btn, transl, rotateTransform1);
            }
     
            //RotateButton(btn, transl, rotateTransform1);
        }

        private void MoveByXtoRight(Button btn, TranslateTransform transl, RotateTransform rotateTransform1)
        {
            int distance = 0;
    
            while (distance <= 640)
            {
                //distance += rnd.Next(10, 30);
                distance += 10;
                Thread.Sleep(100);
                UpdatePositionButton(btn, transl, distance);
            }
            //if(distance==640)
            //{
            //RotateButton(btn, transl, rotateTransform1);
            //}

        }
        private void MoveByXtoLeft(Button btn, TranslateTransform transl, RotateTransform rotateTransform1)
        {
            int distance = 640;

            while (distance >=20)
            {
                //distance += rnd.Next(10, 30);
                distance -= 10;
                Thread.Sleep(100);
                UpdatePositionButton(btn, transl, distance);
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

            while (distance <= 320)
            {
   
                //distance += rnd.Next(10, 30);
                distance += 10;
                Thread.Sleep(100);
                UpdatePositionButtonY(btn, transl, distance);
            }

        }

        private void MoveByYtoUp(Button btn, TranslateTransform transl, RotateTransform rotateTransform1)
        {


            int distance = 320;

            while (distance >= 20)
            {

                //distance += rnd.Next(10, 30);
                distance -= 10;
                Thread.Sleep(100);
                UpdatePositionButtonY(btn, transl, distance);
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

        private void RotateButton(Button btn, TranslateTransform transl, RotateTransform transf)
        {
            UpdateButtonAngle(btn, transl,  transf);
        }

        private void UpdateButtonAngle(Button btn, TranslateTransform transl, RotateTransform transf)
        {

            Action action = () => { RotateCW(btn, transl, transf); };
            btn.Dispatcher.BeginInvoke(action);

        }
        private void RotateCW(Button btn, TranslateTransform transl, RotateTransform transf)
        {
            //transl.X = 650;
            //var buttonLocation = btn.PointToScreen().
            //transf.Angle = 90;
            //transf.CenterX = transl.Y;
            //transf.CenterY = transl.X;
            btn.RenderTransform = transf;
            //btn.RenderTransform = transl;
          

        }

        void StartClick(object sender, RoutedEventArgs e)
        {

            t1 = new Thread(() => Move(btnRacer1, transform1, rotateTransform1));

            //t2.IsBackground = true;

            t1.Start();
   
        }
    }
}
