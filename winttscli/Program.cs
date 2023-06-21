using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;

namespace winttscli
{
    class Program
    {
        static void Main(string[] args)
        {
            var switchMappings = new Dictionary<string, string>()
            {
                {"-o","output"},
                {"-i", "input"}
            };
            
            var builder = new ConfigurationBuilder();
            builder.AddCommandLine(args, switchMappings);
            var config = builder.Build();

            string toSpeak = "你好";
            String timeStamp = Convert.ToString(System.DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            string outputPath = Directory.GetCurrentDirectory() +"/"+ timeStamp + ".wav";
            Console.WriteLine(config["input"]);

            System.Speech.Synthesis.SpeechSynthesizer ss = new System.Speech.Synthesis.SpeechSynthesizer();
            var culture = System.Globalization.CultureInfo.GetCultureInfo("zh-CN");
            var voices = ss.GetInstalledVoices(culture);
            ss.SelectVoice(voices[0].VoiceInfo.Name);
            ss.Volume = 100;

            if (config["output"] != null)
            {
                outputPath = config["output"] + "/" + timeStamp + ".wav";
            }

            if (config["input"] != null)
            {
                FileAttributes attr = File.GetAttributes(config["input"]);
                Console.WriteLine(attr);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    string outputFolder = Directory.GetCurrentDirectory() + "/" + timeStamp;
                    Directory.CreateDirectory(outputFolder);
                    string[] files = Directory.GetFiles(config["input"], "*", SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < files.Length; i++) {
                        toSpeak = "";
                        string cFilePath = files[i];
                        string ext = Path.GetExtension(cFilePath);
                        string filename = Path.GetFileNameWithoutExtension(cFilePath);
                        string outputFilePath = outputFolder + "/" + filename + ".wav";
                        switch (ext) {
                            case (".epub"):
                                break;
                            case (".txt"):
                                using (var sr = new StreamReader(cFilePath))
                                {
                                    toSpeak = sr.ReadToEnd();
                                    if (voices.Count > 0)
                                    {
                                        ss.SetOutputToWaveFile(outputFilePath);
                                        ss.Speak(toSpeak);
                                        ss.SetOutputToNull();
                                    }
                                }
                                Console.WriteLine("Wrote to " + outputFilePath);
                                break;
                            case (".html"):
                                break;
                            default:
                                break;
                        }
                        //break;
                    }
                }
                else
                {
                    using (var sr = new StreamReader(config["input"]))
                    {             
                        //Console.WriteLine(sr.ReadToEnd());
                        // TODO: Handle different file types in a separate function
                        toSpeak = "";
                        toSpeak = sr.ReadToEnd();
                        if (voices.Count > 0)
                        {
                            ss.SetOutputToWaveFile(outputPath);
                            ss.Speak(toSpeak);
                            ss.SetOutputToNull();
                        }
                        Console.WriteLine("Wrote to " + outputPath);
                    }
                }
            }
            //Console.WriteLine("Press any key to exit.");
            //Console.ReadKey();
        }
    }
}
