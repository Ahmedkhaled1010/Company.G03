namespace Company.G03.PL.Helper
{
    public static class DocumentSettings
    {

        public static string UploadFile(IFormFile file,string FolderName)
        {
            //1
            var FolderPath =Path.Combine(Directory.GetCurrentDirectory(),"wwwroot//Files", FolderName);
            //2
            var FileName =$"{Guid.NewGuid()}{file.FileName}";
            //3
            var FilePath = Path.Combine(FolderPath,FileName);
            //4
          using  var FileStream = new FileStream(FilePath,FileMode.Create);
            file.CopyTo(FileStream);
            return FileName;

        }
        public static void DeleteFile(string FileName, string FolderName) 
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, FileName);
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
