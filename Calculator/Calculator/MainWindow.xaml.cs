using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public enum Operations
    {
        Division,
        Multiplication,
        Minus,
        Plus

    }
    public partial class MainWindow : Window
    {
        string calculations;

        float temp = 0;

        bool cleared=false;

        string operation = "";

        string output = "";

        #region IDidNotWriteThisCode
        //https://youtu.be/4JK9VtU8bYw

        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }
            return (IntPtr)0;
        }

        private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
            if (monitor != IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }
            Marshal.StructureToPtr(mmi, lParam, true);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>x coordinate of point.</summary>
            public int x;
            /// <summary>y coordinate of point.</summary>
            public int y;
            /// <summary>Construct a point of coordinates (x,y).</summary>
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public static readonly RECT Empty = new RECT();
            public int Width { get { return Math.Abs(right - left); } }
            public int Height { get { return bottom - top; } }
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }
            public RECT(RECT rcSrc)
            {
                left = rcSrc.left;
                top = rcSrc.top;
                right = rcSrc.right;
                bottom = rcSrc.bottom;
            }
            public bool IsEmpty { get { return left >= right || top >= bottom; } }
            public override string ToString()
            {
                if (this == Empty) { return "RECT {Empty}"; }
                return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
            }
            public override bool Equals(object obj)
            {
                if (!(obj is Rect)) { return false; }
                return (this == (RECT)obj);
            }
            /// <summary>Return the HashCode for this struct (not garanteed to be unique)</summary>
            public override int GetHashCode() => left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();
            /// <summary> Determine if 2 RECT are equal (deep compare)</summary>
            public static bool operator ==(RECT rect1, RECT rect2) { return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom); }
            /// <summary> Determine if 2 RECT are different(deep compare)</summary>
            public static bool operator !=(RECT rect1, RECT rect2) { return !(rect1 == rect2); }
        }

        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

#endregion
        public MainWindow()
        {
            InitializeComponent();

            //Remove whitestrip on top of WPF Window with Window style=None
            SourceInitialized += (s,e) =>
            {
                IntPtr handle = (new WindowInteropHelper(this)).Handle;
                HwndSource.FromHwnd(handle).AddHook(new HwndSourceHook(WindowProc));
            };

            this.DataContext = this;

            DivisionBtn.Content = "\u00F7";

            MinimizeBtn.Click += (s, e) => WindowState = WindowState.Minimized;
            MaximizeBtn.Click += (s, e) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            CloseBtn.Click += (s, e) => Close();

        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            string name = ((Button)sender).Name;

            if (cleared)
            {
                output = string.Empty;

                calculations = output;

                cleared = false;
            }

            switch (name)
            {
                case "ZeroBtn":
                    output += "0";
                    OutputTextBlock.Text = output;
                    break;
                case "OneBtn":
                    output += "1";
                    OutputTextBlock.Text = output;
                    break;
                case "TwoBtn":
                    output += "2";
                    OutputTextBlock.Text = output;
                    break;
                case "ThreeBtn":
                    output += "3";
                    OutputTextBlock.Text = output;
                    break;
                case "FourBtn":
                    output += "4";
                    OutputTextBlock.Text = output;
                    break;
                case "FiveBtn":
                    output += "5";
                    OutputTextBlock.Text = output;
                    break;
                case "SixBtn":
                    output += "6";
                    OutputTextBlock.Text = output;
                    break;
                case "SevenBtn":
                    output += "7";
                    OutputTextBlock.Text = output;
                    break;
                case "EightBtn":
                    output += "8";
                    OutputTextBlock.Text = output;
                    break;
                case "NineBtn":
                    output += "9";
                    OutputTextBlock.Text = output;
                    break;
            }

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            output = string.Empty;
            temp = 0;

            OutputTextBlock.Text = output;

            calculations = string.Empty;

            CalculationsTextBlock.Text = calculations;
        }

        private void DivisionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (output != "")
            {
                temp = float.Parse(output);

                calculations = output;

                CalculationsTextBlock.Text = calculations + " " + DivisionBtn.Content + " ";

                output = string.Empty;

                operation = Operations.Division.ToString();
            }
        }

        private void MultiplicationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (output != "")
            {
                temp = float.Parse(output);

                calculations = output;

                CalculationsTextBlock.Text = calculations + " " + MultiplicationBtn.Content + " ";

                output = string.Empty;

                operation = Operations.Multiplication.ToString();
            }
        }

        private void MinusBtn_Click(object sender, RoutedEventArgs e)
        {
            if(output != "")
            {
                temp = float.Parse(output);

                calculations = output;

                CalculationsTextBlock.Text = calculations + " " + MinusBtn.Content + " ";

                output = string.Empty;

                operation = Operations.Minus.ToString();
            }
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            if (output != "")
            {
                temp = float.Parse(output);

                calculations = output;

                CalculationsTextBlock.Text = calculations + " " + PlusBtn.Content + " ";

                output = string.Empty;

                
                operation = Operations.Plus.ToString();
            }
        }

        private void EqualsBtn_Click(object sender, RoutedEventArgs e)
        {
            Enum.TryParse(operation, out Operations myStatus);


            CalculationsTextBlock.Text += output.ToString();

            switch (myStatus)
            {
                case Operations.Minus:
                    float outputTemp = temp - float.Parse(output);
                    output = outputTemp.ToString();
                    OutputTextBlock.Text = output;
                    temp = 0;
                    cleared = true;
                    break;
                case Operations.Division:
                    float outputTemp2 = temp / float.Parse(output);
                    output = outputTemp2.ToString();
                    OutputTextBlock.Text = output;
                    temp = 0;
                    cleared = true;
                    break;
                case Operations.Plus:
                    float outputTemp3 = temp + float.Parse(output);
                    output = outputTemp3.ToString();
                    OutputTextBlock.Text = output;
                    temp = 0;
                    cleared = true;
                    break;
                case Operations.Multiplication:
                    float outputTemp4 = temp * float.Parse(output);
                    output = outputTemp4.ToString();
                    OutputTextBlock.Text = output;
                    temp = 0;
                    cleared = true;
                    break;
                default:
                    break;


            }
        }

        private void NegateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (output != "")
            {
                output = (float.Parse(output) * -1).ToString();
                OutputTextBlock.Text = output;
            }
        }

        private void PointBtn_Click(object sender, RoutedEventArgs e)
        {
            if (cleared)
            {
                output = string.Empty;

                calculations = output;

                CalculationsTextBlock.Text = calculations;

                cleared = false;
            }

            if (output == string.Empty)
            {
                output = "0.";
            }
            else
            {
                output = (float.Parse(output)).ToString() + ".";
            }

            OutputTextBlock.Text = output;

        }
    }
}
