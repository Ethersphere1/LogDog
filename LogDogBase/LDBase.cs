using LogDogBase.Models;
using Newtonsoft.Json;
using Serilog;
using System.Text.RegularExpressions;

namespace LogDogBase
{
    public class LDBase
    {

        public void baseInit()
        {

            // copying config file into app config
            string logDogRoam = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LogDog");
            if (!Directory.Exists(logDogRoam))
            {
                Directory.CreateDirectory(logDogRoam);
            }
            string configFileRoam = Path.Combine(logDogRoam, "configobal.json");
            try
            {
                File.Copy("configobal.json", configFileRoam);
            }
            catch (Exception)
            {
                { }
            }
            File.Delete("configobal.json");

            string jsonFile = File.ReadAllText(configFileRoam);

            TheLogsModel? deserializeTheLogsModel = JsonConvert.DeserializeObject<TheLogsModel>(jsonFile);

            string? whereToStoreLogs = getUseablePath(deserializeTheLogsModel.WhereToStoreLogs);
            string? whereToCreateTxtFiles = getUseablePath(deserializeTheLogsModel.WhereToCreateTxtFiles);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.File(Path.Combine(whereToStoreLogs, "logs.txt"),
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true,
                fileSizeLimitBytes: 70000000)
                .CreateLogger();

            while (true)
            {

                foreach (var item in deserializeTheLogsModel.LoggingItems)
                {
                    
                    foreach (KeyValuePair<string, string> logItemDictGetter in item.Value)
                    {
                        string logFilePath = "";
                        string logFileName = "";
                        string outputTempFileName = "";
                        //string whereToCreateTxtFiles = "";
                        string whenToDeleteTempFile = "";
                        string whenToCreateTempFile = "";

                        if (logItemDictGetter.Key.Equals("LogFilePath"))
                        {
                            logFilePath = getUseablePath(logItemDictGetter.Value);
                        }
                        if (logItemDictGetter.Key.Equals("LogFileName"))
                        {
                            logFileName = logItemDictGetter.Value;
                        }
                        if (logItemDictGetter.Key.Equals("OutputTempFileName"))
                        {
                            outputTempFileName = logItemDictGetter.Value;
                        }
                        if (logItemDictGetter.Key.Equals("WhenToDeleteTempFile"))
                        {
                            whenToDeleteTempFile = logItemDictGetter.Value;
                        }
                        if (logItemDictGetter.Key.Equals("WhenToCreateTempFile"))
                        {
                            whenToCreateTempFile = logItemDictGetter.Value;
                        }
                        Cerberus cerberus = new Cerberus(logFilePath, logFileName, outputTempFileName, whereToCreateTxtFiles, whenToDeleteTempFile, whenToCreateTempFile);
                    }
                    Thread.Sleep(150);
                }
                Thread.Sleep(150);
            }
        }

        public string getUseablePath(string rawPath)
        {
            string envRegExp = @"\%\w+\%";
            string environVar = "";
            List<string> pathDirs = new List<string>();
            MatchCollection mc = Regex.Matches(rawPath, envRegExp);

            foreach (Match m in mc)
            {
                environVar = m.ToString();
                rawPath = rawPath.Replace(environVar, "");
                pathDirs = rawPath.Split(Path.DirectorySeparatorChar).ToList();
            }
            string baseDirPath = EnvrDecoder(environVar);
            string completePath = Path.Combine(baseDirPath, pathDirs[0]);

            for (int i = 1; i < pathDirs.Count; i++)
            {
                completePath = Path.Combine(completePath, pathDirs[i]);
            }

            return completePath;
        } // getUseablePath

        public string EnvrDecoder(string environVar)
        {
            if (environVar.Equals("%CurrDocuments%"))
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else if (environVar.Equals("%Windows%"))
            {
                return Environment.SpecialFolder.Windows.ToString();
            }
            else if (environVar.Equals("%AppDataRoam%"))
            {
                return Environment.SpecialFolder.ApplicationData.ToString();
            }
            else
            {
                return "Invalid Environment Variable";
            }

        } // EnvrDecoder

    } // class
} // namespace