using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using AForge.Video.DirectShow;
using FaceppSDK;
using System.Threading;
using System.Windows.Interop;


namespace CSFaceCamera
{
	public partial class MainForm : Form
	{
		private FaceService fs = new FaceService("f39178f0f1ad648438571377b0164f5d", "AHi9ON6jnTc24QEprvQXdt_AHkiiOS6r");
		private FilterInfoCollection _videoDevices;
		private volatile Bitmap TempImage;
		private volatile Bitmap RandomMemoImage;
		private volatile DetectResult nowDetectResult;
		private volatile int TrainCount;

		#region Form
		public MainForm()
		{
			InitializeComponent();
			Control.CheckForIllegalCrossThreadCalls = false;
		}
		private void MainForm_Load(object sender, EventArgs e)
		{
			CheckForIllegalCrossThreadCalls = false;
			try
			{
				//Find all the input devices
				_videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

				if (_videoDevices.Count == 0)
					throw new ApplicationException();

				foreach (FilterInfo device in _videoDevices)
				{
					tscbxCameras.Items.Add(device.Name);
				}
				tscbxCameras.SelectedIndex = 0;
			}
			catch (ApplicationException)
			{
				tscbxCameras.Items.Add("没有找到图像采集设备");
				_videoDevices = null;
			}
			Photograph.Enabled = false;
			buttonAutoShot.Enabled = false;
		}
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (btnConnect.Text == "断开")
			{
				videoSourcePlayer.SignalToStop();
				videoSourcePlayer.WaitForStop();
			}
		}
		#endregion

		private void ConsoleOutput(VerifyResult argVerifyResult, string argName, int OrderOfFace)
		{
			textBoxOutput.AppendText(String.Format("编号" + OrderOfFace.ToString() + "与" + argName + "是否为同一人：" + argVerifyResult.is_same_person.ToString() + "几率:{0:F2}" + System.Environment.NewLine, argVerifyResult.confidence));
		}

		private void ConsoleOutput(DetectResult argDetectResult)
		{
			textBoxOutput.Clear();
			State tempState = State.Instance();
			if (argDetectResult.face.Count == 0)
			{
				textBoxOutput.AppendText("没有检测到人脸!" + Environment.NewLine);
				tempState.IsFindFace = false;
			}
			else
			{
				tempState.IsFindFace = true;
				for (int i = 0; i < argDetectResult.face.Count; i++)
				{
					textBoxOutput.AppendText("编号" + i.ToString() + System.Environment.NewLine);
					textBoxOutput.AppendText("ID: " + argDetectResult.face[i].face_id + System.Environment.NewLine);
					textBoxOutput.AppendText(String.Format("年龄:{0:F2}~{1:F2}" + System.Environment.NewLine, (argDetectResult.face[i].attribute.age.value - argDetectResult.face[i].attribute.age.range).ToString(), (argDetectResult.face[i].attribute.age.value + argDetectResult.face[i].attribute.age.range).ToString()));
					textBoxOutput.AppendText(String.Format("性别:{0:F2} 几率:{1:F2}" + System.Environment.NewLine, argDetectResult.face[i].attribute.gender.value, argDetectResult.face[i].attribute.gender.confidence.ToString()));
					textBoxOutput.AppendText(String.Format("人种:{0:F2} 几率:{1:F2}" + System.Environment.NewLine, argDetectResult.face[i].attribute.race.value, argDetectResult.face[i].attribute.race.confidence.ToString()));
					textBoxOutput.AppendText(String.Format("左眼位置:{0:F2},{1:F2}" + System.Environment.NewLine, argDetectResult.face[i].position.eye_left.x, argDetectResult.face[i].position.eye_left.y));
					textBoxOutput.AppendText(String.Format("右眼位置:{0:F2},{1:F2}" + System.Environment.NewLine, argDetectResult.face[i].position.eye_right.x, argDetectResult.face[i].position.eye_right.y));
					textBoxOutput.AppendText(String.Format("嘴唇左边缘:{0:F2},{1:F2}" + System.Environment.NewLine, argDetectResult.face[i].position.mouth_left.x, argDetectResult.face[i].position.mouth_left.y));
					textBoxOutput.AppendText(String.Format("嘴唇右边缘:{0:F2},{1:F2}" + System.Environment.NewLine, argDetectResult.face[i].position.mouth_right.x, argDetectResult.face[i].position.mouth_right.y));
					textBoxOutput.AppendText(String.Format("鼻子:{0:F2},{1:F2}" + System.Environment.NewLine, argDetectResult.face[i].position.nose.x, argDetectResult.face[i].position.nose.y));
					textBoxOutput.AppendText(System.Environment.NewLine);
				}
			}
		}

