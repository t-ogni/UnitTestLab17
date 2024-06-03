using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using UnitTestEx;
using Assert = NUnit.Framework.Assert;

namespace UnitTestProject
{
    [TestClass]
    public class FileTest
    {

        public const string SIZE_EXCEPTION = "Wrong size";
        public const string NAME_EXCEPTION = "Wrong name";
        public const string SPACE_STRING = " ";
        public const string FILE_PATH_STRING = "@D:\\JDK-intellij-downloader-info.txt";
        public const string CONTENT_STRING = "Some text";
        public double lenght;

        /* ПРОВАЙДЕР */
        static object[] FilesData =
        {
            new object[] {new File(FILE_PATH_STRING, CONTENT_STRING), FILE_PATH_STRING, CONTENT_STRING},
            new object[] { new File(SPACE_STRING, SPACE_STRING), SPACE_STRING, SPACE_STRING}
        };

        
        [TestMethod]
        public void GetSizeTest()
        {
            File newFile = new File(FILE_PATH_STRING, CONTENT_STRING);
            String content = CONTENT_STRING;
            lenght = content.Length / 2;
            Assert.AreEqual(newFile.GetSize(), lenght, SIZE_EXCEPTION);

        }

        /* Тестируем получение имени */
        [TestMethod]
        public void GetFilenameTest()
        {
            File newFile = new File(FILE_PATH_STRING, CONTENT_STRING);
            String name = FILE_PATH_STRING;
            Assert.AreEqual(newFile.GetFilename(), name, NAME_EXCEPTION);
        }
        [TestMethod]
        public void GetContentTest()
        {
            File file = new File(FILE_PATH_STRING, CONTENT_STRING);
            String content = CONTENT_STRING;
            Assert.AreEqual(file.GetContent(), content, NAME_EXCEPTION);
        }

    }
}
