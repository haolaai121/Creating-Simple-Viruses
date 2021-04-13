using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace LAB2_Bai1._4
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            //cho phép Service có khả năng xử lí những thay đổi trạng thái của Session
            CanHandleSessionChangeEvent = true;
        }

       
        protected override void OnStart(string[] args)
        {

        }
        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            // SessionUnlock: Bước cuối cùng để thực hiện đăng nhập sau khi Lock hoặc Shutdown
            // SessionLogon: // Bước cuối cùng để thực hiện đăng nhập sau khi Sign out
            if ((changeDescription.Reason == SessionChangeReason.SessionUnlock) || (changeDescription.Reason != SessionChangeReason.SessionUnlock && changeDescription.Reason == SessionChangeReason.SessionLogon))
            {
                // Pop up
                Interop.ShowMessageBox("MSSV: 18521336 !", "Pop up");
            }
        }
        protected override void OnStop()
        {
        }
    }
}
