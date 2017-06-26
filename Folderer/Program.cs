using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Folderer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string[] files = System.IO.Directory.GetFiles(fbd.SelectedPath, "*.*")
                    .Where(s => s.ToLower().EndsWith(".jpg") || s.ToLower().EndsWith(".jpeg") || s.ToLower().EndsWith(".gif") || s.ToLower().EndsWith(".mp4") || s.ToLower().EndsWith(".3gp") || s.ToLower().EndsWith(".mov")).ToArray();

                if (files.Length > 0)
                {
                    Console.WriteLine(files.Length + " adet dosya bulundu..");
                    Console.WriteLine("Dosyalar işleniyor..");
                    Console.WriteLine("...");

                    Console.WriteLine();

                    string destinationRootFolderPath = fbd.SelectedPath + "\\temp";

                    System.IO.Directory.CreateDirectory(destinationRootFolderPath);
                    Console.WriteLine("'temp' klasörü oluşturuldu..");
                    Console.WriteLine("...");

                    //Console.WriteLine("Örnek bir dosya yolu : " + files[0]);
                    //Console.Write("Dosyanın bir üst klasör adını giriniz : ");

                    //string parentFolderName = Console.ReadLine();
                    //Console.WriteLine("...");

                    int left = files[0].LastIndexOf('\\');

                    foreach (string file in files)
                    {
                        Console.WriteLine("'" + file + "' taşınıyor..");

                        string fName = file.Substring(left + 1);
                        // c://windows/users/murat/desktop/IMG-20160415-WA0002.jpeg
                        // c://windows/users/murat/desktop/20160415-WA0002.jpeg

                        fName = fName.Replace("_", "-");

                        if (fName.StartsWith("IMG-"))
                            fName = fName.Replace("IMG-", "");

                        if (fName.StartsWith("VID-"))
                            fName = fName.Replace("VID-", "");

                        if (fName.StartsWith("Video-"))
                            fName = fName.Replace("Video-", "");

                        string folderName = fName.Split('-')[0];
                        folderName = folderName.Substring(0, 4) + "-" + folderName.Substring(4, 2) + "-" + folderName.Substring(6, 2);

                        string destFolderName = destinationRootFolderPath + "\\" + folderName;

                        if (!System.IO.Directory.Exists(destFolderName))
                        {
                            System.IO.Directory.CreateDirectory(destFolderName);
                        }

                        System.IO.File.Move(file, destFolderName + "\\" + fName);
                    }
                }

            }
            else
            {
                Console.WriteLine("İşlem iptal edildi. Klasör seçilmedi.");
            }

            Console.WriteLine();
            Console.WriteLine("İşlem tamamlandı lütfen bir tuşa basınız.");
            Console.ReadKey();

        }

        public void adad()
        {
            // asasa
        }
    }
}
