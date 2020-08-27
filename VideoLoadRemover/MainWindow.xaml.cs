using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideoLoadRemover
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer timer;
        Bitmap bmp;
        Bitmap savedbmp;
        int lst;
        INI settings;
        string filepath;

        public MainWindow()
        {
            InitializeComponent();

            filepath = "settings.ini";

            settings = new INI(filepath);

            SettingsToUI();

            bmp = new Bitmap(settings.GetValue("w"), settings.GetValue("h"));
            savedbmp = new Bitmap(settings.GetValue("w"), settings.GetValue("h"));
            lst = settings.GetValue("t");
            btn_Stop.IsEnabled = false;

            //create files if they're missing
            if (!File.Exists("saved.bmp"))
            {
                savedbmp.Save("saved.bmp");
            }
        }

        

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btn_Start.IsEnabled = false;
            btn_Stop.IsEnabled = true;
            lst = Convert.ToInt32(tbx_LT.Text);
            tbx_LT.IsEnabled = false;
            tbx_FPS.IsEnabled = false;
            savedbmp = new Bitmap("saved.bmp");
            timer = new Timer(1000 / Convert.ToInt32(tbx_FPS.Text));
            timer.Elapsed += OnTimedEvent;

            timer.Start();

        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            btn_Start.IsEnabled = true;
            btn_Stop.IsEnabled = false;
            tbx_LT.IsEnabled = true;
            tbx_FPS.IsEnabled = true;
            savedbmp.Dispose();
        }


        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                //set default values
                int x = 0;
                int y = 0;
                int w = 100;
                int h = 100;

                //attempt to read values from UI
                Dispatcher.Invoke(new Action(() =>
                {
                    try
                    {
                        x = Convert.ToInt32(tbx_X.Text);
                        y = Convert.ToInt32(tbx_Y.Text);
                        w = Convert.ToInt32(tbx_W.Text);
                        h = Convert.ToInt32(tbx_H.Text);
                    }
                    catch (Exception ex)
                    {
                        x = 0;
                        y = 0;
                        w = 100;
                        h = 100;
                    }
                }
                ));
                Bitmap bitmap = new Bitmap(w, h);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(x, y, 0, 0, bitmap.Size);
                }

                bmp = (Bitmap)bitmap.Clone();

                try
                {
                    Response response = BMPExtend.GenResponse(bmp, savedbmp);


                    Dispatcher.Invoke(new Action(() =>
                    {
                        if (response.val < lst)
                        {
                            cbx_Loading.IsChecked = true;
                        }
                        else
                        {
                            cbx_Loading.IsChecked = false;
                        }

                        if (cbx_Preview.IsChecked == true)
                        {
                            //resize image to fit preview
                            Bitmap tmp = new Bitmap(bitmap, new System.Drawing.Size(150, 150));
                            imgPreview.Source = BMPExtend.BitmapToImageSource(tmp);
                            tbx_LV.Text = response.response;
                        }
                    }));
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception exp)
            {
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                settings = new INI(filepath);

                SettingsToUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No settings found, be sure to save settings first");
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            UIToSettings();

            settings.Save(filepath);

            MessageBox.Show("Saved!");
        }

        private void SettingsToUI()
        {
            tbx_X.Text = settings.GetValue("x").ToString();
            tbx_Y.Text = settings.GetValue("y").ToString();
            tbx_W.Text = settings.GetValue("w").ToString();
            tbx_H.Text = settings.GetValue("h").ToString();
            tbx_LT.Text = settings.GetValue("t").ToString();
            tbx_FPS.Text = settings.GetValue("fps").ToString();
        }

        private void UIToSettings()
        {
            settings.SetValue("x", Convert.ToInt32(tbx_X.Text));
            settings.SetValue("y", Convert.ToInt32(tbx_Y.Text));
            settings.SetValue("w", Convert.ToInt32(tbx_W.Text));
            settings.SetValue("h", Convert.ToInt32(tbx_H.Text));
            settings.SetValue("t", Convert.ToInt32(tbx_LT.Text));
            settings.SetValue("fps", Convert.ToInt32(tbx_FPS.Text));
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bitmap newbmp = new Bitmap(bmp);
                savedbmp.Dispose();
                bmp.Dispose();
                newbmp.Save("saved.bmp");
                savedbmp = newbmp;
                bmp = newbmp;
                MessageBox.Show("Screenshot saved!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save failed, is the image or livesplit open?\n" + ex.Message);
            }
        }
    }
}
