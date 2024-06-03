using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using UnitTestEx;
using Assert = NUnit.Framework.Assert;

namespace UnitTestProject
{
    /// <summary>
    /// Summary description for FileStorageTest
    /// </summary>
    [TestClass]
    public class FileStorageTest
    {
        public const string MAX_SIZE_EXCEPTION = "DIFFERENT MAX SIZE";
        public const string NULL_FILE_EXCEPTION = "NULL FILE";
        public const string NO_EXPECTED_EXCEPTION_EXCEPTION = "There is no expected exception";

        public const string SPACE_STRING = " ";
        public const string FILE_PATH_STRING = "@D:\\JDK-intellij-downloader-info.txt";
        public const string CONTENT_STRING = "Some text";
        public const string REPEATED_STRING = "AA";
        public const string WRONG_SIZE_CONTENT_STRING = "TEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtext";
        public const string TIC_TOC_TOE_STRING = "tictoctoe.game";

        public const int NEW_SIZE = 5;

        public FileStorage storage = new FileStorage(NEW_SIZE);

        /* ПРОВАЙДЕРЫ */


        public static IEnumerable<object[]> NewFilesData
        {
            get
            {
                return new[]
                {
                     new object[] {REPEATED_STRING, CONTENT_STRING },
                    new object[] { SPACE_STRING, WRONG_SIZE_CONTENT_STRING },
                    new object[] {FILE_PATH_STRING, CONTENT_STRING },
                };
            }
        }

        static object[] FilesForDeleteData =
        {
            new object[] { new File(REPEATED_STRING, CONTENT_STRING), REPEATED_STRING },
            new object[] { null, TIC_TOC_TOE_STRING }
        };

        
        public static IEnumerable<object[]> NewExceptionFileData
        {
            get
            {
                return new[]
                {
                    new object[] { (REPEATED_STRING, CONTENT_STRING) },

                };
            }
        }

        /* Тестирование записи файла */
        [TestMethod]
        [DynamicData (nameof(NewFilesData))]
        public void WriteTest(string fileName,string content) 
        {
            
            
                File file = new File (fileName,content);
                Assert.True(storage.Write(file));
                storage.DeleteAllFiles();
            

        }

        /* Тестирование записи дублирующегося файла */
        [TestMethod]
        [DynamicData (nameof(NewFilesData))]
        public void WriteExceptionTest(string fileName,string content)
        {
            File file = new File(fileName, content);

            bool isException = false;
            try
            {
                storage.Write(file);
                Assert.False(storage.Write(file));
                storage.DeleteAllFiles();
            } 
            catch (FileNameAlreadyExistsException)
            {
                isException = true;
            }
            Assert.True(isException, NO_EXPECTED_EXCEPTION_EXCEPTION);
        }

        /* Тестирование проверки существования файла */
        [TestMethod]
        [DynamicData(nameof(NewFilesData))]
        public void IsExistsTest(string fileName, string content) {
            File file = new File(fileName, content);
            String name = file.GetFilename();
            Assert.False(storage.IsExists(name));
            try {
                storage.Write(file);
            } catch (FileNameAlreadyExistsException e) {
                Console.WriteLine(String.Format("Exception {0} in method {1}", e.GetBaseException(), MethodBase.GetCurrentMethod().Name));
            }
            Assert.True(storage.IsExists(name));
            storage.DeleteAllFiles();
        }

        /* Тестирование удаления файла */
        [TestMethod]
        public void DeleteTest() {
            File file = new File(FILE_PATH_STRING,CONTENT_STRING);
            String fileName = FILE_PATH_STRING;
            storage.Write(file);
            Assert.True(storage.Delete(fileName));
        }

        /* Тестирование получения файлов */
        [TestMethod]
        public void GetFilesTest()
        {
            foreach (File el in storage.GetFiles()) 
            {
                Assert.NotNull(el);
            }
        }

        // Почти эталонный
        /* Тестирование получения файла */
        [TestMethod]
        [DynamicData(nameof(NewFilesData))]
        public void GetFileTest(string fileName, string content) 
        {

            File expectedFile = new File(fileName, content);
            storage.Write(expectedFile);

            File actualfile = storage.GetFile(expectedFile.GetFilename());
            bool difference = actualfile.GetFilename().Equals(expectedFile.GetFilename()) && actualfile.GetSize().Equals(expectedFile.GetSize());

            Assert.IsTrue(difference, string.Format("There is some differences in {0} or {1}", expectedFile.GetFilename(), expectedFile.GetSize()));
        }
        [TestMethod]
        [DynamicData(nameof(NewFilesData))]
        public void CheckTheFileForExistence_GetFile(string fileName, string content) 
        {
            File NKFile = new File(content, content);
            if (storage.IsExists(NKFile.GetFilename())==false)
            {
                bool difference = false;
                Assert.IsFalse(difference,string.Format("There is some differences in {0} ", NKFile.GetFilename()));
            
            }
            
            

        }
        [TestMethod]
        [DynamicData(nameof(NewFilesData))]
        public void CheckTheFileForExistence_DeletingFile(string fileName, string content)
        {
            File file = new File(fileName, content);
            File ActualFile = new File(content, content);
            storage.Write(file);
            Assert.False(storage.Delete(ActualFile.GetFilename()));

        }
    }
}
