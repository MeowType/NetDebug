using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeowType.NetDebug
{
    public partial class MsgBox : UserControl
    {

        public void LogSystem(params string[] msgs)
        {
            Dispatcher.Invoke(() =>
            {
                var p = new Paragraph()
                {
                    Inlines = {
                        new Run($"[ {DateTime.Now.ToString("zzz yyyy.MM.dd tt hh:mm:ss:fff")} ] [Info]"){
                            Foreground = new SolidColorBrush(Colors.DarkGray),
                            Typography =
                            {
                                StylisticAlternates = 1,
                            }
                        },
                    },
                    Typography =
                    {
                        StandardLigatures = true,
                        ContextualLigatures = true,
                        DiscretionaryLigatures = true,
                        HistoricalLigatures = true,
                        ContextualAlternates = true,
                    },
                };
                p.Inlines.AddRange(msgs.SelectMany(msg =>
                    new Inline[]{
                        new LineBreak(),
                        new Run(msg) { Typography = { StylisticAlternates = 1, }
                    }
                }));
                if (msgs.Length == 0) p.Inlines.Add(new LineBreak());
                Msg_Box.Document.Blocks.Add(p);

                Msg_Box.ScrollToEnd();
            });
        }

        public void LogError(Exception ex)
        {
            Dispatcher.Invoke(() =>
            {
                Msg_Box.Document.Blocks.Add(new Paragraph()
                {
                    Inlines = {
                        new Run($"[ {DateTime.Now.ToString("zzz yyyy.MM.dd tt hh:mm:ss:fff")} ] [Error]"){
                            Foreground = new SolidColorBrush(Colors.DarkRed),
                            Typography =
                            {
                                StylisticAlternates = 1,
                            }
                        },
                        new LineBreak(),
                        new Run(ex.ToString()){
                            Foreground = new SolidColorBrush(Colors.Red),
                            Typography =
                            {
                                StylisticAlternates = 1,
                            }
                        },
                    },
                    Typography =
                    {
                        StandardLigatures = true,
                        ContextualLigatures = true,
                        DiscretionaryLigatures = true,
                        HistoricalLigatures = true,
                        ContextualAlternates = true,
                    },
                });

                Msg_Box.ScrollToEnd();
            });
        }
        public void Log(EndPoint ep, string msg, bool from = false)
        {
            msg = msg.Replace("\0", string.Empty);
            string addr;
            if (ep is IPEndPoint iep)
            {
                addr = $"{iep.Address} :{iep.Port}";
            }
            else
            {
                addr = ep.ToString();
            }

            Dispatcher.Invoke(() =>
            {
                Msg_Box.Document.Blocks.Add(new Paragraph()
                {
                    Inlines = {
                        new Run($"[ {DateTime.Now.ToString("zzz yyyy.MM.dd tt hh:mm:ss:fff")} ]"){
                            Foreground = new SolidColorBrush(Colors.DarkGray),
                            Typography =
                            {
                                StylisticAlternates = 1,
                            }
                        },
                        new Run(from ? " <||| " : " |||> ") {
                            Foreground = new SolidColorBrush(from ? Colors.Green : Colors.Blue),
                            FontFamily = Utils.FiraCodeLight,
                            Typography =
                            {
                                StylisticAlternates = 1,
                            }
                        },
                        new Run($"{{ {addr} }}"){
                            Foreground = new SolidColorBrush(Colors.DarkGray),
                            Typography =
                            {
                                StylisticAlternates = 1,
                            }
                        },
                        new LineBreak(),
                        new Run(msg){
                            Foreground = new SolidColorBrush(from ? Colors.Green : Colors.Blue),
                            Typography =
                            {
                                StylisticAlternates = 1,
                            }
                        },
                    },
                    Typography =
                    {
                        StandardLigatures = true,
                        ContextualLigatures = true,
                        DiscretionaryLigatures = true,
                        HistoricalLigatures = true,
                        ContextualAlternates = true,
                    },
                }); ;

                Msg_Box.ScrollToEnd();
            });

        }
    }
}
