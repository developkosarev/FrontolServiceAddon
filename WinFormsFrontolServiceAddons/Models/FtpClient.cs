﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FrontolServiceAddon
{
    public class FtpClient
    {
        private string password;
        private string userName;
        private string uri;
        private int bufferSize = 1024;

        public bool Passive = true;
        public bool Binary = true;
        public bool EnableSsl = false;
        public bool Hash = false;

        public FtpClient(string uri, string userName, string password)
        {
            this.uri = uri;
            this.userName = userName;
            this.password = password;
        }

        public string ChangeWorkingDirectory(string path)
        {
            uri = Combine(uri, path);

            return PrintWorkingDirectory();
        }

        public string DeleteFile(string fileName)
        {
            var request = CreateRequest(Combine(uri, fileName), WebRequestMethods.Ftp.DeleteFile);

            return GetStatusDescription(request);
        }

        public string DownloadFile(string source, string dest)
        {
            var request = CreateRequest(Combine(uri, source), WebRequestMethods.Ftp.DownloadFile);

            byte[] buffer = new byte[bufferSize];

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var fs = new FileStream(dest, FileMode.OpenOrCreate))
                    {
                        int readCount = stream.Read(buffer, 0, bufferSize);

                        while (readCount > 0)
                        {
                            if (Hash)
                                Console.Write("#");

                            fs.Write(buffer, 0, readCount);
                            readCount = stream.Read(buffer, 0, bufferSize);
                        }
                    }
                }

                return response.StatusDescription;
            }
        }

        public DateTime GetDateTimestamp(string fileName)
        {
            var request = CreateRequest(Combine(uri, fileName), WebRequestMethods.Ftp.GetDateTimestamp);

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return response.LastModified;
            }
        }

        public long GetFileSize(string fileName)
        {
            var request = CreateRequest(Combine(uri, fileName), WebRequestMethods.Ftp.GetFileSize);

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return response.ContentLength;
            }
        }

        public string[] ListDirectory()
        {
            var list = new List<string>();

            var request = CreateRequest(WebRequestMethods.Ftp.ListDirectory);

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, true))
                    {
                        while (!reader.EndOfStream)
                        {
                            list.Add(reader.ReadLine());
                        }
                    }
                }
            }

            return list.ToArray();
        }

        public string[] ListDirectoryDetails()
        {
            var list = new List<string>();

            var request = CreateRequest(WebRequestMethods.Ftp.ListDirectoryDetails);

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, true))
                    {
                        while (!reader.EndOfStream)
                        {
                            list.Add(reader.ReadLine());
                        }
                    }
                }
            }

            return list.ToArray();
        }

        public string MakeDirectory(string directoryName)
        {
            var request = CreateRequest(Combine(uri, directoryName), WebRequestMethods.Ftp.MakeDirectory);

            return GetStatusDescription(request);
        }

        public string PrintWorkingDirectory()
        {
            var request = CreateRequest(WebRequestMethods.Ftp.PrintWorkingDirectory);

            return GetStatusDescription(request);
        }

        public string RemoveDirectory(string directoryName)
        {
            var request = CreateRequest(Combine(uri, directoryName), WebRequestMethods.Ftp.RemoveDirectory);

            return GetStatusDescription(request);
        }

        public string Rename(string currentName, string newName)
        {
            var request = CreateRequest(Combine(uri, currentName), WebRequestMethods.Ftp.Rename);

            request.RenameTo = newName;

            return GetStatusDescription(request);
        }

        public string UploadFile(string source, string destination)
        {
            var request = CreateRequest(Combine(uri, destination), WebRequestMethods.Ftp.UploadFile);

            using (var stream = request.GetRequestStream())
            {
                using (var fileStream = System.IO.File.Open(source, FileMode.Open))
                {
                    int num;

                    byte[] buffer = new byte[bufferSize];

                    while ((num = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (Hash)
                            Console.Write("#");

                        stream.Write(buffer, 0, num);
                    }
                }
            }

            return GetStatusDescription(request);
        }

        public string UploadFileWithUniqueName(string source)
        {
            var request = CreateRequest(WebRequestMethods.Ftp.UploadFileWithUniqueName);

            using (var stream = request.GetRequestStream())
            {
                using (var fileStream = System.IO.File.Open(source, FileMode.Open))
                {
                    int num;

                    byte[] buffer = new byte[bufferSize];

                    while ((num = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (Hash)
                            Console.Write("#");

                        stream.Write(buffer, 0, num);
                    }
                }
            }

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return Path.GetFileName(response.ResponseUri.ToString());
            }
        }

        private FtpWebRequest CreateRequest(string method)
        {
            return CreateRequest(uri, method);
        }

        private FtpWebRequest CreateRequest(string uri, string method)
        {
            var r = (FtpWebRequest)WebRequest.Create(uri);                  

            r.Credentials = new NetworkCredential(userName, password);
            r.Method = method;
            r.UseBinary = Binary;
            r.EnableSsl = EnableSsl;
            r.UsePassive = Passive;

            return r;
        }

        private string GetStatusDescription(FtpWebRequest request)
        {
            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return response.StatusDescription;
            }
        }

        private string Combine(string path1, string path2)
        {
            return Path.Combine(path1, path2).Replace("\\", "/");
        }
    }
}