using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ПР__2_Вдовін
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TranslateTransform myTriangleTransform, myRectangleTransform;
        private DispatcherTimer myTimer;
        private double triangleSpeed = 4, rectangleSpeed = 2, circleRotationSpeed = 10;
        private double triangleY = 0, rectangleX = 0, circleAngle = 40;
        private bool isMovingDown = true, isMovingRight = true;
        private Transform3DGroup myTransform1, myTransform2;
        private AxisAngleRotation3D myYAxis, myZAxis, myYAxis2, myZAxis2;
        private RotateTransform3D myYRotate, myZRotate, myYRotate2, myZRotate2;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myTriangleTransform = new TranslateTransform();
            triangle.RenderTransform = myTriangleTransform;

            myRectangleTransform = new TranslateTransform();
            rectangle.RenderTransform = myRectangleTransform;

            myYAxis = new AxisAngleRotation3D();
            myYAxis.Axis = new Vector3D(0, 1, 0);
            myYAxis.Angle = 0;
            myYRotate = new RotateTransform3D(myYAxis);
            myZAxis = new AxisAngleRotation3D();
            myZAxis.Axis = new Vector3D(0, 0, 1);
            myZAxis.Angle = 0;
            myZRotate = new RotateTransform3D(myZAxis);
            myTransform1 = new Transform3DGroup();
            figure4.Transform = myTransform1;
            myTransform1.Children.Add(myYRotate);
            myTransform1.Children.Add(myZRotate);

            myYRotate2 = new RotateTransform3D();
            myYAxis2 = new AxisAngleRotation3D();
            myYAxis2.Axis = new Vector3D(0, 1, 0);
            myYAxis2.Angle = 0;
            myYRotate2.Rotation = myYAxis2;
            myZRotate2 = new RotateTransform3D();
            myZAxis2 = new AxisAngleRotation3D();
            myZAxis2.Axis = new Vector3D(0, 0, 1);
            myZAxis2.Angle = 0;
            myZRotate2.Rotation = myZAxis2;
            myTransform2 = new Transform3DGroup();
            figure5.Transform = myTransform2;
            myTransform2.Children.Add(myYRotate2);
            myTransform2.Children.Add(myZRotate2);

            myTimer = new DispatcherTimer();
            myTimer.Tick += new EventHandler(MyTimer_Tick);
            myTimer.Interval = TimeSpan.FromMilliseconds(30);
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            triangleY += isMovingDown ? triangleSpeed : -triangleSpeed;
            if (triangleY >= 100 || triangleY <= 0)
                isMovingDown = !isMovingDown;
            myTriangleTransform.Y = triangleY;

            rectangleX += isMovingRight ? rectangleSpeed : -rectangleSpeed;
            if (rectangleX > 250 - rectangle.ActualWidth || rectangleX < 0)
                isMovingRight = !isMovingRight;
            myRectangleTransform.X = rectangleX;

            circleAngle += circleRotationSpeed;
            if (circleAngle >= 360)
                circleAngle = 0;
            RotateTransform rotation = new RotateTransform(circleAngle, circle.ActualWidth / 2, circle.ActualHeight / 2);
            circle.RenderTransform = rotation;

            myYAxis.Angle += 3;
            myZAxis.Angle += 3;

            myYAxis2.Angle -= 1;
            myZAxis2.Angle -= 1;
        }

        private void StartMovement_Click(object sender, RoutedEventArgs e)
        {
            myTimer.Start();
        }

        private void StopMovement_Click(object sender, RoutedEventArgs e)
        {
            myTimer.Stop();
        }
    }
}
