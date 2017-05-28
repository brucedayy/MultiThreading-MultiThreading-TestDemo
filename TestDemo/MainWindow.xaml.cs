using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace TestDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool btnStatusOne = true;
        private static bool btnStatusTwo = true;
        private static bool btnStatusThree = true;
        private static bool btnStatusFour = true;
        public MainWindow()
        {
            InitializeComponent();
        }
        #region
        private void mainStkME(object sender, MouseEventArgs e)
        {
            stkMainFunc.Visibility = Visibility.Visible;
            stkMainFunc.Width = 100;
            txbInput.Focus();
        }
        private void mainStkML(object sender, MouseEventArgs e)
        {
            stkMainFunc.Visibility = Visibility.Hidden;
            stkMainFunc.Width = 50;
        }
        #endregion

        private async void btnGo_Click(object sender, RoutedEventArgs e)
        {
            #region
            CFuncChoices.FuncStatus status = CFuncChoices.ShowChoicesResult(txbInput.Text);
            lbWork.Content = status;
            switch (status)
            {
                case CFuncChoices.FuncStatus.equal:
                    if (btnStatusOne)
                        await funOne();
                    break;
                case CFuncChoices.FuncStatus.more:
                    if (btnStatusTwo)
                        await funTwo();
                    break;
                case CFuncChoices.FuncStatus.lesseven:
                    if (btnStatusThree)
                        await funThree();
                    break;
                case CFuncChoices.FuncStatus.lessodd:
                    if (btnStatusFour)
                        await funFour();
                    break;
                default: break;
            }
            #endregion
        }

        private Task funOne()
        {
            return Task.Run(() =>
            {
                btnStatusOne = !btnStatusOne;
                int count = 0;
                Timer tmr = new Timer((x) =>
                {
                    this.InvokeIfNeeded(() =>
                    {
                        lbFunOne.Content = count++.ToString();
                    }, System.Windows.Threading.DispatcherPriority.Normal);
                });
                tmr.Change(0, 1000);
            });
        }

        private static bool LineStatus = true;
        private Task funTwo()
        {
            btnStatusTwo = !btnStatusTwo;
            return Task.Run(() =>
            {
                this.InvokeIfNeeded(() =>
                {
                    foreach (Line line in cvsFunTwo.Children)
                    {
                        line.Visibility = Visibility.Visible;
                    }
                }, System.Windows.Threading.DispatcherPriority.Normal);
                Timer tmr = new Timer((x) =>
                  {
                      this.InvokeIfNeeded(() =>
                      {
                          if (LineStatus)
                          {
                              lineone.Y2 = 50;
                              linetwo.Y1 = 50;
                              linetwo.Y2 = -50;
                              linethree.Y1 = -50;
                          }
                          else
                          {
                              lineone.Y2 = -50;
                              linetwo.Y1 = -50;
                              linetwo.Y2 = 50;
                              linethree.Y1 = 50;
                          }
                          LineStatus = !LineStatus;
                      }, System.Windows.Threading.DispatcherPriority.Normal);
                  });
                tmr.Change(0, 100);
            });
        }
        private Task funThree()
        {
            btnStatusThree = !btnStatusThree;
            return Task.Run(() =>
            {
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        int count = 0;
                        FileStream fs = new FileStream(@"./demo.txt", FileMode.Open, FileAccess.Read, FileShare.None);
                        StreamReader sr = new StreamReader(fs);
                        List<string> lstTime = new List<string>();
                        while (!sr.EndOfStream)
                        {
                            lstTime.Add(sr.ReadLine());
                            count++;
                        }
                        sr.Close();
                        fs.Close();
                        this.InvokeIfNeeded(() =>
                        {
                            lbLineNum.Content = count.ToString();
                            lstDir.ItemsSource = lstTime;
                        }, System.Windows.Threading.DispatcherPriority.Normal);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                });
                thread.Start();
            });
        }

        private void webLoadComp(object sender, EventArgs args)
        {
            MSHTML.HTMLDocument dom = (MSHTML.HTMLDocument)webBrow.Document;
            dom.documentElement.style.overflow = "hidden";
            dom.body.setAttribute("scroll", "no");
        }

        private Task funFour()
        {
            btnStatusFour = !btnStatusFour;
            return Task.Run(() =>
            {
                this.InvokeIfNeeded(() =>
                {
                    string url = System.Environment.CurrentDirectory + "\\index.html";
                    webBrow.Navigate(new Uri(url));
                }, System.Windows.Threading.DispatcherPriority.Normal);
            });
        }

    }
}
