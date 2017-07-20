namespace CSFaceCamera
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			this.labelCameraSelect = new System.Windows.Forms.Label();
			this.tscbxCameras = new System.Windows.Forms.ComboBox();
			this.btnConnect = new System.Windows.Forms.Button();
			this.Photograph = new System.Windows.Forms.Button();
			this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.labelPic = new System.Windows.Forms.Label();
			this.textBoxOutput = new System.Windows.Forms.TextBox();
			this.labelConsole = new System.Windows.Forms.Label();
			this.buttonAutoShot = new System.Windows.Forms.Button();
			this.timerAutoCompare = new System.Windows.Forms.Timer(this.components);
			this.buttonAddToLib = new System.Windows.Forms.Button();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.labelName = new System.Windows.Forms.Label();
			this.buttonCompare = new System.Windows.Forms.Button();
			this.timerAutoShot = new System.Windows.Forms.Timer(this.components);
			this.buttonReadLib = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// labelCameraSelect
			// 
			this.labelCameraSelect.AutoSize = true;
			this.labelCameraSelect.Location = new System.Drawing.Point(12, 9);
			this.labelCameraSelect.Name = "labelCameraSelect";
			this.labelCameraSelect.Size = new System.Drawing.Size(65, 12);
			this.labelCameraSelect.TabIndex = 10;
			this.labelCameraSelect.Text = "采集设备：";
			// 
			// tscbxCameras
			// 
			this.tscbxCameras.FormattingEnabled = true;
			this.tscbxCameras.Location = new System.Drawing.Point(73, 6);
			this.tscbxCameras.Name = "tscbxCameras";
			this.tscbxCameras.Size = new System.Drawing.Size(129, 20);
			this.tscbxCameras.TabIndex = 11;
			// 
			// btnConnect
			// 
			this.btnConnect.Location = new System.Drawing.Point(208, 4);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(75, 23);
			this.btnConnect.TabIndex = 12;
			this.btnConnect.Text = "连接";
			this.btnConnect.UseVisualStyleBackColor = true;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// Photograph
			// 
			this.Photograph.Location = new System.Drawing.Point(289, 4);
			this.Photograph.Name = "Photograph";
			this.Photograph.Size = new System.Drawing.Size(75, 23);
			this.Photograph.TabIndex = 14;
			this.Photograph.Text = "拍照";
			this.Photograph.UseVisualStyleBackColor = true;
			this.Photograph.Click += new System.EventHandler(this.Photograph_Click);
			// 
			// videoSourcePlayer
			// 
			this.videoSourcePlayer.Location = new System.Drawing.Point(14, 32);
			this.videoSourcePlayer.Name = "videoSourcePlayer";
			this.videoSourcePlayer.Size = new System.Drawing.Size(559, 297);
			this.videoSourcePlayer.TabIndex = 15;
			this.videoSourcePlayer.Text = "videoSourcePlayer";
			this.videoSourcePlayer.VideoSource = null;
			// 
			// pictureBox
			// 
			this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox.Location = new System.Drawing.Point(579, 32);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(559, 297);
			this.pictureBox.TabIndex = 16;
			this.pictureBox.TabStop = false;
			// 
			// labelPic
			// 
			this.labelPic.AutoSize = true;
			this.labelPic.Location = new System.Drawing.Point(577, 17);
			this.labelPic.Name = "labelPic";
			this.labelPic.Size = new System.Drawing.Size(71, 12);
			this.labelPic.TabIndex = 17;
			this.labelPic.Text = "采集的图片:";
			// 
			// textBoxOutput
			// 
			this.textBoxOutput.Location = new System.Drawing.Point(1144, 32);
			this.textBoxOutput.Multiline = true;
			this.textBoxOutput.Name = "textBoxOutput";
			this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxOutput.Size = new System.Drawing.Size(280, 297);
			this.textBoxOutput.TabIndex = 18;
			// 
			// labelConsole
			// 
			this.labelConsole.AutoSize = true;
			this.labelConsole.Location = new System.Drawing.Point(1144, 17);
			this.labelConsole.Name = "labelConsole";
			this.labelConsole.Size = new System.Drawing.Size(47, 12);
			this.labelConsole.TabIndex = 19;
			this.labelConsole.Text = "控制台:";
			// 
			// buttonAutoShot
			// 
			this.buttonAutoShot.Location = new System.Drawing.Point(370, 4);
			this.buttonAutoShot.Name = "buttonAutoShot";
			this.buttonAutoShot.Size = new System.Drawing.Size(97, 23);
			this.buttonAutoShot.TabIndex = 20;
			this.buttonAutoShot.Text = "自动拍照：关";
			this.buttonAutoShot.UseVisualStyleBackColor = true;
			this.buttonAutoShot.Click += new System.EventHandler(this.buttonAutoShot_Click);
			// 
			// timerAutoCompare
			// 
			this.timerAutoCompare.Interval = 2000;
			this.timerAutoCompare.Tick += new System.EventHandler(this.timerAutoCompare_Tick);
			// 
			// buttonAddToLib
			// 
			this.buttonAddToLib.Enabled = false;
			this.buttonAddToLib.Location = new System.Drawing.Point(724, 333);
			this.buttonAddToLib.Name = "buttonAddToLib";
			this.buttonAddToLib.Size = new System.Drawing.Size(86, 23);
			this.buttonAddToLib.TabIndex = 21;
			this.buttonAddToLib.Text = "添加到库中";
			this.buttonAddToLib.UseVisualStyleBackColor = true;
			this.buttonAddToLib.Click += new System.EventHandler(this.buttonAddToLib_Click);
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(618, 335);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(100, 21);
			this.textBoxName.TabIndex = 22;
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.Location = new System.Drawing.Point(577, 338);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(35, 12);
			this.labelName.TabIndex = 23;
			this.labelName.Text = "名字:";
			// 
			// buttonCompare
			// 
			this.buttonCompare.Enabled = false;
			this.buttonCompare.Location = new System.Drawing.Point(1338, 333);
			this.buttonCompare.Name = "buttonCompare";
			this.buttonCompare.Size = new System.Drawing.Size(86, 23);
			this.buttonCompare.TabIndex = 24;
			this.buttonCompare.Text = "比较人脸";
			this.buttonCompare.UseVisualStyleBackColor = true;
			this.buttonCompare.Click += new System.EventHandler(this.buttonCompare_Click);
			// 
			// timerAutoShot
			// 
			this.timerAutoShot.Interval = 500;
			this.timerAutoShot.Tick += new System.EventHandler(this.timerAutoShot_Tick);
			// 
			// buttonReadLib
			// 
			this.buttonReadLib.Location = new System.Drawing.Point(14, 333);
			this.buttonReadLib.Name = "buttonReadLib";
			this.buttonReadLib.Size = new System.Drawing.Size(350, 23);
			this.buttonReadLib.TabIndex = 25;
			this.buttonReadLib.Text = "导入至云数据库（当本地数据库改变时，请务必上传至云端）";
			this.buttonReadLib.UseVisualStyleBackColor = true;
			this.buttonReadLib.Click += new System.EventHandler(this.buttonReadLib_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1441, 383);
			this.Controls.Add(this.buttonReadLib);
			this.Controls.Add(this.buttonCompare);
			this.Controls.Add(this.labelName);
			this.Controls.Add(this.textBoxName);
			this.Controls.Add(this.buttonAddToLib);
			this.Controls.Add(this.buttonAutoShot);
			this.Controls.Add(this.labelConsole);
			this.Controls.Add(this.textBoxOutput);
			this.Controls.Add(this.labelPic);
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.videoSourcePlayer);
			this.Controls.Add(this.Photograph);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.tscbxCameras);
			this.Controls.Add(this.labelCameraSelect);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Twoentartian UESTC";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelCameraSelect;
		private System.Windows.Forms.ComboBox tscbxCameras;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.Button Photograph;
		private AForge.Controls.VideoSourcePlayer videoSourcePlayer;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.Windows.Forms.Label labelPic;
		private System.Windows.Forms.TextBox textBoxOutput;
		private System.Windows.Forms.Label labelConsole;
		private System.Windows.Forms.Button buttonAutoShot;
		private System.Windows.Forms.Timer timerAutoCompare;
		private System.Windows.Forms.Button buttonAddToLib;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.Button buttonCompare;
		private System.Windows.Forms.Timer timerAutoShot;
		private System.Windows.Forms.Button buttonReadLib;
	}
}

