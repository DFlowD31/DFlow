using HtmlAgilityPack;
using LazyPortal.services;
using Matroska.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MetroFramework.Controls;
using System.Text.RegularExpressions;

namespace LazyPortal
{
    public partial class web_browser : MetroFramework.Forms.MetroForm
    {
        private static readonly Main main = Program.Main_Form;
        private List<manga> Mangas;
        public web_browser()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync(Mangas.Find(x => x.id == Convert.ToInt32(((MetroButton)sender).Tag)));
            //downloadChapter(357, "https://onepiecechapters.com/mangas/3/black-clover", "Black Clover");
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync(Mangas.Find(x => x.id == Convert.ToInt32(((MetroButton)sender).Tag)));
            //downloadChapter(386, "https://onepiecechapters.com/mangas/6/my-hero-academia", "My Hero Academia");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            manga manga = e.Argument as manga;
            try
            {
                label2.Invoke((MethodInvoker)delegate
                {
                    label2.Text = "Searching chapter after " + (manga.latest_read_chapter).ToString();
                });

                using (WebClient webClient = new WebClient() { Proxy = new WebProxy("38.154.227.167", 5868) { Credentials = new NetworkCredential("jcrreoto", "rxx4xn3vfovy"), BypassProxyOnLocal = false } })
                {
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(webClient.DownloadString(manga.base_url));
                    
                    List<HtmlNode> chapterNodes = null;
                    chapterNodes = (from HtmlNode node in doc.DocumentNode.SelectNodes("//a[@class='block border border-border bg-card mb-3 p-3 rounded']")
                                    where (node.Attributes["href"].Value.Contains("-" + (manga.latest_read_chapter + 1).ToString() + "-")
                                    || node.Attributes["href"].Value.EndsWith("-" + (manga.latest_read_chapter + 1).ToString()))
                                    && node.Attributes["href"] != null
                                    select node).ToList();

                    if (chapterNodes.Count() > 0)
                    {
                        label2.Invoke((MethodInvoker)delegate
                        {
                            label2.Text = "Available";
                        });
                        label3.Invoke((MethodInvoker)delegate
                        {
                            label3.Text = (manga.latest_read_chapter + 1).ToString();
                        });

                        doc.LoadHtml(webClient.DownloadString("https://onepiecechapters.com" + chapterNodes[0].Attributes["href"].Value));
                        List<HtmlNode> imageNodes = null;
                        imageNodes = (from HtmlNode node in doc.DocumentNode.SelectNodes("//img[@class='fixed-ratio-content']")
                                      where node.Attributes["src"] != null
                                      && node.Attributes["src"].Value.EndsWith(".png")
                                      && node.Attributes["alt"].Value.Contains(" Page ")
                                      select node).ToList();

                        progressBar1.Invoke((MethodInvoker)delegate
                        {
                            progressBar1.Maximum = imageNodes.Count;
                            progressBar1.Value = 0;
                            progressBar1.Step = 1;
                        });
                        foreach (HtmlNode node in imageNodes)
                        {
                            if (!Directory.Exists(@"D:\Manga\" + manga.english_name + @"\" + (manga.latest_read_chapter + 1) + @"\"))
                                Directory.CreateDirectory(@"D:\Manga\" + manga.english_name + @"\" + (manga.latest_read_chapter + 1) + @"\");
                            webClient.DownloadFile(new Uri(node.Attributes["src"].Value), @"D:\Manga\" + manga.english_name + @"\" + (manga.latest_read_chapter + 1) + @"\" + node.Attributes["alt"].Value + ".png");
                            progressBar1.Invoke((MethodInvoker)delegate
                            {
                                progressBar1.Value += 1;
                            });
                        }
                        manga.latest_read_chapter += 1;
                        database.updateObjectToDatabase(manga);
                    }
                    else
                    {
                        label2.Invoke((MethodInvoker)delegate
                        {
                            label2.Text = "Unavailable";
                        });
                        label3.Invoke((MethodInvoker)delegate
                        {
                            label3.Text = "";
                        });
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void web_browser_Load(object sender, EventArgs e)
        {
            Mangas = (List<manga>)database.getObjectFromDatabase<manga>();
            metroButton1.Tag = Mangas.Find(x => x.english_name == "Black Clover").id;
            metroButton2.Tag = Mangas.Find(x => x.english_name == "My Hero Academia").id;
        }
    }
}
