using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    public void ShowPictures()
    {
      //get last 3 files in your observed Directory
      var files = System.IO.Directory.GetFiles(Program.sourceDirectory, Program.filter, System.IO.SearchOption.TopDirectoryOnly)
        .OrderByDescending(f => System.IO.File.GetCreationTime(f))
        .Take(3)
        .ToList();


      pictureBox1.Image = null;
      pictureBox2.Image = null;
      pictureBox3.Image = null;


      if (!files.Any())
      {
        return;
      }

      var actImage = Image.FromFile(files[0]);
      Bitmap b = new Bitmap(actImage);
      Image i = resizeImage(b, new Size(100, 100));
      pictureBox1.Image = i;
      actImage.Dispose();

      if (files.Count == 1)
      {
        return;
      }

      actImage = Image.FromFile(files[1]);
       b = new Bitmap(actImage);
       i = resizeImage(b, new Size(100, 100));
      pictureBox2.Image = i;
      actImage.Dispose();

      if (files.Count == 2)
      {
        return;
      }

      actImage = Image.FromFile(files[2]);
      b = new Bitmap(actImage);
      i = resizeImage(b, new Size(100, 100));
      pictureBox3.Image = i;
      actImage.Dispose();

    }

    private System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
    {
      //Get the image current width  
      int sourceWidth = imgToResize.Width;
      //Get the image current height  
      int sourceHeight = imgToResize.Height;
      float nPercent = 0;
      float nPercentW = 0;
      float nPercentH = 0;
      //Calulate  width with new desired size  
      nPercentW = ((float)size.Width / (float)sourceWidth);
      //Calculate height with new desired size  
      nPercentH = ((float)size.Height / (float)sourceHeight);
      if (nPercentH < nPercentW)
        nPercent = nPercentH;
      else
        nPercent = nPercentW;
      //New Width  
      int destWidth = (int)(sourceWidth * nPercent);
      //New Height  
      int destHeight = (int)(sourceHeight * nPercent);
      Bitmap b = new Bitmap(destWidth, destHeight);
      Graphics g = Graphics.FromImage((System.Drawing.Image)b);
      g.InterpolationMode = InterpolationMode.HighQualityBicubic;
      // Draw image with new width and height  
      g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
      g.Dispose();
      return (System.Drawing.Image)b;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      ShowPictures();
    }
  }
}
