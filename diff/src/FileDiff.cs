using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace diff
{
    public class FileDiff : Diff
    {
        private string fileOne;
        private string fileTwo;
        public FileDiff(string fileOne, string fileTwo)
        {
            this.fileOne = fileOne;
            this.fileTwo = fileTwo;
        }

        protected override void Initialize()
        {
            this.left = hashLines(fileOne);
            this.right = hashLines(fileTwo);
            this.tally = new Tally[this.left.Length, this.right.Length];
        }

        private string[] hashLines(string fileName)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                using (var fs = new FileStream(fileName, FileMode.Open))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        string line;
                        byte[] hash;
                        var result = new List<string>();
                        while ((line = sr.ReadLine()) != null)
                        {
                            hash = sha256.ComputeHash(Encoding.Unicode.GetBytes(line));
                            // result.Add(Convert.ToBase64String(hash));
                            result.Add(line);
                        }
                        return result.ToArray();
                    }
                }
            }
        }
    }
}