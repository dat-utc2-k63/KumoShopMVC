using System.Text;

namespace KumoShopMVC.Helpers
{
	public class MyUtil
	{
        public static string UpLoadAvatar(IFormFile Avatar, string folder)
        {
            try
            {
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", folder);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(Avatar.FileName);
                var fullPath = Path.Combine(directoryPath, uniqueFileName);

                using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    Avatar.CopyTo(myfile);
                }

                return uniqueFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return string.Empty;
            }
        }
        public static List<string> UpLoadListProduct(List<IFormFile> files, string folder)
        {
            var fileNames = new List<string>();

            foreach (var file in files)
            {
                try
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "img", folder);

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var fullPath = Path.Combine(directoryPath, uniqueFileName);

                    using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
                    {
                        file.CopyTo(myfile);
                    }

                    fileNames.Add(uniqueFileName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error uploading file {file.FileName}: {ex.Message}");
                }
            }

            return fileNames; // Trả về danh sách tên file
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
