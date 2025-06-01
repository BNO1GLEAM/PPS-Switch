using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PPS
{
    // 主窗体类，继承自Form
    public partial class MainForm : Form
    {
        // SPI常量：用于获取和设置鼠标参数
        private const uint SpiGetmouse = 0x0003;
        private const uint SpiSetmouse = 0x0004;

        // 枚举：用于指定SystemParametersInfo的附加选项
        public enum Spif
        {
            None = 0x00,                // 无操作
            SpifUpdateinifile = 0x01,   // 更新ini文件
            SpifSendchange = 0x02       // 通知系统参数已更改
        }

        // 导入user32.dll中的SystemParametersInfo函数（用于获取参数）
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = true)]
        public static extern bool SystemParametersInfoGet(uint action, uint param, IntPtr vparam, uint fWinIni);

        // 导入user32.dll中的SystemParametersInfo函数（用于设置参数）
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = true)]
        public static extern bool SystemParametersInfoSet(uint action, uint param, IntPtr vparam, uint fWinIni);

        // 构造函数：初始化窗体及控件
        public MainForm()
        {
            InitializeComponent();
            this.MaximizeBox = false; // 禁用最大化按钮
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // 防止窗口被拉伸
            RefreshPointerPrecisionStatus(); // 刷新鼠标加速状态显示
        }

        // “开启加速”按钮点击事件
        private void btnEnable_Click(object sender, EventArgs e)
        {
            SetPointerPrecision(true);
        }

        // “关闭加速”按钮点击事件
        private void btnDisable_Click(object sender, EventArgs e)
        {
            SetPointerPrecision(false);
        }

        // 设置鼠标加速开关
        private void SetPointerPrecision(bool enable)
        {
            var mouseParams = GetCurrentMouseParams(); // 获取当前鼠标参数
            mouseParams[2] = enable ? 1 : 0; // 设置加速开关（1为开启，0为关闭）
            var handle = GCHandle.Alloc(mouseParams, GCHandleType.Pinned); // 固定数组内存
            SystemParametersInfoSet(SpiSetmouse, 0, handle.AddrOfPinnedObject(), (uint)Spif.SpifSendchange); // 设置参数
            handle.Free(); // 释放内存

            MessageBox.Show(enable ? "鼠标加速已打开" : "鼠标加速已关闭", "提示"); // 弹窗提示
            RefreshPointerPrecisionStatus(); // 刷新状态显示
        }

        // 刷新界面上鼠标加速状态的显示
        private void RefreshPointerPrecisionStatus()
        {
            var mouseParams = GetCurrentMouseParams(); // 获取当前鼠标参数
            bool enabled = mouseParams[2] != 0; // 判断加速是否开启
            lblStatus.Text = enabled ? "鼠标加速状态：已开启" : "鼠标加速状态：已关闭"; // 设置状态文本
            lblStatus.ForeColor = enabled ? System.Drawing.Color.Red : System.Drawing.Color.Green; // 设置状态颜色
        }

        // 获取当前鼠标参数
        private static int[] GetCurrentMouseParams()
        {
            var mouseParams = new int[3]; // 创建参数数组
            var handle = GCHandle.Alloc(mouseParams, GCHandleType.Pinned); // 固定数组内存
            SystemParametersInfoGet(SpiGetmouse, 0, handle.AddrOfPinnedObject(), (uint)Spif.None); // 获取参数
            handle.Free(); // 释放内存
            return mouseParams; // 返回参数数组
        }

        // 状态标签点击事件（未实现功能）
        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        // “打开鼠标控制面板”按钮点击事件
        private void btnOpenMousePanel_Click(object sender, EventArgs e)
        {
            try
            {
                // 使用 control.exe 打开鼠标设置面板
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

        // 窗体加载事件（未实现功能）
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}