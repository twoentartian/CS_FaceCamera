using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSFaceCamera
{
	static public class Env
	{
		public const string libFolder = "Face Lib";
		public const string libExtName = ".jpg";

		public const int PicProcessWidth = 200;
		public const int PicProcessHeight = 200;

		public const string GroupName = "TaoBao_FaceCompare";
	}

	public class State
	{
		#region Construction
		private static State _instance;
		protected State()
		{

		}
		public static State Instance()
		{
			return _instance ?? (_instance = new State());
		}
		#endregion

		public bool IsFindFace { get; set; }
	}
}
