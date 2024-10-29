using System.Text;

namespace KumoShopMVC.Helpers
{
	public class MyUtil
	{
		public static string UpLoadAvatar(IFormFile Avatar, string folder)
		{
			try
			{
				var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", "User", folder, Avatar.FileName);
				using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
				{
					Avatar.CopyTo(myfile);
				}
				return Avatar.FileName;
			}
			catch (Exception ex)
			{
				return string.Empty;
			}
		}

		public static String GenerRateRandomKey(int length = 5)
		{
			var pattern = @"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM!";
			var sb = new StringBuilder();
			var rd = new Random();
			for (int i = 0; i < length; i++)
			{
				sb.Append(pattern[rd.Next(0, pattern.Length)]);
			}
			return sb.ToString();
		}
	}
}
