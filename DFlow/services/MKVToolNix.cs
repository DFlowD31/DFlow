using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using LanguageDetection;
using System.Text.RegularExpressions;

namespace LazyPortal.services
{
    public static class MKVToolNix
    {
        private static readonly Main main = Program.Main_Form;
        public static bool cancellationPending = false;
        private static int currentIndex = 0;
        public static bool mergeInProgress = false;
        #region Merge Movies

        public static async void MergeMovies()
        {
            await Task.Run(() => mergeMovies());
        }

        private static void mergeMovies()
        {
            string statusText = "All Done";
            try
            {
                using (FolderBrowserDialog Folder_Browser_Dialog = main.getFolderBrowserDialog())
                {
                    if (Folder_Browser_Dialog == null)
                        return;
                    List<string> Merging_Movies = new List<string>();
                    List<string> commands = new List<string>();
                    List<string> commands_report = new List<string>();
                    if (new choice_box().ShowDialog() == DialogResult.OK)
                    {
                        main.changeButtonText("Stop Merging", main.getCtlByName("Merge_Button"), msgType.error);
                        if (choice.series)
                        {

                            //needs more tinkering

                            foreach (FileInfo fInfo in new DirectoryInfo(Folder_Browser_Dialog.SelectedPath).GetFiles("*.*", SearchOption.AllDirectories).Where(f => extension.videoEXT.Contains(f.Extension.ToLower())).ToArray())
                            {
                                commands.Add(@"""" + Application.StartupPath + @"\MKVToolNix\/k mkvmerge.exe --ui-language en --output ^""" + Properties.Settings.Default.merge_destination + @"\" + Path.GetFileNameWithoutExtension(fInfo.Name) + @".mkv^"" --language 0:und --language 1:und ^""^(^"" ^""" + fInfo.DirectoryName + @"\" + Path.GetFileNameWithoutExtension(fInfo.Name) + "" + fInfo.Extension + @"^"" ^""^)^"" --language 0:en --track-name ^""0:English Subtitle ^"" --default-track 0:yes --forced-track 0:yes ^""^(^"" ^""" + fInfo.DirectoryName + @"\" + Path.GetFileNameWithoutExtension(fInfo.Name) + @".srt ^"" ^""^)^"" --track-order 0:0,0:1,1:0");
                                if (new DirectoryInfo(Folder_Browser_Dialog.SelectedPath).GetFiles(Path.GetFileNameWithoutExtension(fInfo.Name) + ".*", SearchOption.AllDirectories).Where(f => extension.subtitleEXT.Contains(f.Extension.ToLower())).ToArray().Count() > 0)
                                    commands_report.Add("Done.");
                                else
                                    commands_report.Add("Subtitle File Not Found.");
                                Merging_Movies.Add(Path.GetFileNameWithoutExtension(fInfo.Name));
                                main.Log(@"""" + Application.StartupPath + @"\MKVToolNix\/k mkvmerge.exe --ui-language en --output ^""" + Properties.Settings.Default.merge_destination + @"\" + Path.GetFileNameWithoutExtension(fInfo.Name) + @".mkv^"" --language 0:und --language 1:und ^""^(^"" ^""" + fInfo.DirectoryName + @"\" + Path.GetFileNameWithoutExtension(fInfo.Name) + "" + fInfo.Extension + @"^"" ^""^)^"" --language 0:en --track-name ^""0:English Subtitle ^"" --default-track 0:yes --forced-track 0:yes ^""^(^"" ^""" + fInfo.DirectoryName + @"\" + Path.GetFileNameWithoutExtension(fInfo.Name) + @".srt ^"" ^""^)^"" --track-order 0:0,0:1,1:0", msgType.message);
                            }
                        }
                        else
                        {
                            List<DirectoryInfo> allDirectory = new List<DirectoryInfo>() { new DirectoryInfo(Folder_Browser_Dialog.SelectedPath) };
                            if (choice.container)
                                allDirectory = new DirectoryInfo(Folder_Browser_Dialog.SelectedPath).GetDirectories().ToList();

                            foreach (DirectoryInfo dInfo in allDirectory)
                            {
                                FileInfo[] allFileInfo = new DirectoryInfo(dInfo.FullName).GetFiles("*.*", SearchOption.AllDirectories);
                                FileInfo[] vidInfo = allFileInfo.Where(f => extension.videoEXT.Contains(f.Extension.ToLower())).ToArray();
                                FileInfo[] srtInfo = allFileInfo.Where(f => extension.subtitleEXT.Contains(f.Extension.ToLower())).ToArray();

                                string vidName = Path.GetFileNameWithoutExtension(vidInfo[0].Name);

                                string commandStr = @"""" + Application.StartupPath + @"\MKVToolNix\/c mkvmerge.exe --ui-language en --output ^""" + Properties.Settings.Default.merge_destination + @"\" + Path.GetFileNameWithoutExtension(dInfo.Name) + @".mkv^"" --language 0:und --language 1:und ^""^(^"" ^""" + dInfo.FullName + @"\" + vidName + vidInfo[0].Extension + @"^"" ^""^)^"" ";

                                string trackOrder = "--track-order 0:0,0:1,";
                                foreach (FileInfo srt in srtInfo)
                                {
                                    string dLanguage = string.Empty;
                                    
                                    using (StreamReader sReader = new StreamReader(srt.FullName))
                                    {
                                        LanguageDetector detector = new LanguageDetector();
                                        detector.AddAllLanguages();
                                        dLanguage = detector.Detect(sReader.ReadToEnd());
                                    }

                                    int objIndex = Array.IndexOf(srtInfo, srt);
                                    commandStr += @"--language " + objIndex + @":" + dLanguage + @" ";

                                    if (dLanguage == "en")
                                        commandStr += @"--default-track " + objIndex + @":yes --forced-track " + objIndex + @":yes ";

                                    commandStr += @"^""^(^"" ^""" + dInfo.FullName + @"\" + Path.GetFileNameWithoutExtension(srt.Name) + srt.Extension + @" ^"" ^""^)^"" ";
                                    trackOrder += (objIndex + 1) + ":0,";
                                }

                                commandStr += trackOrder.Substring(0, trackOrder.LastIndexOf(","));

                                if (srtInfo.Length > 0)
                                    commands_report.Add("Done.");
                                else
                                    commands_report.Add("Muxed without subtitle.");

                                commands.Add(commandStr);
                                Merging_Movies.Add(Path.GetFileNameWithoutExtension(dInfo.Name) + ".mkv");
                            }
                        }

                        main.maxProgress(commands.Count * 100, main.ProgressBar);
                        main.maxProgress(100, main.Mini_ProgressBar);

                        mergeInProgress = true;
                        foreach (string merge_command in commands)
                        {
                            main.Log("Multiplexing Movie --> " + Merging_Movies[commands.IndexOf(merge_command)] + "... ", msgType.message);
                            main.changeStatus("Merging " + (currentIndex + 1) + " of " + commands.Count, msgType.message);

                            Process p = Process.Start(new ProcessStartInfo() { FileName = "cmd.exe", Arguments = merge_command, RedirectStandardInput = true, RedirectStandardOutput = true, UseShellExecute = false, CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden });
                            p.OutputDataReceived += new DataReceivedEventHandler(process_OutputDataReceived);
                            p.BeginOutputReadLine();
                            do
                            {
                                try
                                {
                                    if (cancellationPending)
                                    {
                                        p.CancelOutputRead();
                                        foreach (Process mkvmerge in Process.GetProcessesByName("mkvmerge"))
                                            mkvmerge.Kill();
                                        p.Kill();
                                        p.Close();
                                        statusText = "Canceled by user.";
                                        main.Log("Canceled", msgType.error, true);
                                        goto Finish;
                                    }
                                }
                                catch (Exception) { }
                            } while (!p.HasExited);
                            p.WaitForExit();
                            currentIndex += 1;

                            if (commands_report[commands.IndexOf(merge_command)] == "Done.")
                                main.Log(commands_report[commands.IndexOf(merge_command)], msgType.success, true);
                            else
                                main.Log(commands_report[commands.IndexOf(merge_command)], msgType.error, true);
                        }
                    }
                }
            Finish:;
            }
            catch (Exception ex)
            {
                main.Log(ex.ToString(), msgType.error);
                statusText = "Error";
            }
            finally
            {
                main.updateProgressBar(0, main.ProgressBar);
                main.updateProgressBar(0, main.Mini_ProgressBar);
                if (statusText == "Error")
                    main.changeStatus(statusText, msgType.error);
                else
                    main.changeStatus(statusText, msgType.success);
                mergeInProgress = false;
                main.changeButtonText("Merge Movies", main.getCtlByName("Merge_Button"), msgType.baseColor);
            }
        }

        public static void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data.Contains("Progress"))
                {
                    int n = Convert.ToInt32(e.Data.Substring(e.Data.LastIndexOf("Progress") + 9).Replace("%", string.Empty));
                    main.updateProgressBar(n, main.Mini_ProgressBar);
                    main.updateProgressBar((currentIndex * 100) + n, main.ProgressBar);
                }
            }
            catch (Exception) { }
        }

        #endregion
    }
}
