namespace FastRoute
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.AddRoute = new System.Windows.Forms.Button();
            this.DelRoute = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pross = new System.Windows.Forms.ProgressBar();
            this.gateiptext = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RoutePath = new System.Windows.Forms.ComboBox();
            this.SavePathFile = new System.Windows.Forms.CheckBox();
            this.GithubLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // AddRoute
            // 
            this.AddRoute.Location = new System.Drawing.Point(12, 87);
            this.AddRoute.Name = "AddRoute";
            this.AddRoute.Size = new System.Drawing.Size(75, 23);
            this.AddRoute.TabIndex = 0;
            this.AddRoute.Text = "添加";
            this.AddRoute.UseVisualStyleBackColor = true;
            this.AddRoute.Click += new System.EventHandler(this.AddRoute_Click);
            // 
            // DelRoute
            // 
            this.DelRoute.Location = new System.Drawing.Point(139, 87);
            this.DelRoute.Name = "DelRoute";
            this.DelRoute.Size = new System.Drawing.Size(75, 23);
            this.DelRoute.TabIndex = 0;
            this.DelRoute.Text = "删除";
            this.DelRoute.UseVisualStyleBackColor = true;
            this.DelRoute.Click += new System.EventHandler(this.DelRoute_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "路由表路径";
            // 
            // pross
            // 
            this.pross.Location = new System.Drawing.Point(13, 110);
            this.pross.Name = "pross";
            this.pross.Size = new System.Drawing.Size(201, 10);
            this.pross.TabIndex = 3;
            this.pross.Visible = false;
            // 
            // gateiptext
            // 
            this.gateiptext.Location = new System.Drawing.Point(12, 64);
            this.gateiptext.Name = "gateiptext";
            this.gateiptext.Size = new System.Drawing.Size(202, 21);
            this.gateiptext.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "网关";
            // 
            // RoutePath
            // 
            this.RoutePath.FormattingEnabled = true;
            this.RoutePath.Items.AddRange(new object[] {
            "选择路由表..."});
            this.RoutePath.Location = new System.Drawing.Point(15, 28);
            this.RoutePath.Name = "RoutePath";
            this.RoutePath.Size = new System.Drawing.Size(199, 20);
            this.RoutePath.TabIndex = 4;
            this.RoutePath.SelectedIndexChanged += new System.EventHandler(this.RoutePath_SelectedIndexChanged);
            // 
            // SavePathFile
            // 
            this.SavePathFile.AutoSize = true;
            this.SavePathFile.Location = new System.Drawing.Point(139, 6);
            this.SavePathFile.Name = "SavePathFile";
            this.SavePathFile.Size = new System.Drawing.Size(72, 16);
            this.SavePathFile.TabIndex = 5;
            this.SavePathFile.Text = "记录路径";
            this.SavePathFile.UseVisualStyleBackColor = true;
            // 
            // GithubLabel
            // 
            this.GithubLabel.AutoSize = true;
            this.GithubLabel.Location = new System.Drawing.Point(13, 123);
            this.GithubLabel.Name = "GithubLabel";
            this.GithubLabel.Size = new System.Drawing.Size(41, 12);
            this.GithubLabel.TabIndex = 6;
            this.GithubLabel.TabStop = true;
            this.GithubLabel.Text = "Github";
            this.GithubLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GithubLabel_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(226, 125);
            this.Controls.Add(this.GithubLabel);
            this.Controls.Add(this.SavePathFile);
            this.Controls.Add(this.RoutePath);
            this.Controls.Add(this.pross);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gateiptext);
            this.Controls.Add(this.DelRoute);
            this.Controls.Add(this.AddRoute);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "FastRoute";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddRoute;
        private System.Windows.Forms.Button DelRoute;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pross;
        private System.Windows.Forms.TextBox gateiptext;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox RoutePath;
        private System.Windows.Forms.CheckBox SavePathFile;
        private System.Windows.Forms.LinkLabel GithubLabel;
    }
}

