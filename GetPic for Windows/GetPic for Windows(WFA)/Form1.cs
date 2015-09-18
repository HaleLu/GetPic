using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace GetPic_for_Windows_WFA_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cookie = "Cookie:" + textBoxCookie.Text;
            _main(cookie);
        }

        private void _main(string cookie)
        {
            for (int college = int.Parse(textBoxCollegeMin.Text);
                college <= int.Parse(textBoxCollegeMax.Text);
                college++)
                for (int grade = int.Parse(textBoxGradeMin.Text); grade <= int.Parse(textBoxGradeMax.Text); grade++)
                    for (int major = int.Parse(textBoxMajorMin.Text); major <= int.Parse(textBoxMajorMax.Text); major++)
                        for (int _class = int.Parse(textBoxClassMin.Text);
                            _class <= int.Parse(textBoxClassMax.Text);
                            _class++)
                        {
                            for (int num = int.Parse(textBoxNumMin.Text); num <= int.Parse(textBoxNumMax.Text); num++)
                                try
                                {
                                    string id = "";
                                    if (college < 10) id += "0";
                                    id += college.ToString() + grade.ToString() + major.ToString();
                                    if (_class < 10) id += "0";
                                    id += _class;
                                    if (num < 10) id += "0";
                                    id += num;
                                    GetPic(id, cookie);
                                }
                                catch (Exception e)
                                {
                                    Trace.WriteLine(e.Message);
                                    if (college == int.Parse(textBoxCollegeMax.Text) + 1) return;
                                    if (grade == int.Parse(textBoxGradeMax.Text) + 1)
                                    {
                                        grade = int.Parse(textBoxGradeMin.Text);
                                        college++;
                                        major = int.Parse(textBoxMajorMin.Text);
                                        _class = int.Parse(textBoxClassMin.Text);
                                        num = int.Parse(textBoxNumMin.Text);
                                        continue;
                                    }
                                    if (_class == int.Parse(textBoxClassMin.Text) &&
                                        num == int.Parse(textBoxNumMin.Text))
                                    {
                                        _class++;
                                        num--;
                                        continue;
                                    }
                                    if (major <= int.Parse(textBoxMajorMax.Text) &&
                                        _class == int.Parse(textBoxClassMin.Text) + 1 &&
                                        num == int.Parse(textBoxNumMin.Text))
                                    {
                                        major++;
                                        _class--;
                                        num--;
                                        continue;
                                    }
                                    if (_class == int.Parse(textBoxClassMin.Text) + 1 &&
                                        num == int.Parse(textBoxNumMin.Text))
                                    {
                                        grade++;
                                        major = int.Parse(textBoxMajorMin.Text);
                                        _class = int.Parse(textBoxClassMin.Text);
                                        num--;
                                        continue;
                                    }
                                    if (num == int.Parse(textBoxNumMin.Text))
                                    {
                                        major++;
                                        _class = int.Parse(textBoxClassMin.Text);
                                        num--;
                                        continue;
                                    }
                                    if (num < int.Parse(textBoxNumMax.Text))
                                    {
                                        num++;
                                        continue;
                                    }
                                    break;
                                }
                        }
            // for (int college = 4; college <= 4; college++)
            //     for (int grade = 13; grade <= 14; grade++)
            //         for (int major = 0; major <= 9; major++)
            //             for (int _class = 0; _class <= 99; _class++)
            //             {
            //                 for (int num = 1; num <= 99; num++)
            //                     try
            //                     {
            //                         string id = "";
            //                         if (college < 10) id += "0";
            //                         id += college.ToString() + grade.ToString();
            //                         if (major < 10) id += "0";
            //                         id += major.ToString() + _class.ToString();
            //                         if (num < 10) id += "0";
            //                         id += num;
            //                         GetPic(id);
            //                     }
            //                     catch (Exception e)
            //                     {
            //                         Trace.WriteLine(e.Message);
            //                         if (college == 5) return;
            //                         if (grade == 15)
            //                         {
            //                             grade = 11;
            //                             college++;
            //                             major = 0;
            //                             _class = 0;
            //                             num = 0;
            //                             continue;
            //                         }
            //                         if (_class == 0 && num == 1)
            //                         {
            //                             _class++; num--;
            //                             continue;
            //                         }
            //                         if (major == 0 && _class == 1 && num == 1)
            //                         {
            //                             major++; _class--; num--;
            //                             continue;
            //                         }
            //                         if (_class == 1 && num == 1)
            //                         {
            //                             grade++; major = 0; _class = 0; num--;
            //                             continue;
            //                         }
            //                         if (num == 1)
            //                         {
            //                             major++; _class = 0; num--;
            //                             continue;
            //                         }
            //                         break;
            //                     }
            //             }
        }

        private static void GetPic(string id, string cookie)
        {
            Trace.WriteLine("------------------------HTTP GET------------------------");
            var url = "http://ded.nuaa.edu.cn/netean/GetPic.asp?pic=xh&xh=" + id;

            string[] headers =
            {
                "Accept-Encoding:gzip, deflate, sdch",
                "Accept-Language:zh-CN,zh;q=0.8",
                cookie,
            };

            GetData(url, "", "GET", "", id, headers);
        }

        private static void GetData(string url, string contentType, string method, string content, string id,
            params string[] headers)
        {
            Stream s;
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(url);
            req.Host = "ded.nuaa.edu.cn";
            req.KeepAlive = true;
            req.Accept = "image/webp,*/*;q=0.8";
            req.UserAgent =
                "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36";
            req.Referer = "http://ded.nuaa.edu.cn/netean/com/jbqkcx.asp";
            foreach (string header in headers)
                req.Headers.Add(header);
            if (method.Length > 0)
                req.Method = method;
            if (contentType.Length > 0)
                req.ContentType = contentType;
            if (content.Length > 0)
            {
                req.ContentLength = content.Length;
                s = req.GetRequestStream();
                StreamWriter sw = new StreamWriter(s);
                sw.Write(content);
                sw.Close();
            }
            DisplayRequest(req);
            try
            {
                HttpWebResponse res = (HttpWebResponse) req.GetResponse();
                DisplayResponse(res, id);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                throw new Exception("No this id!\n");
            }
            req.Abort();
        }

        private static void DisplayRequest(HttpWebRequest req)
        {
            Trace.WriteLine("*** Request Start ***");
            Trace.WriteLine(req.RequestUri.ToString());
            DisplayHeaders(req.Headers);
            Trace.WriteLine("*** Request End ***");
        }

        private static void DisplayResponse(HttpWebResponse hresp, string id)
        {
            Trace.WriteLine(null);
            Trace.WriteLine("*** Response Start ***");
            Trace.WriteLine(hresp.StatusCode);
            Trace.WriteLine(hresp.StatusDescription);
            DisplayHeaders(hresp.Headers);
            DisplayContent(hresp, id);
            hresp.Close();
            Trace.WriteLine("*** Response End ***");
            Trace.WriteLine("");
        }

        private static void DisplayContent(HttpWebResponse response, string id)
        {
            Stream stream = response.GetResponseStream();
            if (stream != null)
            {
                string filePath = "D:\\pic\\" + id + ".png";
                FileStream fs = new FileStream(filePath, FileMode.Create);
                GZipStream gzip = new GZipStream(stream, CompressionMode.Decompress);
                byte[] bytes = new byte[1024];
                int len;
                if ((len = gzip.Read(bytes, 0, bytes.Length)) > 0)
                {
                    fs.Write(bytes, 0, len);
                }
                else
                {
                    fs.Close();
                    File.Delete(filePath);
                    throw new Exception();
                }
                while ((len = gzip.Read(bytes, 0, bytes.Length)) > 0)
                {
                    fs.Write(bytes, 0, len);
                }
                fs.Close();
            }
        }

        private static void DisplayHeaders(WebHeaderCollection headers)
        {
            foreach (string key in headers.Keys)
                Trace.WriteLine(key + ":" + headers.Get(key));
        }
    }
}