		private void btnConnect_Click(object sender, EventArgs e)
		{
			CameraConn();
		}

		//Connect / Close camera
		private void TimerAutoShotOpen()
		{
			Thread.Sleep(1000);
			this.Invoke((EventHandler) (delegate
			{
				timerAutoShot.Enabled = true;
			}));
		}

		private void CameraConn()
		{
			if (btnConnect.Text == "连接")
			{
				VideoCaptureDevice videoSource = new VideoCaptureDevice(_videoDevices[tscbxCameras.SelectedIndex].MonikerString);
				videoSource.DesiredFrameSize = new System.Drawing.Size(videoSourcePlayer.Size.Width, videoSourcePlayer.Size.Height);
				videoSource.DesiredFrameRate = 1;
				videoSourcePlayer.VideoSource = videoSource;
				videoSourcePlayer.Start();
				btnConnect.Text = "断开";
				Thread tempThread = new Thread(new ThreadStart(TimerAutoShotOpen));
				tempThread.IsBackground = true;
				tempThread.Start();
				Photograph.Enabled = true;
				buttonAutoShot.Enabled = true;
			}
			else if (btnConnect.Text == "断开")
			{
				videoSourcePlayer.SignalToStop();
				videoSourcePlayer.WaitForStop();
				btnConnect.Text = "连接";
				timerAutoShot.Enabled = false;
				Photograph.Enabled = false;
				buttonAutoShot.Enabled = false;
			}
			else
			{
				//Never reach
			}
		}

