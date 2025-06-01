namespace PPS
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnEnable;
        private System.Windows.Forms.Button btnDisable;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnOpenMousePanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnEnable = new Button();
            btnDisable = new Button();
            lblStatus = new Label();
            btnOpenMousePanel = new Button();
            SuspendLayout();
            // 
            // btnEnable
            // 
            btnEnable.BackColor = SystemColors.ControlLight;
            btnEnable.Location = new Point(31, 54);
            btnEnable.Margin = new Padding(3, 4, 3, 4);
            btnEnable.Name = "btnEnable";
            btnEnable.Size = new Size(120, 51);
            btnEnable.TabIndex = 0;
            btnEnable.Text = "打开鼠标加速";
            btnEnable.UseVisualStyleBackColor = false;
            btnEnable.Click += btnEnable_Click;
            // 
            // btnDisable
            // 
            btnDisable.BackColor = SystemColors.ControlLight;
            btnDisable.Location = new Point(178, 54);
            btnDisable.Margin = new Padding(3, 4, 3, 4);
            btnDisable.Name = "btnDisable";
            btnDisable.Size = new Size(120, 51);
            btnDisable.TabIndex = 1;
            btnDisable.Text = "关闭鼠标加速";
            btnDisable.UseVisualStyleBackColor = false;
            btnDisable.Click += btnDisable_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(94, 21);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(126, 19);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "鼠标加速状态：未知";
            lblStatus.Click += lblStatus_Click;
            // 
            // btnOpenMousePanel
            // 
            btnOpenMousePanel.BackColor = SystemColors.ControlLight;
            btnOpenMousePanel.Location = new Point(31, 116);
            btnOpenMousePanel.Margin = new Padding(3, 4, 3, 4);
            btnOpenMousePanel.Name = "btnOpenMousePanel";
            btnOpenMousePanel.Size = new Size(267, 35);
            btnOpenMousePanel.TabIndex = 4;
            btnOpenMousePanel.Text = "打开鼠标控制面板";
            btnOpenMousePanel.UseVisualStyleBackColor = false;
            btnOpenMousePanel.Click += btnOpenMousePanel_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(320, 164);
            Controls.Add(btnOpenMousePanel);
            Controls.Add(btnEnable);
            Controls.Add(btnDisable);
            Controls.Add(lblStatus);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PPS鼠标加速开关";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}