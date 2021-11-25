using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
  static class Program
  {
    static FileSystemWatcher fswatch;
    internal static string sourceDirectory => Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
    internal static string filter = "*.jpg";
    static Form1 form;
    [STAThread]
    static void Main()
    {
      Application.SetHighDpiMode(HighDpiMode.SystemAware);
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      fswatch = new FileSystemWatcher(sourceDirectory,filter);
      fswatch.EnableRaisingEvents = true;
      fswatch.Changed += Fswatch_Changed;
      fswatch.Deleted  += Fswatch_Changed;
      fswatch.Created  += Fswatch_Changed;

      form = new Form1();
      form.FormClosed += Form_FormClosed;
      Application.Run(form);
    }

    private static void Form_FormClosed(object sender, FormClosedEventArgs e)
    {
      fswatch.Changed -= Fswatch_Changed;
      fswatch.Deleted -= Fswatch_Changed;
      fswatch.Created -= Fswatch_Changed;
      fswatch.Dispose();
    }

    private static void Fswatch_Changed(object sender, FileSystemEventArgs e)
    {
      form.ShowPictures();
    }
  }
}
