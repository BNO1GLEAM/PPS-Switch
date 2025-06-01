using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PPS
{
    public partial class MainForm : Form
    {
        private const uint SpiGetmouse = 0x0003;
        private const uint SpiSetmouse = 0x0004;

        public enum Spif
        {
            None = 0x00,
            SpifUpdateinifile = 0x01,
            SpifSendchange = 0x02
        }

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = true)]
        public static extern bool SystemParametersInfoGet(uint action, uint param, IntPtr vparam, uint fWinIni);

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = true)]
        public static extern bool SystemParametersInfoSet(uint action, uint param, IntPtr vparam, uint fWinIni);

        public MainForm()
        {
            InitializeComponent();
            this.MaximizeBox = false; // 禁用最大化按钮
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // 可选：防止窗口被拉伸
            RefreshPointerPrecisionStatus();
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            SetPointerPrecision(true);
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            SetPointerPrecision(false);
        }

        private void SetPointerPrecision(bool enable)
        {
            var mouseParams = GetCurrentMouseParams();
            mouseParams[2] = enable ? 1 : 0;
            var handle = GCHandle.Alloc(mouseParams, GCHandleType.Pinned);
            SystemParametersInfoSet(SpiSetmouse, 0, handle.AddrOfPinnedObject(), (uint)Spif.SpifSendchange);
            handle.Free();

            MessageBox.Show(enable ? "鼠标加速已打开" : "鼠标加速已关闭", "提示");
            RefreshPointerPrecisionStatus();
        }

        private void RefreshPointerPrecisionStatus()
        {
            var mouseParams = GetCurrentMouseParams();
            bool enabled = mouseParams[2] != 0;
            lblStatus.Text = enabled ? "鼠标加速状态：已开启" : "鼠标加速状态：已关闭";
            lblStatus.ForeColor = enabled ? System.Drawing.Color.Red : System.Drawing.Color.Green;
        }

        private static int[] GetCurrentMouseParams()
        {
            var mouseParams = new int[3];
            var handle = GCHandle.Alloc(mouseParams, GCHandleType.Pinned);
            SystemParametersInfoGet(SpiGetmouse, 0, handle.AddrOfPinnedObject(), (uint)Spif.None);
            handle.Free();
            return mouseParams;
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void btnOpenMousePanel_Click(object sender, EventArgs e)
        {
            try
            {
                // 使用 control.exe 打开 main.cpl
                Process.Start(new ProcessStartInfo
                {
                    FileName = "control.exe",
                    Arguments = "main.cpl",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法打开鼠标控制面板: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}