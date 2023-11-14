using HtmlAgilityPack;
using LazyPortal.Classes;
using LazyPortal.services;
using Matroska.Models;
using MetroFramework;
using MetroFramework.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Input;
using TraktNet.Utils;
using static MetroFramework.Drawing.MetroPaint.BackColor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace LazyPortal
{
    public partial class manga_browser : MetroFramework.Forms.MetroForm
    {
        private static readonly Main main = Program.Main_Form;
        private List<manga> Mangas;
        private manga currentManga;
        public manga_browser()
        {
            InitializeComponent();
            if (!Directory.Exists(Properties.Settings.Default.manga_destination))
                Directory.CreateDirectory(Properties.Settings.Default.manga_destination);
            defaultLocation.Text = Properties.Settings.Default.manga_destination;
            if (!Directory.Exists(Application.StartupPath + @"\Manga"))
                Directory.CreateDirectory(Application.StartupPath + @"\Manga");
            chapter_listview.Columns.Add("Name");
        }

        private void manga_browser_Load(object sender, EventArgs e)
        {
            if (!load_background_worker.IsBusy)
                load_background_worker.RunWorkerAsync();
        }

        public async Task<MangaDex> getManga(string mangaTitle)
        {
            try
            {
                HttpClient client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.mangadex.org/manga?title={mangaTitle}");
                request.Headers.UserAgent.Add(new ProductInfoHeaderValue("DFlow", "1.0"));
                HttpResponseMessage response = await client.SendAsync(request);
                return JsonConvert.DeserializeObject<MangaDex>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception) { return null; }
        }

        public async Task getMangaCover(manga manga)
        {
            try
            {
                HttpRequestMessage mangaRequest;
                //if (manga.mangadex_id != "" || manga.mangadex_id != null)
                //    mangaRequest = new HttpRequestMessage(HttpMethod.Get, $"https://api.mangadex.org/manga/{manga.mangadex_id}");
                //else
                    mangaRequest = new HttpRequestMessage(HttpMethod.Get, $"https://api.mangadex.org/manga?title={manga.english_name}");


                mangaRequest.Headers.UserAgent.Add(new ProductInfoHeaderValue("DFlow", "1.0"));
                MangaDex mangaDex = JsonConvert.DeserializeObject<MangaDex>(await (await new HttpClient().SendAsync(mangaRequest)).Content.ReadAsStringAsync());

                if (manga.mangadex_id == "" || manga.mangadex_id == null || manga.mangadex_id != mangaDex.data[0].id)
                    manga.mangadex_id = mangaDex.data[0].id;

                if (manga.cover_id != mangaDex.data[0].relationships.Find(x => x.type == "cover_art").id)
                    manga.cover_id = mangaDex.data[0].relationships.Find(x => x.type == "cover_art").id;
                var coverRequest = new HttpRequestMessage(HttpMethod.Get, $"https://api.mangadex.org/cover/{manga.cover_id}");
                coverRequest.Headers.UserAgent.Add(new ProductInfoHeaderValue("DFlow", "1.0"));
                CoverDex cover = JsonConvert.DeserializeObject<CoverDex>(await (await new HttpClient().SendAsync(coverRequest)).Content.ReadAsStringAsync());

                if (manga.cover_file_name != cover.data.attributes.fileName)
                    manga.cover_file_name = cover.data.attributes.fileName;

                if (!File.Exists(Application.StartupPath + @"\Manga\" + manga.cover_file_name))
                {
                    Bitmap mainBitmap = new Bitmap(WebRequest.Create($"https://uploads.mangadex.org/covers/{mangaDex.data[0].id}/{manga.cover_file_name}").GetResponse().GetResponseStream());
                    using (MemoryStream memory = new MemoryStream())
                    {
                        using (FileStream fs = new FileStream(Application.StartupPath + @"\Manga\" + manga.cover_file_name, FileMode.Create, FileAccess.ReadWrite))
                        {
                            mainBitmap.Save(memory, ImageFormat.Jpeg);
                            byte[] bytes = memory.ToArray();
                            fs.Write(bytes, 0, bytes.Length);
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally { database.updateObjectToDatabase(manga); }
        }

        public IDictionary<string, object> GetTokens()
        {
            HttpClient httpClient = new HttpClient();
            var formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", "DFlowD31"),
                new KeyValuePair<string, string>("password", "Tj@ycPX#0m3d^ZR2"),
                new KeyValuePair<string, string>("client_id", "personal-client-54f66996-29f0-4b97-8174-d1b9d217a81d-0cb9e8e8"),
                new KeyValuePair<string, string>("client_secret", "wIMAsC73n3Ln9vndtp3ydL3PFSgFK8zi")
            };
            HttpContent httpContent = new FormUrlEncodedContent(formData);

            HttpResponseMessage response = httpClient.PostAsync("https://auth.mangadex.org/realms/mangadex/protocol/openid-connect/token", httpContent).Result;
            string responseString = response.Content.ReadAsStringAsync().Result;
            IDictionary<string, object> responseJson = JsonConvert.DeserializeObject<IDictionary<string, object>>(responseString);

            return responseJson;
        }

        private void MetroPanel_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!main_background_worker.IsBusy)
            {
                currentManga = Mangas.Find(x => x.id == Convert.ToInt32(((MetroPanel)sender).Tag));
                main_background_worker.RunWorkerAsync();
            }
        }

        private void chapter_listview_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (chapter_listview.SelectedItems.Count > 0)
            {
                Process.Start("explorer.exe", chapter_listview.SelectedItems[0].Tag.ToString());
            }
        }

        private void main_background_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            main_grid.Invoke((MethodInvoker)delegate
            {
                main_grid.Enabled = false;
            });

            try
            {
                status_text.Invoke((MethodInvoker)delegate
                {
                    status_text.Text = "Searching chapter after " + (currentManga.latest_read_chapter).ToString();
                    status_text.ForeColor = Color.FromArgb((int)msgType.warning);
                });

                using (WebClient webClient = new WebClient() { Proxy = new WebProxy("38.154.227.167", 5868) { Credentials = new NetworkCredential("jcrreoto", "rxx4xn3vfovy"), BypassProxyOnLocal = false } })
                {
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(webClient.DownloadString(currentManga.base_url));

                    List<HtmlNode> chapterNodes = null;
                    chapterNodes = (from HtmlNode node in doc.DocumentNode.SelectNodes("//a[@class='block border border-border bg-card mb-3 p-3 rounded']")
                                    where (node.Attributes["href"].Value.Contains("-" + (currentManga.latest_read_chapter + 1).ToString() + "-")
                                    || node.Attributes["href"].Value.EndsWith("-" + (currentManga.latest_read_chapter + 1).ToString()))
                                    && node.Attributes["href"] != null
                                    select node).ToList();

                    if (chapterNodes.Count() > 0)
                    {
                        status_text.Invoke((MethodInvoker)delegate
                        {
                            status_text.Text = (currentManga.latest_read_chapter + 1).ToString() + " Available";
                            status_text.ForeColor = Color.FromArgb((int)msgType.success);
                        });

                        doc.LoadHtml(webClient.DownloadString("https://onepiecechapters.com" + chapterNodes[0].Attributes["href"].Value));
                        List<HtmlNode> imageNodes = null;
                        imageNodes = (from HtmlNode node in doc.DocumentNode.SelectNodes("//img[@class='fixed-ratio-content']")
                                      where node.Attributes["src"] != null
                                      && node.Attributes["src"].Value.EndsWith(".png")
                                      && node.Attributes["alt"].Value.Contains(" Page ")
                                      select node).ToList();

                        ProgressBar.Invoke((MethodInvoker)delegate
                        {
                            ProgressBar.Maximum = imageNodes.Count;
                            ProgressBar.Value = 0;
                            ProgressBar.Step = 1;
                        });
                        foreach (HtmlNode node in imageNodes)
                        {
                            if (!Directory.Exists(Properties.Settings.Default.manga_destination + @"\" + currentManga.english_name + @"\" + (currentManga.latest_read_chapter + 1) + @"\"))
                                Directory.CreateDirectory(Properties.Settings.Default.manga_destination + @"\" + currentManga.english_name + @"\" + (currentManga.latest_read_chapter + 1) + @"\");
                            webClient.DownloadFile(new Uri(node.Attributes["src"].Value), Properties.Settings.Default.manga_destination + @"\" + currentManga.english_name + @"\" + (currentManga.latest_read_chapter + 1) + @"\" + node.Attributes["alt"].Value + ".png");
                            ProgressBar.Invoke((MethodInvoker)delegate
                            {
                                ProgressBar.Value += 1;
                            });
                        }
                        currentManga.latest_read_chapter += 1;
                        database.updateObjectToDatabase(currentManga);
                    }
                    else
                    {
                        status_text.Invoke((MethodInvoker)delegate
                        {
                            status_text.Text = "New chapter unavailable";
                            status_text.ForeColor = Color.FromArgb((int)msgType.error);
                        });
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void main_background_worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                main_grid.Invoke((MethodInvoker)delegate
                {
                    main_grid.Enabled = true;
                });
                chapter_listview.Items.Clear();
                chapter_listview.Columns[0].Width = chapter_listview.Width - 17;
                chapter_listview.HeaderStyle = ColumnHeaderStyle.None;
                foreach (string chapter in Directory.GetDirectories(Properties.Settings.Default.manga_destination + @"\" + currentManga.english_name).ToList())
                {
                    chapter_listview.Items.Add(new ListViewItem() { Text = "Chapter " + chapter.Substring(chapter.LastIndexOf(@"\") + 1), Tag = Properties.Settings.Default.manga_destination + @"\" + currentManga.english_name + @"\" + chapter.Substring(chapter.LastIndexOf(@"\") + 1) });
                }
                chapter_listview.Sorting = SortOrder.Descending;
                chapter_listview.Visible = true;
            }
            catch (Exception) { };
        }

        private void chapter_listview_SizeChanged(object sender, EventArgs e)
        {
            chapter_listview.Columns[0].Width = chapter_listview.ClientRectangle.Width - 17;
        }

        private void defaultLocation_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Properties.Settings.Default.manga_destination = defaultLocation.Text;
        }

        private async void load_background_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                load_image.Invoke((MethodInvoker)delegate
                {
                    load_image.Visible = true;
                });
                Mangas = (List<manga>)database.getObjectFromDatabase<manga>();
                foreach (manga manga in Mangas)
                {
                    await getMangaCover(manga);
                    addCover(manga);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally 
            {
                load_image.Invoke((MethodInvoker)delegate
                {
                    load_image.Visible = false;
                });
            }
        }
        private void addCover(manga manga)
        {
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new Action(() => addCover(manga)));
                else
                {
                    MetroPanel metroPanel = new MetroPanel() { Visible = true, Size = new Size(191, 300), BackColor = Color.Green, UseCustomBackColor = true, Tag = manga.id, Cursor = System.Windows.Forms.Cursors.Hand, BackgroundImage = System.Drawing.Image.FromFile(Application.StartupPath + @"\Manga\" + manga.cover_file_name), BackgroundImageLayout = ImageLayout.Zoom };
                    metroPanel.MouseClick += MetroPanel_MouseClick;
                    main_grid.Controls.Add(metroPanel);
                }
            }
            catch (Exception) { }
        }
    }
}