		#region Flash Image In Memory
		private void timerAutoShot_Tick(object sender, EventArgs e)
		{
			try
			{
				if (videoSourcePlayer.IsRunning)
				{
					TempImage = videoSourcePlayer.GetCurrentVideoFrame();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("相机故障：" + ex.Message);
				return;
			}
		}
		#endregion

		#region Photograph and face process
		//Real time picture
		#region File to stream
		private byte[] SetImageToByteArray(string fileName)
		{
			byte[] image = null;
			try
			{
				FileStream fs = new FileStream(fileName, FileMode.Open);
				FileInfo fileInfo = new FileInfo(fileName);
				int streamLength = (int)fs.Length;
				image = new byte[streamLength];
				fs.Read(image, 0, streamLength);
				fs.Close();
				return image;
			}
			catch
			{
				return image;
			}
		}
		#endregion
		#region byte to MemoryStream
		public MemoryStream ByteToStream(byte[] mybyte)
		{
			MemoryStream mymemorystream = new MemoryStream(mybyte, 0, mybyte.Length);
			return mymemorystream;
		}
		#endregion

		//Change the state of timer
		private void buttonAutoShot_Click(object sender, EventArgs e)
		{
			if (timerAutoCompare.Enabled)
			{
				buttonAutoShot.Text = "自动拍照：关";
				timerAutoCompare.Enabled = false;
			}
			else
			{
				buttonAutoShot.Text = "自动拍照：开";
				timerAutoCompare.Enabled = true;
			}
		}

		//Timer to auto copare
		private void timerAutoCompare_Tick(object sender, EventArgs e)
		{
			Photograph_Click(sender, e);
		}

		//Shot and detect
		private void Photograph_Click(object sender, EventArgs e)
		{
			//Shot
			ParameterizedThreadStart ParStart = new ParameterizedThreadStart(Photograph_Cilck_Process);
			Thread ShotProcess = new Thread(ParStart);
			ShotProcess.IsBackground = true;
			if (sender == timerAutoCompare)
			{
				object temp = "TIMER";
				ShotProcess.Start(temp);
			}
			else if (sender == Photograph)
			{
				object temp = "BUTTON";
				ShotProcess.Start(temp);
			}
			else
			{
				//Never Reach
			}
		}

		//Shot and detect Process
		private void Photograph_Cilck_Process(object arg)
		{
			//Face detect
			try
			{
				Bitmap tempImage0 = new Bitmap(TempImage, pictureBox.Width, pictureBox.Height);
				string argString = (string) arg;
				if (argString == "TIMER")
				{
					
				}
				else if (argString == "BUTTON")
				{
					this.Invoke((EventHandler) (delegate
					{
						pictureBox.Image = tempImage0;
					}));
					textBoxOutput.Clear();
					textBoxOutput.AppendText("正在识别。" + Environment.NewLine);
				}
				else
				{
					//Never Reach
				}

				Bitmap tempImage1 = new Bitmap(TempImage, Env.PicProcessWidth, Env.PicProcessHeight);
				RandomMemoImage = TempImage;

				string tempPath = Path.GetTempFileName();
				tempImage1.Save(tempPath);

				DetectResult res = fs.Detection_DetectImg(tempPath);
				nowDetectResult = res;
				Graphics g = Graphics.FromImage(tempImage0);
				float FontSize = 20;
				for (int i = 0; i < res.face.Count; i++)
				{
					#region Draw lines
					float centerX = (float)(res.face[i].position.center.x / 100 * pictureBox.Width);
					float centerY = (float)(res.face[i].position.center.y / 100 * pictureBox.Height);
					float halfX = (float)(res.face[i].position.width / 2 / 100 * pictureBox.Width);
					float halfY = (float)(res.face[i].position.height / 2 / 100 * pictureBox.Height);
					g.DrawString(i.ToString(), new Font("等线", FontSize), Brushes.DarkTurquoise, new PointF(centerX - FontSize/2, centerY - FontSize/2));
					g.DrawLine(new Pen(Color.DarkTurquoise, 4), centerX - halfX, centerY - halfY, centerX + halfX, centerY - halfY);
					g.DrawLine(new Pen(Color.DarkTurquoise, 4), centerX - halfX, centerY + halfY, centerX + halfX, centerY + halfY);
					g.DrawLine(new Pen(Color.DarkTurquoise, 4), centerX - halfX, centerY - halfY, centerX - halfX, centerY + halfY);
					g.DrawLine(new Pen(Color.DarkTurquoise, 4), centerX + halfX, centerY - halfY, centerX + halfX, centerY + halfY);
					#endregion
				}
				g.Dispose();
				pictureBox.Image = tempImage0;
				#region Button enable
				buttonAddToLib.Enabled = true;
				buttonCompare.Enabled = true;
				#endregion
				ConsoleOutput(res);
				File.Delete(tempPath);
			}
			catch (Exception ex)
			{
				textBoxOutput.Clear();
				textBoxOutput.AppendText("识别已终止。" + Environment.NewLine);
				MessageBox.Show("图片识别中发生错误：" + ex.Message);
			}
			GC.Collect();
		}
		#endregion

		private void buttonAddToLib_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxName.Text))
			{
				MessageBox.Show("请输入名字 !", "错误");
				return;
			}
			if (nowDetectResult.face.Count != 1)
			{
				MessageBox.Show("图片中人脸个数不为1 !", "错误");
				return;
			}
			string libPath = System.Environment.CurrentDirectory + Path.DirectorySeparatorChar + Env.libFolder + Path.DirectorySeparatorChar + textBoxName.Text + Path.DirectorySeparatorChar;
			if (!Directory.Exists(libPath))
			{
				Directory.CreateDirectory(libPath);
			}
			long i = 0;
			while (File.Exists(libPath + i.ToString() + Env.libExtName))
			{
				i++;
			}
			RandomMemoImage.Save(libPath + i.ToString() + Env.libExtName, ImageFormat.Jpeg);
			MessageBox.Show(String.Format("写入库完成。总共有{0:D}个数据", i + 1), "信息");
			buttonAddToLib.Enabled = false;
			buttonCompare.Enabled = false;
		}

		private void buttonCompare_Click(object sender, EventArgs e)
		{
			string libPath = System.Environment.CurrentDirectory + Path.DirectorySeparatorChar + Env.libFolder + Path.DirectorySeparatorChar;
			if (!Directory.Exists(libPath))
			{
				MessageBox.Show("没有找到数据库 !", "错误");
				return;
			}
			string[] folders = Directory.GetFileSystemEntries(libPath);

			textBoxOutput.Clear();
			foreach (string singleFolder in folders)
			{
				string[] spiltString = new string[1]
				{
					Path.DirectorySeparatorChar.ToString()
				};
				string[] items = singleFolder.Split(spiltString, StringSplitOptions.RemoveEmptyEntries);
				string Name = items[items.Count() - 1];

				for (int i = 0; i < nowDetectResult.face.Count; i++)
				{
					VerifyResult tempVerifyResult = fs.Recognition_VerifyByName(nowDetectResult.face[i].face_id, Name);
					if (tempVerifyResult == null)
					{
						MessageBox.Show("本地数据库与云端不符！请先将数据同步到云端！", "错误");
						return;
					}
					ConsoleOutput(tempVerifyResult, Name, i);
				}
			}
			buttonAddToLib.Enabled = false;
			buttonCompare.Enabled = false;
		}

		private void ProcessCheckAsyncVerifyByName(object arg)
		{
			string[] session_id = (string[]) arg;
			while (true)
			{
				TrainSessionInfo tempTrainSessionInfo = fs.Info_GetTrainSession(session_id[0]);
				if (tempTrainSessionInfo.status == "INQUEUE")
				{
					continue;
				}
				else if (tempTrainSessionInfo.status == "SUCC")
				{
					textBoxOutput.AppendText(session_id[1] + "的人脸学习完成！" + Environment.NewLine);
					TrainCount--;
					if (TrainCount == 0)
					{
						textBoxOutput.AppendText("所有人脸学习完成！现在可以开始识别！" + Environment.NewLine);
					}
					break;
				}
				else if (tempTrainSessionInfo.status == "EXPIRED")
				{
					MessageBox.Show(session_id[1] + "：超时未响应，请检查网络连接后重试！", "错误");
					break;
				}
				else if (tempTrainSessionInfo.status == "FAILED")
				{
					MessageBox.Show(session_id[1] + "：学习失败，请检查数据库中的数据并重试！", "错误");
					break;
				}
				else
				{
					
				}
			}

		}

		private void buttonReadLib_Click(object sender, EventArgs e)
		{
			textBoxOutput.Clear();
			textBoxOutput.AppendText("请稍等。" + Environment.NewLine);

			fs.Group_DeleteByName(Env.GroupName);
			GroupBasicInfo tempGroupBasicInfo = fs.Group_CreateByIdList(Env.GroupName);
			string libPath = System.Environment.CurrentDirectory + Path.DirectorySeparatorChar + Env.libFolder + Path.DirectorySeparatorChar;
			if (!Directory.Exists(libPath))
			{
				MessageBox.Show("没有找到数据库 !", "错误");
				return;
			}
			string[] folders = Directory.GetFileSystemEntries(libPath);
			TrainCount = 0;

			//Every name in folder
			foreach (string singleFolder in folders)
			{
				TrainCount++;
				string[] spiltString = new string[1]
				{
					Path.DirectorySeparatorChar.ToString()
				};
				string[] items = singleFolder.Split(spiltString, StringSplitOptions.RemoveEmptyEntries);
				string Name = items[items.Count() - 1];
				fs.Person_DeleteByName(Name);
				PersonBasicInfo tempPersonBasicInfo = fs.Person_Create(Name);
				long i = 0;
				bool faceExist = false;
				while (File.Exists(libPath + Name + Path.DirectorySeparatorChar + i.ToString() + Env.libExtName))
				{
					faceExist = true;
					DetectResult res = fs.Detection_DetectImg(libPath + Name + Path.DirectorySeparatorChar + i.ToString() + Env.libExtName);
					if (res.face.Count != 1)
					{
						MessageBox.Show("路径" + libPath + Name + Path.DirectorySeparatorChar + i.ToString() + Env.libExtName + "中不存在人脸！已经自动删除","警告");
						File.Delete(libPath + Name + Path.DirectorySeparatorChar + i.ToString() + Env.libExtName);
						buttonReadLib_Click(sender, e);
						return;
					}
					ManageResult AddFaceResult = fs.Person_AddFaceById(tempPersonBasicInfo.person_id, res.face[0].face_id);
					if (!AddFaceResult.success)
					{
						MessageBox.Show("为用户添加脸部信息失败！", "错误");
						return;
					}
					i++;
				}
				if (!faceExist)
				{
					MessageBox.Show(Name + "中没有脸部数据！", "警告");
					continue;
				}
				ManageResult tempAddPersonResult = fs.Group_AddPerson("", Env.GroupName, "", Name);
				if (!tempAddPersonResult.success)
				{
					MessageBox.Show("添加用户信息失败！", "错误");
					return;
				}
				AsyncResult tempAsyncResult = fs.Train_VerifyByName(Name);
				ParameterizedThreadStart CheckFinished = new ParameterizedThreadStart(ProcessCheckAsyncVerifyByName);
				Thread CheckFinishedProcess = new Thread(CheckFinished);
				CheckFinishedProcess.IsBackground = true;
				string[] arguementStrings = new string[] {tempAsyncResult.session_id, Name};
				CheckFinishedProcess.Start(arguementStrings);

			}


			MessageBox.Show("数据库建立完成！请等待为每一个人的人脸学习过程完成。", "信息");
		}
	}
}
